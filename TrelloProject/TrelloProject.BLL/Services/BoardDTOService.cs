using AutoMapper;
using System.Collections.Generic;
using TrelloProject.BLL.Interfaces.ServicesInterfaces;
using TrelloProject.BLL.Interfaces.RepositoriesInterfaces;
using TrelloProject.DTOsAndViewModels.DTOs;
using TrelloProject.DTOsAndViewModels.ViewModels;
using System;
using TrelloProject.DTOsAndViewModels.Exceptions;

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
            int createdBoardId;
            try
            {
                _backgroundColorDTORepository.DoesBackgroundColorExist(bgColorId);
                newBoardDTO.CurrentBackgroundColorId = bgColorId;
                createdBoardId = _boardRepository.Create(newBoardDTO);
            }
            catch (Exception innerEx)
            {
                throw new ApiException(400, innerEx, 1);
            }
            return createdBoardId;
        }

        public bool DeleteBoardDTO(int id)
        {
            BoardBgDTO boardDTO = _boardRepository.GetBoard(id);
            if (boardDTO == null)
            {
                throw new ApiException(404, 6);
            }

            else
            {
                try
                {
                    bool status = _boardRepository.Delete(id);
                    return status;
                }
                catch (Exception innerEx)
                {
                    throw new ApiException(400, innerEx, 6);
                }
                
            }
        }

        public List<BoardBgViewModel> GetAllBoards()
        {
            try
            {
                var boardsBgDTO = _boardRepository.GetAllBoards();

                List<BoardBgViewModel> boardBgViewModels = new List<BoardBgViewModel>();
                
                foreach (var boardBgDTO in boardsBgDTO)
                {
                    BoardBgViewModel boardBgViewModel = new BoardBgViewModel();
                    boardBgViewModel.Id = boardBgDTO.Id;
                    boardBgViewModel.Title = boardBgDTO.Title;
                    boardBgViewModel.BgColorId = boardBgDTO.BgColorId;
                    boardBgViewModel.BgColorName = boardBgDTO.BgColorName;
                    boardBgViewModel.BgColorHex = boardBgDTO.BgColorHex;

                    boardBgViewModels.Add(boardBgViewModel);
                }
                return boardBgViewModels;
            }
            catch(Exception innerEx)
            {
                throw new ApiException(400, innerEx, 10);
            }
         
        }

        public BoardBgViewModel GetBoard(int id)
        {

            try
            {
                BoardBgDTO boardBgDTO = _boardRepository.GetBoard(id);

                BoardBgViewModel boardBgViewModel = new BoardBgViewModel();

                boardBgViewModel.Id = boardBgDTO.Id;
                boardBgViewModel.Title = boardBgDTO.Title;
                boardBgViewModel.BgColorId = boardBgDTO.BgColorId;
                boardBgViewModel.BgColorName = boardBgDTO.BgColorName;
                boardBgViewModel.BgColorHex = boardBgDTO.BgColorHex;

                return boardBgViewModel;
            }
            catch (Exception innerEx)
            {
                throw new ApiException(404, innerEx, 6);
            }


        }

        public bool UpdateBoardDTO(BoardUpdateViewModel boardUpdateViewModel)
        {
            BoardDTO boardDTO = new BoardDTO();

            boardDTO.BoardId = boardUpdateViewModel.Id;
            boardDTO.Title = boardUpdateViewModel.Title;
           
            int bgColorId = (boardUpdateViewModel.CurrentBackgroundColorId != 0) ? boardUpdateViewModel.CurrentBackgroundColorId : 1;
            try
            {
                BoardBgDTO boardBgDTO = _boardRepository.GetBoard(boardUpdateViewModel.Id);

                _backgroundColorDTORepository.DoesBackgroundColorExist(bgColorId);
                boardDTO.CurrentBackgroundColorId = bgColorId;

                var status = _boardRepository.Update(boardDTO);
                return status;
            }
            
            catch (Exception innerEx)
            {
                throw new ApiException(400, innerEx, 4);
            }
            
        }
    }
}
