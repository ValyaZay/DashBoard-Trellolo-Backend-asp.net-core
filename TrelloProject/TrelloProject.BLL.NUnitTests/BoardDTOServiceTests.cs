using Moq;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using TrelloProject.BLL.Interfaces.RepositoriesInterfaces;
using TrelloProject.BLL.Services;
using TrelloProject.DTOsAndViewModels.DTOs;
using TrelloProject.DTOsAndViewModels.ViewModels;

namespace TrelloProject.BLL.Tests
{
    public class ListSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
                yield return new object[] { (new List<BoardDTO>()), 0 };
                yield return new object[] { (new List<BoardDTO>() { new BoardDTO { BoardId = 1, Title = "Test1", CurrentBackgroundColorId = 1 },
                                                                    new BoardDTO { BoardId = 2, Title = "Test2", CurrentBackgroundColorId = 2 }
                                                                    }), 2 };
            
        }
    }
    [TestFixture]
    class BoardDTOServiceTests
    {
        [Test]
        [TestCaseSource(typeof(ListSource))]
        public void GetAllBoardsDTO_ReturnsCorrectAmount(List<BoardDTO> list, int expectedCount)
        {
            //Arrange
            var mock = new Mock<IBoardDTORepository>();
            mock.Setup(a => a.GetAllBoards()).Returns(list);
            BoardDTOService boardDTOService = new BoardDTOService(mock.Object);

            //Act
            var count = list.Count;

            //Assert
            Assert.That(count, Is.EqualTo(expectedCount));
        }
            
        [Test]
        public void GetAllBoardsDTO_ByDefault_ReturnsListOfTypeBoardDTO()
        {
            //Arrange
            var mock = new Mock<IBoardDTORepository>();
            mock.Setup(a => a.GetAllBoards()).Returns(new List<BoardDTO>());
            BoardDTOService boardDTOService = new BoardDTOService(mock.Object);
            
            //Act
            var result = boardDTOService.GetAllBoardsDTO();
            

            //Assert
            Assert.IsInstanceOf<List<BoardDTO>>(result);
            Assert.That(result, Is.Not.Null);
            
        }
        
        [Test]
        public void GetBoardDTO_ByDefault_ReturnsObjectOfTypeBoardDTO()
        {
            //Arrange
            var mock = new Mock<IBoardDTORepository>();

            string idString = DateTime.Now.Ticks.ToString().Substring(0, 9);
            int id = Convert.ToInt32(idString);

            mock.Setup(a => a.GetBoard(id)).Returns(new BoardDTO());
            BoardDTOService boardDTOService = new BoardDTOService(mock.Object);
            
            //Act
            var result = boardDTOService.GetBoardDTO(id);

            //Assert
            Assert.IsInstanceOf<BoardDTO>(result);
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void CreateBoardDTO_ByDefault_ReturnsId()
        {
            // Arrange
            var mock = new Mock<IBoardDTORepository>();
            BoardDTO boardDTO = new BoardDTO();

            string idString = DateTime.Now.Ticks.ToString().Substring(0, 9);
            int id = Convert.ToInt32(idString);

            mock.Setup(a => a.Create(boardDTO)).Returns(id);
            BoardDTOService boardDTOService = new BoardDTOService(mock.Object);
            BoardCreateViewModel boardCreateViewModel = new BoardCreateViewModel();

            //Act
            var result = boardDTOService.CreateBoardDTO(boardCreateViewModel);
            
            //Assert
            Assert.IsInstanceOf<int>(result);
            Assert.That(result, Is.Not.Null);

        }

        [Test]
        public void UpdateBoardDTO_ByDefault_ReturnsId()
        {
            // Arrange
            var mock = new Mock<IBoardDTORepository>();
            BoardDTO boardDTO = new BoardDTO();

            string idString = DateTime.Now.Ticks.ToString().Substring(0, 9);
            int id = Convert.ToInt32(idString);

            mock.Setup(a => a.Update(boardDTO)).Returns(id);
            mock.Setup(a => a.GetBoard(id)).Returns(new BoardDTO());
            BoardDTOService boardDTOService = new BoardDTOService(mock.Object);
            BoardUpdateViewModel boardUpdateViewModel = new BoardUpdateViewModel();
            
            //Act
            var result = boardDTOService.UpdateBoardDTO(id, boardUpdateViewModel);

            //Assert
            Assert.IsInstanceOf<int>(result);
            Assert.That(result, Is.Not.Null);
        }

        
        [Test]
        public void DeleteBoardDTO_BoardDoesNotExist_ThrowsNullReferenceException()
        {
            // Arrange
            var mock = new Mock<IBoardDTORepository>();
            BoardDTO boardDTO = new BoardDTO();

            string idString = DateTime.Now.Ticks.ToString().Substring(0, 9);
            int id = Convert.ToInt32(idString);

            NullReferenceException ex = new NullReferenceException();
            mock.Setup(a => a.Delete(id));
            mock.Setup(a => a.GetBoard(id));
            
            BoardDTOService boardDTOService = new BoardDTOService(mock.Object);
            
            //Act
           

            //Assert
            Assert.Throws<NullReferenceException>(() => boardDTOService.DeleteBoardDTO(id));
        }

        [Test]
        public void DeleteBoardDTO_BoardExists_CallsDeleteMethodOfRepository()
        {
            //Arrange
            var mock = new Mock<IBoardDTORepository>();
            BoardDTO boardDTO = new BoardDTO();

            string idString = DateTime.Now.Ticks.ToString().Substring(0, 9);
            int id = Convert.ToInt32(idString);

            mock.Setup(a => a.Delete(id));
            mock.Setup(a => a.GetBoard(id)).Returns(new BoardDTO());
            BoardDTOService boardDTOService = new BoardDTOService(mock.Object);

            //Act
            boardDTOService.DeleteBoardDTO(id);
           
            //Assert
            Assert.That(boardDTOService.deleted, Is.True);

        }
    }
}
