using System.Collections.Generic;
using TrelloProject.DAL.Entities;
using TrelloProject.DAL.Interfaces;

namespace TrelloProject.BLL.Tests
{
    class BoardRepositoryStub : IBoardRepository
    {
        private IEnumerable<Board> _list;
        private Board _board;

        public void SetReturnList(IEnumerable<Board> list)
        {
            _list = list;
        }

        public IEnumerable<Board> GetAllBoards()
        {
            return _list;
        }

        public Board GetBoard(int Id)
        {
            return _board;
        }

        public void SetReturnObject(Board board)
        {
            _board = board;
        }
    }
}
