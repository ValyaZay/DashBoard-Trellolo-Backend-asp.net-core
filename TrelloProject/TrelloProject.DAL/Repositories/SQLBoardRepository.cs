using AutoMapper;
using System;
using System.Collections.Generic;
using TrelloProject.DAL.EF;
using TrelloProject.DAL.Entities;
using TrelloProject.BLL.Interfaces.RepositoriesInterfaces;
using TrelloProject.DTOsAndViewModels.DTOs;

namespace TrelloProject.DAL.Repositories
{
    internal class SQLBoardRepository : IBoardDTORepository
    {
        private IMapper mapper;

        private readonly TrelloDbContext _trelloDbContext;
        private MapperConfiguration config;

        

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
            BoardDTO boardDTO = MapToBoardDTO(board);
            return boardDTO;
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
    }
}
