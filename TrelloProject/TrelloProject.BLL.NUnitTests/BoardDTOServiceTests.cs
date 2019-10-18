using Moq;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using TrelloProject.BLL.Interfaces.RepositoriesInterfaces;
using TrelloProject.BLL.Services;
using TrelloProject.DTOsAndViewModels.DTOs;
using TrelloProject.DTOsAndViewModels.Exceptions;
using TrelloProject.DTOsAndViewModels.ViewModels;
using static TrelloProject.WEB.Contracts.V1.ApiRoutes;

namespace TrelloProject.BLL.Tests
{
    public class ListSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
                yield return new object[] { (new List<BoardBgDTO>()), 0 };
                yield return new object[] { (new List<BoardBgDTO>() { new BoardBgDTO { Id = 1, Title = "Test1", BgColorId = 1 },
                                                                            new BoardBgDTO { Id = 2, Title = "Test2", BgColorId = 2 }
                                                                    }), 2 };
            
        }
    }
    [TestFixture]
    class BoardDTOServiceTests
    {
        [Test]
        
        [TestCaseSource(typeof(ListSource))]
        public void GetAllBoards_ReturnsCorrectAmount(List<BoardBgDTO> list, int expectedCount)
        {
            //Arrange
            var mock = new Mock<IBoardDTORepository>();
            var mockBg = new Mock<IBackgroundColorDTORepository>();
            mock.Setup(a => a.GetAllBoards()).Returns(list);
            BoardDTOService boardDTOService = new BoardDTOService(mock.Object, mockBg.Object);

            //Act
            var count = list.Count;

            //Assert
            Assert.That(count, Is.EqualTo(expectedCount));
        }
            
        [Test]
        public void GetAllBoardsDTO_ByDefault_ReturnsListOfTypeBoardViewModel()
        {
            //Arrange
            var mock = new Mock<IBoardDTORepository>();
            var mockBg = new Mock<IBackgroundColorDTORepository>();
            mock.Setup(a => a.GetAllBoards()).Returns(new List<BoardBgDTO>());
            BoardDTOService boardDTOService = new BoardDTOService(mock.Object, mockBg.Object);
            
            //Act
            var result = boardDTOService.GetAllBoards();
            

            //Assert
            Assert.IsInstanceOf<List<BoardBgViewModel>>(result);
            Assert.That(result, Is.Not.Null);
            
        }
        
        [Test]
        
        public void GetBoard_ByDefault_ReturnsObjectOfTypeBoardBgViewModel()
        {
            //Arrange
            var mock = new Mock<IBoardDTORepository>();
            var mockBg = new Mock<IBackgroundColorDTORepository>();
            string idString = DateTime.Now.Ticks.ToString().Substring(0, 9);
            int id = Convert.ToInt32(idString);

            mock.Setup(a => a.GetBoard(id)).Returns(new BoardBgDTO());
            BoardDTOService boardDTOService = new BoardDTOService(mock.Object, mockBg.Object);
            
            //Act
            var result = boardDTOService.GetBoard(id);

            //Assert
            Assert.IsInstanceOf<BoardBgViewModel>(result);
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        
        public void CreateBoardDTO_BgColorExistsAndTitleIsUnique_ReturnsId()
        {
            // Arrange
            var mock = new Mock<IBoardDTORepository>();
            var mockBg = new Mock<IBackgroundColorDTORepository>();

            string idString = DateTime.Now.Ticks.ToString().Substring(0, 9);
            int id = Convert.ToInt32(idString);

            BoardDTO boardDTO = new BoardDTO();
            BoardCreateViewModel boardCreateViewModel = new BoardCreateViewModel();
            

            mock.Setup(a => a.Create(boardDTO))
                .Returns(id);

            mockBg
                .Setup(a => a.DoesBackgroundColorExist(boardCreateViewModel.CurrentBackgroundColorId))
                .Returns(true);
            BoardDTOService boardDTOService = new BoardDTOService(mock.Object, mockBg.Object);

            
            //Act
            
            var result = boardDTOService.CreateBoardDTO(boardCreateViewModel);
            
            
            //Assert
            Assert.IsInstanceOf<int>(result);
            Assert.That(result, Is.Not.Null);

        }

        [Test]
        public void CreateBoardDTO_BgColorDoesNOTExist_BoardIsNotCreated()
        {
            // Arrange
            var mock = new Mock<IBoardDTORepository>();
            var mockBg = new Mock<IBackgroundColorDTORepository>();
            BoardDTO boardDTO = new BoardDTO();
            BoardCreateViewModel boardCreateViewModel = new BoardCreateViewModel() ;
            
            mock
                .Setup(a => a.Create(boardDTO))
                .Returns(It.IsAny<int>);

            mockBg
                .Setup(a => a.DoesBackgroundColorExist(It.IsAny<int>()))
                .Returns(null);
            BoardDTOService boardDTOService = new BoardDTOService(mock.Object, mockBg.Object);


            //Act

            //Assert
            Assert.Throws<BoardIsNotCreated>(() => boardDTOService.CreateBoardDTO(boardCreateViewModel));

        }

        [Test]
        
        public void UpdateBoardDTO_ByDefault_ReturnsId()
        {
            // Arrange
            var mock = new Mock<IBoardDTORepository>();
            var mockBg = new Mock<IBackgroundColorDTORepository>();
            BoardDTO boardDTO = new BoardDTO();

            string idString = DateTime.Now.Ticks.ToString().Substring(0, 9);
            int id = Convert.ToInt32(idString);

            mock.Setup(a => a.Update(boardDTO)).Returns(true);
            mock.Setup(a => a.GetBoard(id)).Returns(new BoardBgDTO());
            BoardDTOService boardDTOService = new BoardDTOService(mock.Object, mockBg.Object);
            BoardUpdateViewModel boardUpdateViewModel = new BoardUpdateViewModel();
            
            //Act
            var result = boardDTOService.UpdateBoardDTO(boardUpdateViewModel);

            //Assert
            Assert.IsInstanceOf<int>(result);
            Assert.That(result, Is.Not.Null);
        }

        
        [Test]
        public void DeleteBoardDTO_BoardDoesNotExist_ThrowsNullReferenceException()
        {
            // Arrange
            var mock = new Mock<IBoardDTORepository>();
            var mockBg = new Mock<IBackgroundColorDTORepository>();
            BoardDTO boardDTO = new BoardDTO();

            string idString = DateTime.Now.Ticks.ToString().Substring(0, 9);
            int id = Convert.ToInt32(idString);

            mock.Setup(a => a.Delete(id));
            mock.Setup(a => a.GetBoard(id));
            
            BoardDTOService boardDTOService = new BoardDTOService(mock.Object, mockBg.Object);
            
            //Act
           

            //Assert
            Assert.Throws<NullReferenceException>(() => boardDTOService.DeleteBoardDTO(id));
        }

        [Test]
        
        public void DeleteBoardDTO_BoardExists_ReturnsTrue()
        {
            //Arrange
            var mock = new Mock<IBoardDTORepository>();
            var mockBg = new Mock<IBackgroundColorDTORepository>();
            BoardDTO boardDTO = new BoardDTO();

            string idString = DateTime.Now.Ticks.ToString().Substring(0, 9);
            int id = Convert.ToInt32(idString);

            mock.Setup(a => a.Delete(id)).Returns(true);
            mock.Setup(a => a.GetBoard(id)).Returns(new BoardBgDTO());
            //mock.Setup(a => a.GetBoard(id)).Returns(new BoardDTO());
            BoardDTOService boardDTOService = new BoardDTOService(mock.Object, mockBg.Object);

            //Act
            var result = boardDTOService.DeleteBoardDTO(id);
           
            //Assert
            Assert.That(result, Is.True);

        }
    }
}
