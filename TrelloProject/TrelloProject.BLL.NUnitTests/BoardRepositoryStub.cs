using System.Collections.Generic;
using TrelloProject.BLL.Interfaces.RepositoriesInterfaces;
using TrelloProject.DTOs;

namespace TrelloProject.BLL.Tests
{
    
    class BoardRepositoryStub : IBoardDTORepository
    {
        private List<BoardDTO> _list;
        private BoardDTO _board;

        public void SetReturnList(List<BoardDTO> list)
        {
            _list = list;
        }

        public List<BoardDTO> GetAllBoards()
        {
            return _list;
        }

        public BoardDTO GetBoard(int Id)
        {
            return _board;
        }

        public void SetReturnObject(BoardDTO board)
        {
            _board = board;
        }
    }
}
