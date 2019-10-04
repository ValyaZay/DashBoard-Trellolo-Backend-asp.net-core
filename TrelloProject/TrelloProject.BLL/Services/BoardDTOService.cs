﻿using AutoMapper;
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
            bool bgExists = _backgroundColorDTORepository.DoesBackgroundColorExist(bgColorId);
            if(bgExists == false)
            {
                throw new NullReferenceException("The background color with ID=" + boardCreateViewModel.CurrentBackgroundColorId + " does not exist");
            }
            else
            {
                newBoardDTO.CurrentBackgroundColorId = bgColorId;
            }

            int id = _boardRepository.Create(newBoardDTO);
            return id;
            
        }

        public void DeleteBoardDTO(int id)
        {
            //BoardDTO boardDTO = _boardRepository.GetBoard(id);
            //if(boardDTO == null)
            //{
            //    throw new NullReferenceException("The item with ID=" + id + " does not exist");
            //}
            
            //else
            //{
            //    _boardRepository.Delete(id);
            //    deleted = true;
            //}
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
                    boardBgViewModel.BgColorName = boardBgDTO.BgColorName;
                    boardBgViewModel.BgColorHex = boardBgDTO.BgColorHex;

                    boardBgViewModels.Add(boardBgViewModel);
                }
                return boardBgViewModels;
            }
            catch(Exception innerEx)
            {
                throw new Exception("err", innerEx); // custom exception should be thrown
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
                boardBgViewModel.BgColorName = boardBgDTO.BgColorName;
                boardBgViewModel.BgColorHex = boardBgDTO.BgColorHex;

                return boardBgViewModel;
            }
            catch(Exception innerEx)
            {
                throw new Exception("The board with ID = " + id + " does not exist", innerEx); // custom exception should be thrown
            }
            
            
        }

        public int UpdateBoardDTO(int id, BoardUpdateViewModel boardUpdateViewModel)
        {
            //BoardBgDTO boardToUpdate = _boardRepository.GetBoard(id);
            //boardToUpdate.Title = boardUpdateViewModel.Title;
            //boardToUpdate.CurrentBackgroundColorId = (int)boardUpdateViewModel.CurrentBackgroundColorId;
            //_boardRepository.Update(boardToUpdate);
            //return boardToUpdate.BoardId;
            return 2;
        }
    }
}
