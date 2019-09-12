using System;
using System.Collections.Generic;
using System.Text;
using TrelloProject.BLL.DTO;
using TrelloProject.BLL.Interfaces;

namespace TrelloProject.WEB.Tests
{
    class BoardDTOServiceStub : IBoardDTOService
    {
        private List<BoardDTO> _list;
        private BoardDTO _boardDTO;

        public List<BoardDTO> GetAllBoardsDTO()
        {
            return _list;
        }


        public void SetReturnList(List<BoardDTO> list)
        {
            _list = list;
        }

        public BoardDTO GetBoardDTO(int Id)
        {
            return _boardDTO;
        }

        public void SetReturnObject(BoardDTO boardDTO)
        {
            _boardDTO = boardDTO;
        }
    }
}
