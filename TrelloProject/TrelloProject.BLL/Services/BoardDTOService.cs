using AutoMapper;
using System.Collections.Generic;
using TrelloProject.BLL.Interfaces.ServicesInterfaces;
using TrelloProject.BLL.Interfaces.RepositoriesInterfaces;
using TrelloProject.DTOsAndViewModels.DTOs;
using TrelloProject.DTOsAndViewModels.ViewModels;

namespace TrelloProject.BLL.Services
{
    public class BoardDTOService : IBoardDTOService
    {
        private readonly IBoardDTORepository _boardRepository;
        
        public BoardDTOService(IBoardDTORepository boardRepository)
        {
            _boardRepository = boardRepository;
        }

        public int CreateBoardDTO(BoardCreateViewModel boardCreateViewModel)
        {
            BoardDTO newBoardDTO = new BoardDTO();
            newBoardDTO.Title = boardCreateViewModel.Title;
            newBoardDTO.CurrentBackgroundColorId = (int)boardCreateViewModel.BgColor;

            int id = _boardRepository.Create(newBoardDTO);
            return id;
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
