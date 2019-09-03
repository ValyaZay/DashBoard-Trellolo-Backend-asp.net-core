using NUnit.Framework;
using System.Collections.Generic;
using TrelloProject.BLL.DTO;
using TrelloProject.BLL.Services;
using TrelloProject.DAL.Entities;

namespace TrelloProject.BLL.Tests
{
    [TestFixture]
    class BoardDTOServiceTests
    {
        [Test]
        public void GetAllBoardsDTO_ByDefault_ReturnsListOfTypeBoardDTO()
        {
            //Arrange
            var stub = new BoardRepositoryStub();
            IEnumerable<Board> list = new List<Board> {
                            new Board { BoardId = 1, Title = "TestBoard1", CurrentBackgroundColorId = 1 },
                            new Board { BoardId = 2, Title = "TestBoard2", CurrentBackgroundColorId = 1 },
                            new Board { BoardId = 3, Title = "TestBoard3", CurrentBackgroundColorId = 1 }
                        };

            stub.SetReturnList(list);

            BoardDTOService boardDTOService = new BoardDTOService(stub);

            //Act
            var result = boardDTOService.GetAllBoardsDTO();

            //Assert
            Assert.IsInstanceOf<List<BoardDTO>>(result);

        }

        [Test]
        public void GetBoardDTO_ByDefault_ReturnsObjectOfTypeBoardDTO()
        {
            //Arrange
            var stub = new BoardRepositoryStub();
            Board board = new Board { BoardId = 1, Title = "TestBoard1", CurrentBackgroundColorId = 1 };

            stub.SetReturnObject(board);

            BoardDTOService boardDTOService = new BoardDTOService(stub);

            //Act
            var result = boardDTOService.GetBoardDTO(3);

            //Assert
            Assert.IsInstanceOf<BoardDTO>(result);
        }
    }
}
