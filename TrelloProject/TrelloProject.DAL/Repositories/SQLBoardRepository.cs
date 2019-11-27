using AutoMapper;
using System;
using System.Collections.Generic;
using TrelloProject.DAL.EF;
using TrelloProject.DAL.Entities;
using TrelloProject.BLL.Interfaces.RepositoriesInterfaces;
using TrelloProject.DTOsAndViewModels.DTOs;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TrelloProject.DTOsAndViewModels.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace TrelloProject.DAL.Repositories
{
    internal class SQLBoardRepository : IBoardDTORepository
    {
       
        private readonly TrelloDbContext _trelloDbContext;
        //private readonly UserManager<User> _userManager;

        public bool deleted = false;


        
        public SQLBoardRepository(TrelloDbContext trelloDbContext) //UserManager<User> userManager
        {
            _trelloDbContext = trelloDbContext;
           // _userManager = userManager;
        }

        public BoardBgDTO GetBoard(int id)
        {
            try
            {
                
                Board board = _trelloDbContext.Boards.Where(b => b.BoardId == id)
                                                     .Include(b => b.BackgroundColor)
                                                     .AsNoTracking()
                                                     .FirstOrDefault();
                

                BoardBgDTO boardBgDTO = new BoardBgDTO();

                boardBgDTO.Id = board.BoardId;
                boardBgDTO.Title = board.Title;
                boardBgDTO.BgColorId = board.BackgroundColor.BackgroundColorId;
                boardBgDTO.BgColorName = board.BackgroundColor.ColorName;
                boardBgDTO.BgColorHex = board.BackgroundColor.ColorHex;
                
                return boardBgDTO;
            }
            catch(Exception innerEx)
            {
                throw new ApiException(404, innerEx, 6);
            }
        }

        public List<BoardBgDTO> GetAllBoards(string userId)
        {
            IEnumerable<Board> boards = _trelloDbContext.Boards.Include(b => b.BackgroundColor)
                                                               .AsNoTracking()
                                                               .OrderBy(b => b.Title)
                                                               .ToList();

            List<UserBoard> userBoards = _trelloDbContext.UserBoards.Where(x => x.UserId == userId).ToList();

            List<BoardBgDTO> boardsBgDTO = new List<BoardBgDTO>();
            
            foreach (Board board in boards)
            {
                foreach(var userBoard in userBoards)
                {
                    if(userBoard.BoardId == board.BoardId)
                    {
                        BoardBgDTO boardBgDTO = new BoardBgDTO();
                
                        boardBgDTO.Id = board.BoardId;
                        boardBgDTO.Title = board.Title;
                        boardBgDTO.BgColorId = board.BackgroundColor.BackgroundColorId;
                        boardBgDTO.BgColorName = board.BackgroundColor.ColorName;
                        boardBgDTO.BgColorHex = board.BackgroundColor.ColorHex;

                        boardsBgDTO.Add(boardBgDTO);
                    }
                }
                 
            }
            return boardsBgDTO;
        }

        public int Create(BoardDTO newBoardDTO, string userId)
        {
            if (userId == null)
            {
                throw new ApiException(400, 19);
            }
            Board board = new Board();
            board.Title = newBoardDTO.Title;
            board.CurrentBackgroundColorId = newBoardDTO.CurrentBackgroundColorId;

            try
            {
                var test = new UserBoard(){ UserId = userId };
                board.UserBoards.Add(test);

                _trelloDbContext.Boards.Add(board);
                _trelloDbContext.SaveChanges();
            }
            
            catch (Exception innerEx)
            {
                throw new ApiException(400, innerEx, 1);
            }
            return board.BoardId;
        }

        public bool Update(BoardDTO updatedBoardDTO)
        {
            Board board = new Board();
            board.BoardId = updatedBoardDTO.BoardId;
            board.Title = updatedBoardDTO.Title;
            board.CurrentBackgroundColorId = updatedBoardDTO.CurrentBackgroundColorId;

            _trelloDbContext.Boards.Attach(board);
            _trelloDbContext.Entry(board).State = EntityState.Modified;

            try
            {
                return (_trelloDbContext.SaveChanges() > 0 ? true : false);
            }
            catch (Exception innerEx)
            {
                throw new ApiException(400, innerEx, 2);
            }
        }

        public bool Delete(int id)
        {
            Board boardToDelete = _trelloDbContext.Boards.Find(id);
            _trelloDbContext.Remove(boardToDelete);
            try
            {
                int deleted = _trelloDbContext.SaveChanges();
                if(deleted > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                 
            }
            catch (Exception innerEx)
            {
                throw new ApiException(400, innerEx, 7);
            }           
        }

        public string GetUserIdFromContext(int boardId)
        {
            var userBoard = _trelloDbContext.UserBoards.Where(b => b.BoardId == boardId).AsNoTracking().FirstOrDefault();
            string userId = string.Empty;
            try
            {
                userId = userBoard.UserId;
            }

            catch(Exception)
            {
                throw new ApiException(400, new Exception("You do not own this board"), 20);
            }
            
            return userId;
        }

        public List<BoardBgDTO> GetAllBoards()
        {
            IEnumerable<Board> boards = _trelloDbContext.Boards.Include(b => b.BackgroundColor)
                                                               .AsNoTracking()
                                                               .OrderBy(b => b.Title)
                                                               .ToList();
            List<BoardBgDTO> boardsBgDTO = new List<BoardBgDTO>();
            foreach (Board board in boards)
            {
                BoardBgDTO boardBgDTO = new BoardBgDTO();

                boardBgDTO.Id = board.BoardId;
                boardBgDTO.Title = board.Title;
                boardBgDTO.BgColorId = board.BackgroundColor.BackgroundColorId;
                boardBgDTO.BgColorName = board.BackgroundColor.ColorName;
                boardBgDTO.BgColorHex = board.BackgroundColor.ColorHex;

                boardsBgDTO.Add(boardBgDTO);
            }
            return boardsBgDTO;
        }
    }
}
