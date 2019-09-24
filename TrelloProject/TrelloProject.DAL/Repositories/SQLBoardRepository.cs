using AutoMapper;
using System;
using System.Collections.Generic;
using TrelloProject.DAL.EF;
using TrelloProject.DAL.Entities;
using TrelloProject.BLL.Interfaces.RepositoriesInterfaces;
using TrelloProject.DTOsAndViewModels.DTOs;
using System.Linq;

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

        public BoardDTO GetBoard(int id)
        {
            Board board = _trelloDbContext.Boards.Find(id);
            if(board == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                BoardDTO boardDTO = MapToBoardDTO(board);
                return boardDTO;
            } 
        }

        public List<BoardDTO> GetAllBoards()
        {
            IEnumerable<Board> boards = _trelloDbContext.Boards;

            List<BoardDTO> boardsDTO = new List<BoardDTO>();

            foreach (Board board in boards)
            {
                BoardDTO boardDTO = MapToBoardDTO(board);
                boardsDTO.Add(boardDTO);
            }
            return boardsDTO;
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
