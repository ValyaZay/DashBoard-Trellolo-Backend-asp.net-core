using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TrelloProject.DAL.EF;
using TrelloProject.DAL.Entities;
using TrelloProject.BLL.Interfaces.RepositoriesInterfaces;
using TrelloProject.DTOs;

namespace TrelloProject.DAL.Repositories
{
    internal class SQLBoardRepository : IBoardDTORepository
    {
        private IMapper mapper;

        private readonly TrelloDbContext _trelloDbContext;
        private MapperConfiguration config;

        

        private BoardDTO MapBoardDTO(Board board)
        {
            config = new MapperConfiguration(cfg => cfg.CreateMap<Board, BoardDTO>());
            mapper = config.CreateMapper();
            BoardDTO boardDTO = mapper.Map<Board, BoardDTO>(board);
            return boardDTO;
        }

        public SQLBoardRepository(TrelloDbContext trelloDbContext)
        {
            _trelloDbContext = trelloDbContext;
        }

        public BoardDTO GetBoard(int id)
        {
            Board board = _trelloDbContext.Boards.Find(id);
            BoardDTO boardDTO = MapBoardDTO(board);
            return boardDTO;
        }

        public List<BoardDTO> GetAllBoards()
        {
            IEnumerable<Board> boards = _trelloDbContext.Boards;
            List<BoardDTO> boardsDTO = new List<BoardDTO>();

            foreach (Board board in boards)
            {
                BoardDTO boardDTO = MapBoardDTO(board);
                boardsDTO.Add(boardDTO);
            }
            return boardsDTO;
        }

        
    }
}
