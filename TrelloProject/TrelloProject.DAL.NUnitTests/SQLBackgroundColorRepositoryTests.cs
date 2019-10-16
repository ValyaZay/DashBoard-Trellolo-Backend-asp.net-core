using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TrelloProject.DAL.EF;
using TrelloProject.DAL.Entities;
using TrelloProject.DAL.Repositories;
using TrelloProject.DTOsAndViewModels.Exceptions;

namespace TrelloProject.DAL.NUnitTests
{
    [TestFixture]
    class SQLBackgroundColorRepositoryTests
    {
        [Test]
        public void DoesBackgroundColorExist_BgExists_ReturnsTrue()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<TrelloDbContext>()
                .UseInMemoryDatabase(databaseName: "DoesBackgroundColorExist_BgExists_ReturnsTrue")
                .Options;

            BackgroundColor backgroundColor = new BackgroundColor() { BackgroundColorId = 1, ColorHex = "AnyHex" };

            using (var context = new TrelloDbContext(options))
            {
                context.BackgroundColors.Add(backgroundColor);
                context.SaveChanges();
            }
            
            //Act
            using (var context = new TrelloDbContext(options))
            {
                var repository = new SQLBackgroundColorRepository(context);
                var result = repository.DoesBackgroundColorExist(backgroundColor.BackgroundColorId);
                
                //Assert
                Assert.IsInstanceOf<bool>(result);
                Assert.That(result, Is.True);
                Assert.That(result, Is.Not.Null.Or.Empty);
            }
        }

        [Test]
        public void DoesBackgroundColorExist_BgDoesNOTExist_ReturnsFalse()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<TrelloDbContext>()
                .UseInMemoryDatabase(databaseName: "DoesBackgroundColorExist_BgDoesNOTExist_ReturnsFalse")
                .Options;

            string idDoesNotExistString = DateTime.Now.Ticks.ToString().Substring(0, 9);
            int idDoesNotExist = Convert.ToInt32(idDoesNotExistString);

            //Act
            using (var context = new TrelloDbContext(options))
            {
                var repository = new SQLBackgroundColorRepository(context);

                //Assert
                Assert.Throws<BgColorDoesNotExistException>(() => repository.DoesBackgroundColorExist(idDoesNotExist));
            }
        }
    }
}
