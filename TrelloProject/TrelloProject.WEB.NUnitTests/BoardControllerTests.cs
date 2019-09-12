using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Collections.Generic;
using TrelloProject.BLL.DTO;
using TrelloProject.WEB.Controllers;

namespace TrelloProject.WEB.Tests
{
    [TestFixture]
    class BoardControllerTests
    {
        [Test]
        public void Get_BoardsCountIsZero_ReturnsNotFoundObjectResult()
        {
            // Arrange
            var stub = new BoardDTOServiceStub();
            stub.SetReturnList(new List<BoardDTO>());

            BoardController bc = new BoardController(stub);

            // Act
            NotFoundObjectResult result = bc.Get() as NotFoundObjectResult;

            //Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public void Get_BoardsCountIsNotZero_ReturnsOkObjectResult()
        {
            // Arrange
            var stub = new BoardDTOServiceStub();
            stub.SetReturnList(new List<BoardDTO> {
                new BoardDTO { BoardId = 1, Title = "TestBoard1", CurrentBackgroundColorId = 1 },
                new BoardDTO { BoardId = 2, Title = "TestBoard2", CurrentBackgroundColorId = 1 },
                new BoardDTO { BoardId = 3, Title = "TestBoard3", CurrentBackgroundColorId = 1 }
            });

            BoardController bc = new BoardController(stub);

            // Act
            OkObjectResult result = bc.Get() as OkObjectResult;

            //Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public void GetById_BoardDoesNotExist_ReturnsNotFoundObjectResult()
        {
            // Arrange
            var stub = new BoardDTOServiceStub();
            BoardDTO boardDTO = null;
            stub.SetReturnObject(boardDTO);

            BoardController bc = new BoardController(stub);

            //Act
            NotFoundObjectResult result = bc.GetById(2) as NotFoundObjectResult;

            //Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public void GetById_BoardExists_ReturnsOkObjectResult()
        {
            // Arrange
            var stub = new BoardDTOServiceStub();
            BoardDTO boardDTO = new BoardDTO { BoardId = 1, Title = "TestBoard1", CurrentBackgroundColorId = 1 };
            stub.SetReturnObject(boardDTO);

            BoardController bc = new BoardController(stub);

            //Act
            OkObjectResult result = bc.GetById(2) as OkObjectResult;

            //Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
    }
}
