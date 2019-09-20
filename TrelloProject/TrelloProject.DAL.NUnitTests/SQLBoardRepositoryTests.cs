using NUnit.Framework;
using System.Collections.Generic;
using TrelloProject.DTOsAndViewModels.DTOs;
using TrelloProject.DAL.Repositories;
using TrelloProject.DAL.EF;
using TrelloProject.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;

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

        [Test]
        public void GetBoard_IfBoardExists_ReturnsBoardDTO()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<TrelloDbContext>()
                .UseInMemoryDatabase(databaseName: "GetBoard_IfBoardExists_ReturnsBoardDTO")
                .Options;

            Board board = new Board() { BoardId = 1, Title = "Test", CurrentBackgroundColorId = 2 };

            using (var context = new TrelloDbContext(options))
            {
                context.Boards.Add(board);
                context.SaveChanges();
            }

            //Act

            using (var context = new TrelloDbContext(options))
            {
                var repository = new SQLBoardRepository(context);
                var result = repository.GetBoard(board.BoardId);

                //Assert
                Assert.IsInstanceOf<BoardDTO>(result);
            }
        }

        [Test]
        public void GetBoard_IfBoardDoesNOTExist_ThrowsNullReferenceException()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<TrelloDbContext>()
                .UseInMemoryDatabase(databaseName: "GetBoard_IfBoardDoesNOTExist_ThrowsNullReferenceException")
                .Options;

            string idString = DateTime.Now.Ticks.ToString().Substring(0, 9);
            int id = Convert.ToInt32(idString);

            //Act
            using (var context = new TrelloDbContext(options))
            {
                var repository = new SQLBoardRepository(context);
                
                //Assert
                Assert.Throws<NullReferenceException>(() => repository.GetBoard(id));
            }
        }

        [Test]
        public void Create_ByDefault_ReturnsBoardId()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<TrelloDbContext>()
                .UseInMemoryDatabase(databaseName: "Create_ByDefault_ReturnsBoardId")
                .Options;

            Board newBoard = new Board() { Title = "Test", CurrentBackgroundColorId = 2 };

            string idString = DateTime.Now.Ticks.ToString().Substring(0, 9);
            int id = Convert.ToInt32(idString);

            //Act
            using (var context = new TrelloDbContext(options))
            {
                var repository = new SQLBoardRepository(context);
                context.Boards.Add(newBoard);
                context.SaveChanges();
                var result = newBoard.BoardId;

                //Assert
                Assert.IsInstanceOf<int>(result);
            }
        }
    }
}
