using AutoMapper;
using System;
using System.Collections.Generic;
using TrelloProject.DAL.EF;
using TrelloProject.DAL.Entities;
using TrelloProject.BLL.Interfaces.RepositoriesInterfaces;
using TrelloProject.DTOsAndViewModels.DTOs;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TrelloProject.DAL.Repositories
{
    internal class SQLBoardRepository : IBoardDTORepository
    {
        private IMapper mapper;

        private readonly TrelloDbContext _trelloDbContext;
        private MapperConfiguration config;
        public bool deleted = false;


        private BoardDTO MapToBoardDTO(Board board)
        {
            config = new MapperConfiguration(cfg => cfg.CreateMap<Board, BoardDTO>());
            mapper = config.CreateMapper();
            BoardDTO boardDTO = mapper.Map<Board, BoardDTO>(board);
            return boardDTO;
        }

        private Board MapToBoard(BoardDTO boardDTO)
        {
            config = new MapperConfiguration(cfg => cfg.CreateMap<BoardDTO, Board>());
            mapper = config.CreateMapper();
            Board board = mapper.Map<BoardDTO, Board>(boardDTO);
            return board;
        }

        public SQLBoardRepository(TrelloDbContext trelloDbContext)
        {
            _trelloDbContext = trelloDbContext;
        }

        private BackgroundColor FindBgColor(Board board)
        {
            return _trelloDbContext.BackgroundColors.Find(board.CurrentBackgroundColorId);
        }

        public BoardBgDTO GetBoard(int id)
        {
            //should be caught System.InvalidOperationException
            Board board = _trelloDbContext.Boards.Where(b => b.BoardId == id)
                                                 .Include(b => b.BackgroundColor)
                                                 .FirstOrDefault(); 
            //BackgroundColor bgColor = FindBgColor(board);
            BoardBgDTO boardBgDTO = new BoardBgDTO();

            boardBgDTO.Id = board.BoardId;
            boardBgDTO.Title = board.Title;
            boardBgDTO.BgColorName = board.BackgroundColor.ColorName;
            boardBgDTO.BgColorHex = board.BackgroundColor.ColorHex;

            return boardBgDTO;
        }

        public List<BoardBgDTO> GetAllBoards()
        {
            IEnumerable<Board> boards = _trelloDbContext.Boards.Include(b => b.BackgroundColor)
                                                               .ToList();

            List<BoardBgDTO> boardsBgDTO = new List<BoardBgDTO>();
            

            foreach (Board board in boards)
            {
                BoardBgDTO boardBgDTO = new BoardBgDTO();
                
                boardBgDTO.Id = board.BoardId;
                boardBgDTO.Title = board.Title;
                boardBgDTO.BgColorName = board.BackgroundColor.ColorName;
                boardBgDTO.BgColorHex = board.BackgroundColor.ColorHex;

                boardsBgDTO.Add(boardBgDTO);
            }
            return boardsBgDTO;
        }

        public int Create(BoardDTO newBoardDTO)
        {
            
            Board newBoard = MapToBoard(newBoardDTO);
            _trelloDbContext.Boards.Add(newBoard);
            _trelloDbContext.SaveChanges();
                        
            return newBoard.BoardId;
        }

        public int Update(BoardDTO updatedBoardDTO)
        {
            Board boardToUpdate = _trelloDbContext.Boards.Find(updatedBoardDTO.BoardId);
            boardToUpdate.Title = updatedBoardDTO.Title;
            boardToUpdate.CurrentBackgroundColorId = updatedBoardDTO.CurrentBackgroundColorId;
            _trelloDbContext.SaveChanges();
            return updatedBoardDTO.BoardId;
            
        }

        public void Delete(int id)
        {
            Board boardToDelete = _trelloDbContext.Boards.Find(id);
            _trelloDbContext.Remove(boardToDelete);
            _trelloDbContext.SaveChanges();
            deleted = true;
        }
    }
}
