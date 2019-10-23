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
        private readonly UserManager<User> _userManager;

        public bool deleted = false;


        
        public SQLBoardRepository(TrelloDbContext trelloDbContext,
                                   UserManager<User> userManager)
        {
            _trelloDbContext = trelloDbContext;
            _userManager = userManager;
        }

        private BackgroundColor FindBgColor(Board board)
        {
            return _trelloDbContext.BackgroundColors.Find(board.CurrentBackgroundColorId);
        }

        public BoardBgDTO GetBoard(int id)
        {
            try
            {
                //should be caught System.InvalidOperationException
                Board board = _trelloDbContext.Boards.Where(b => b.BoardId == id)
                                                     .Include(b => b.BackgroundColor)
                                                     .AsNoTracking()
                                                     .FirstOrDefault();
                //BackgroundColor bgColor = FindBgColor(board);
                BoardBgDTO boardBgDTO = new BoardBgDTO();

                boardBgDTO.Id = board.BoardId;
                boardBgDTO.Title = board.Title;
                boardBgDTO.BgColorId = board.BackgroundColor.BackgroundColorId;
                boardBgDTO.BgColorName = board.BackgroundColor.ColorName;
                boardBgDTO.BgColorHex = board.BackgroundColor.ColorHex;
                return boardBgDTO;
            }
            catch (Exception e)
            {
                throw new BoardDoesNotExistException(e.Message);//throw custom repo-ex
            }

            
        }

        public List<BoardBgDTO> GetAllBoards()
        {
            IEnumerable<Board> boards = _trelloDbContext.Boards.Include(b => b.BackgroundColor)
                                                               .AsNoTracking()
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

        public int Create(BoardDTO newBoardDTO)
        {
            Board board = new Board();
            board.Title = newBoardDTO.Title;
            board.CurrentBackgroundColorId = newBoardDTO.CurrentBackgroundColorId;

            //var userId = _userManager.GetUserId(HttpContext.User);
            _trelloDbContext.Boards.Add(board);
            try
            {
                _trelloDbContext.SaveChanges();
            }
            
            catch (Exception)
            {
                throw new BoardTitleAlreadyExists();//throw custom repo-ex
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
            catch (Exception)
            {
                throw new BoardTitleAlreadyExists();//throw custom repo-ex
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
            catch (Exception)
            {
                throw new Exception();
            }
            
            
        }
    }
}
