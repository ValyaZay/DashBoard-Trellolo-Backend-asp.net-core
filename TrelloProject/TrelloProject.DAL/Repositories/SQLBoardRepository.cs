﻿using AutoMapper;
using System;
using System.Collections.Generic;
using TrelloProject.DAL.EF;
using TrelloProject.DAL.Entities;
using TrelloProject.BLL.Interfaces.RepositoriesInterfaces;
using TrelloProject.DTOsAndViewModels.DTOs;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TrelloProject.DTOsAndViewModels.Exceptions;

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

        private BackgroundColor FindBgColor(Board board)
        {
            return _trelloDbContext.BackgroundColors.Find(board.CurrentBackgroundColorId);
        }

        public BoardBgDTO GetBoard(int id)
        {
            try
            {
                //should be caught System.InvalidOperationException
                Board board = _trelloDbContext.Boards.Where(b => b.BoardId == id)
                                                     .Include(b => b.BackgroundColor)
                                                     .AsNoTracking()
                                                     .FirstOrDefault();
                //BackgroundColor bgColor = FindBgColor(board);
                BoardBgDTO boardBgDTO = new BoardBgDTO();

                boardBgDTO.Id = board.BoardId;
                boardBgDTO.Title = board.Title;
                boardBgDTO.BgColorId = board.BackgroundColor.BackgroundColorId;
                boardBgDTO.BgColorName = board.BackgroundColor.ColorName;
                boardBgDTO.BgColorHex = board.BackgroundColor.ColorHex;
                return boardBgDTO;
            }
            catch (Exception e)
            {
                throw new BoardDoesNotExistException(e.Message);//throw custom repo-ex
            }

            
        }

        public List<BoardBgDTO> GetAllBoards()
        {
            IEnumerable<Board> boards = _trelloDbContext.Boards.Include(b => b.BackgroundColor)
                                                               .AsNoTracking()
                                                               .ToList();

            List<BoardBgDTO> boardsBgDTO = new List<BoardBgDTO>();
            

            foreach (Board board in boards)
            {
                BoardBgDTO boardBgDTO = new BoardBgDTO();
                
                boardBgDTO.Id = board.BoardId;
                boardBgDTO.Title = board.Title;
                boardBgDTO.BgColorId = board.BackgroundColor.BackgroundColorId;
                boardBgDTO.BgColorName = board.BackgroundColor.ColorName;
                boardBgDTO.BgColorHex = board.BackgroundColor.ColorHex;

                boardsBgDTO.Add(boardBgDTO);
            }
            return boardsBgDTO;
        }

        public int Create(BoardDTO newBoardDTO)
        {
            Board board = new Board();
            board.Title = newBoardDTO.Title;
            board.CurrentBackgroundColorId = newBoardDTO.CurrentBackgroundColorId;
            _trelloDbContext.Boards.Add(board);
            try
            {
                _trelloDbContext.SaveChanges();
            }
            
            catch (Exception)
            {
                throw new BoardTitleAlreadyExists();//throw custom repo-ex
            }
            return board.BoardId;
        }

        public bool Update(BoardDTO updatedBoardDTO)
        {
            Board board = new Board();
            board.BoardId = updatedBoardDTO.BoardId;
            board.Title = updatedBoardDTO.Title;
            board.CurrentBackgroundColorId = updatedBoardDTO.CurrentBackgroundColorId;

            _trelloDbContext.Boards.Attach(board);
            _trelloDbContext.Entry(board).State = EntityState.Modified;

            try
            {
                return (_trelloDbContext.SaveChanges() > 0 ? true : false);
            }
            catch (Exception)
            {
                throw new BoardTitleAlreadyExists();//throw custom repo-ex
            }
        }

        public void Delete(int id)
        {
            Board boardToDelete = _trelloDbContext.Boards.Find(id);
            _trelloDbContext.Remove(boardToDelete);
            _trelloDbContext.SaveChanges();
            
        }
    }
}
