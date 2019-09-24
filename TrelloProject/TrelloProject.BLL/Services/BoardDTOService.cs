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
        private readonly IBackgroundColorDTORepository _backgroundColorDTORepository;
        public bool deleted = false;
        
        public BoardDTOService(IBoardDTORepository boardRepository,
                               IBackgroundColorDTORepository backgroundColorDTORepository)
        {
            _boardRepository = boardRepository;
            _backgroundColorDTORepository = backgroundColorDTORepository;
        }

        public int CreateBoardDTO(BoardCreateViewModel boardCreateViewModel)
        {
            BoardDTO newBoardDTO = new BoardDTO();
            newBoardDTO.Title = boardCreateViewModel.Title;
            int bgColorId = (boardCreateViewModel.CurrentBackgroundColorId != 0) ? boardCreateViewModel.CurrentBackgroundColorId : 1;
            //check whether bg color exists
            BackgroundColorDTO backgroundColorDTO = _backgroundColorDTORepository.GetBackgroundById(bgColorId);
            if(backgroundColorDTO == null)
            {
                throw new NullReferenceException("The background color with ID=" + boardCreateViewModel.CurrentBackgroundColorId + " does not exist");
            }
            else
            {
                newBoardDTO.CurrentBackgroundColorId = (int)boardCreateViewModel.CurrentBackgroundColorId;
            }

            int id = _boardRepository.Create(newBoardDTO);
            return id;
        }

        public void DeleteBoardDTO(int id)
        {
            BoardDTO boardDTO = _boardRepository.GetBoard(id);
            if(boardDTO == null)
            {
                throw new NullReferenceException("The item with ID=" + id + " does not exist");
            }
            
            else
            {
                _boardRepository.Delete(id);
                deleted = true;
            }
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
