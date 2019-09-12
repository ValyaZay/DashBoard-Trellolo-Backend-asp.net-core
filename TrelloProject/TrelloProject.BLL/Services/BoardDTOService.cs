using AutoMapper;
using System.Collections.Generic;
using TrelloProject.BLL.Interfaces.ServicesInterfaces;
using TrelloProject.BLL.Interfaces.RepositoriesInterfaces;
using TrelloProject.DTOsAndViewModels.DTOs;
using TrelloProject.DTOsAndViewModels.ViewModels;
using System;

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
            newBoardDTO.CurrentBackgroundColorId = (int)boardCreateViewModel.CurrentBackgroundColorId;

            int id = _boardRepository.Create(newBoardDTO);
            return id;
        }

        public void DeleteBoardDTO(int id)
        {
            _boardRepository.GetBoard(id);
            _boardRepository.Delete(id);
        }

        public List<BoardDTO> GetAllBoardsDTO()
        {
            return _boardRepository.GetAllBoards();
        }

        public BoardDTO GetBoardDTO(int Id)
        {
           return _boardRepository.GetBoard(Id);
            
        }

        public int UpdateBoardDTO(int id, BoardUpdateViewModel boardUpdateViewModel)
        {
            BoardDTO boardToUpdate = _boardRepository.GetBoard(id);
            boardToUpdate.Title = boardUpdateViewModel.Title;
            boardToUpdate.CurrentBackgroundColorId = (int)boardUpdateViewModel.CurrentBackgroundColorId;
            _boardRepository.Update(boardToUpdate);
            return boardToUpdate.BoardId;  
            
        }
    }
}
