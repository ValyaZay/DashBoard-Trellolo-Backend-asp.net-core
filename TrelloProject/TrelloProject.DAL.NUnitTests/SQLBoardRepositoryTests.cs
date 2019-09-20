using NUnit.Framework;
using System.Collections.Generic;
using TrelloProject.DTOsAndViewModels.DTOs;
using TrelloProject.DAL.Repositories;
using TrelloProject.DAL.EF;
using TrelloProject.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace TrelloProject.DAL.NUnitTests
{
    [TestFixture]
    class SQLBoardRepositoryTests
    {
        [Test]
        public void GetAllBoards_ByDefault_ReturnsListBoardDTO()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<TrelloDbContext>()
                .UseInMemoryDatabase(databaseName: "GetAllBoards_ByDefault_ReturnsListBoardDTO")
                .Options;

            using(var context = new TrelloDbContext(options))
            {
                context.Boards.Add(new Board { BoardId = 1, Title = "Test", CurrentBackgroundColorId = 2 });
                context.SaveChanges();
            }

            //Act
            
            using (var context = new TrelloDbContext(options))
            {
                var repository = new SQLBoardRepository(context);
                var result = repository.GetAllBoards();
            
                //Assert
                Assert.IsInstanceOf<List<BoardDTO>>(result);
            }

            
        }
    }
}
