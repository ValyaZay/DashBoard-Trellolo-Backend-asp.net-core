using System;
using System.Collections.Generic;
using System.Text;
using TrelloProject.DAL.EF;
using TrelloProject.DAL.Entities;
using TrelloProject.DAL.Interfaces;

namespace TrelloProject.DAL.Repositories
{
    public class SQLBoardRepository : IBoardRepository
    {
        private readonly TrelloDbContext _trelloDbContext;

        public SQLBoardRepository(TrelloDbContext trelloDbContext)
        {
            _trelloDbContext = trelloDbContext;
        }

        public Board GetBoard(int id)
        {
            return _trelloDbContext.Boards.Find(id);
        }

        public IEnumerable<Board> GetAllBoards()
        {
            return _trelloDbContext.Boards;
        }

        //public int Create(Board board)
        //{
        //    _trelloDbContext.Boards.Add(board);
        //    _trelloDbContext.SaveChanges();
        //    return board;
        //}
        //public Board Update(Board boardChanges)
        //{
        //    var board = _trelloDbContext.Boards.Attach(boardChanges);
        //    board.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        //    _trelloDbContext.SaveChanges();
        //    return boardChanges;
        //}
        //public Board Delete(int id)
        //{
        //    Board board = _trelloDbContext.Boards.Find(id);
        //    if(board != null)
        //    {
        //        _trelloDbContext.Boards.Remove(board);
        //        _trelloDbContext.SaveChanges();
        //    }
        //    return board;
        //}
    }
}
