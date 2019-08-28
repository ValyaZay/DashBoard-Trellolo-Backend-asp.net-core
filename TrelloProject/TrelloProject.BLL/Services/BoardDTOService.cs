using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TrelloProject.BLL.DTO;
using TrelloProject.BLL.Interfaces;
using TrelloProject.DAL.Entities;
using TrelloProject.DAL.Interfaces;

namespace TrelloProject.BLL.Services
{
    public class BoardDTOService : IBoardDTOService
    {
        private readonly IBoardRepository _boardRepository;
        private IMapper mapper;
        private MapperConfiguration config;

        private BoardDTO MapBoardDTO (Board board)
        {
            config = new MapperConfiguration(cfg => cfg.CreateMap<Board, BoardDTO>());
            mapper = config.CreateMapper();
            BoardDTO boardDTO = mapper.Map<Board, BoardDTO>(board);
            return boardDTO;
        }


        public BoardDTOService(IBoardRepository boardRepository)
        {
            _boardRepository = boardRepository;
        }
        public List<BoardDTO> GetAllBoardsDTO()
        {
            IEnumerable<Board> boards = _boardRepository.GetAllBoards();
            List<BoardDTO> boardsDTO = new List<BoardDTO>();

            foreach (Board board in boards)
            {
                BoardDTO boardDTO = MapBoardDTO(board);
                boardsDTO.Add(boardDTO);
            }

            return boardsDTO;
        }

        public BoardDTO GetBoardDTO(int Id)
        {
            Board board = _boardRepository.GetBoard(Id);
            BoardDTO boardDTO = MapBoardDTO(board);

            return boardDTO;
        }
    }
}
