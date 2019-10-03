using Moq;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TrelloProject.BLL.Interfaces.RepositoriesInterfaces;
using TrelloProject.BLL.Services;
using TrelloProject.DTOsAndViewModels.DTOs;
using TrelloProject.DTOsAndViewModels.ViewModels;

namespace TrelloProject.BLL.NUnitTests
{
    public class ListSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new object[] { (new List<BackgroundColorDTO>()), 0 };
            yield return new object[] { (new List<BackgroundColorDTO>() { new BackgroundColorDTO { ColorHex = "hex1", ColorName = "name1" },
                                                                          new BackgroundColorDTO { ColorHex = "hex2", ColorName = "name2" }
                                                                    }), 2 };

        }
    }
    [TestFixture]
    public class BackgroundColorDTOServiceTests  
    {
        [Test]
        public void GetAllBgColors_ByDefault_ReturnsListOfTypeBgColorViewModel()
        {
            //Arrange
            var mock = new Mock<IBackgroundColorDTORepository>();
            mock.Setup(a => a.GetAllBgColors()).Returns(new List<BackgroundColorDTO>()); 
            BackgroundColorDTOService backgroundColorDTOService = new BackgroundColorDTOService(mock.Object);

            //Act
            var result = backgroundColorDTOService.GetAllBgColors();

            //Assert
            Assert.IsInstanceOf<List<BgColorViewModel>>(result);
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        [TestCaseSource(typeof(ListSource))]
        public void GetAllBgColors_ReturnsCorrectAmount(List<BackgroundColorDTO> list, int expectedCount)
        {
            //Arrange
            var mock = new Mock<IBackgroundColorDTORepository>();
            mock.Setup(a => a.GetAllBgColors()).Returns(list);
            BackgroundColorDTOService backgroundColorDTOService = new BackgroundColorDTOService(mock.Object);

            //Act
            var count = list.Count;

            //Assert
            Assert.That(count, Is.EqualTo(expectedCount));
            Assert.That(count, Is.Not.Null);
        }
    }
}
