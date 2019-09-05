using AutoMapper;

using System.Collections.Generic;

using TrelloProject.BLL.Interfaces.ServicesInterfaces;
using TrelloProject.BLL.Interfaces.RepositoriesInterfaces;
using TrelloProject.DTOs;

namespace TrelloProject.BLL.Services
{
    public class BoardDTOService : IBoardDTOService
    {
        private readonly IBoardDTORepository _boardRepository;
        
        public BoardDTOService(IBoardDTORepository boardRepository)
        {
            _boardRepository = boardRepository;
        }

        public List<BoardDTO> GetAllBoardsDTO()
        {
            return _boardRepository.GetAllBoards();
        }

        public BoardDTO GetBoardDTO(int Id)
        {
           return _boardRepository.GetBoard(Id);
            
        }
    }
}
