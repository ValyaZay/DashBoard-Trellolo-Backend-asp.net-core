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
        public void GetAllBoards_IfRepositoryIsNOTEmpty_ReturnsListBoardDTOAndCorrectCountOfCollection()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<TrelloDbContext>()
                .UseInMemoryDatabase(databaseName: "GetAllBoards_ByDefault_ReturnsListBoardDTO")
                .Options;

            using (var context = new TrelloDbContext(options))
            {
                context.Boards.Add(new Board { BoardId = 1, Title = "Test", CurrentBackgroundColorId = 2 });
                context.SaveChanges();
            }

            //Act
            using (var context = new TrelloDbContext(options))
            {
                var repository = new SQLBoardRepository(context);
                var result = repository.GetAllBoards();
                var countOfBoardsInRepository = repository.GetAllBoards().Count;

                //Assert
                Assert.IsInstanceOf<List<BoardDTO>>(result);
                Assert.That(result, Is.Not.Null.Or.Empty);
                Assert.That(countOfBoardsInRepository, Is.EqualTo(1));
            }
        }

        public void GetAllBoards_IfRepositoryIsEmpty_ReturnsZeroCount()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<TrelloDbContext>()
                .UseInMemoryDatabase(databaseName: "GetAllBoards_ByDefault_ReturnsListBoardDTO")
                .Options;

            //Act
            using (var context = new TrelloDbContext(options))
            {
                var repository = new SQLBoardRepository(context);
                var result = repository.GetAllBoards();
                var countOfBoardsInRepository = repository.GetAllBoards().Count;

                //Assert
                Assert.IsInstanceOf<List<BoardDTO>>(result);
                Assert.That(result, Is.Null.Or.Empty);
                Assert.That(countOfBoardsInRepository, Is.EqualTo(0));
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
                Assert.That(result, Is.Not.Null);
                Assert.That(result.BoardId, Is.EqualTo(board.BoardId));
                Assert.That(result.Title, Is.EqualTo(board.Title));
                Assert.That(result.CurrentBackgroundColorId, Is.EqualTo(board.CurrentBackgroundColorId));
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

            //Act
            using (var context = new TrelloDbContext(options))
            {
                var repository = new SQLBoardRepository(context);
                context.Boards.Add(newBoard);
                context.SaveChanges();

                var createdBoardId = newBoard.BoardId;
                var createdBoard = repository.GetBoard(createdBoardId);

                //Assert
                Assert.IsInstanceOf<int>(createdBoardId);
                Assert.That(createdBoardId, Is.Not.Null);
                Assert.That(createdBoard.BoardId, Is.EqualTo(newBoard.BoardId));
                Assert.That(createdBoard.Title, Is.EqualTo(newBoard.Title));
                Assert.That(createdBoard.CurrentBackgroundColorId, Is.EqualTo(newBoard.CurrentBackgroundColorId));
            }
        }

        [Test]
        public void Update_ByDefault_ReturnsBoardId()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<TrelloDbContext>()
                .UseInMemoryDatabase(databaseName: "Update_ByDefault_ReturnsBoardId")
                .Options;

            Board newBoard = new Board() { Title = "Test", CurrentBackgroundColorId = 2 };
            Board boardToUpdate = new Board() { BoardId = newBoard.BoardId, Title = "TestUpdated", CurrentBackgroundColorId = 2 };

            using (var context = new TrelloDbContext(options))
            {
                var repository = new SQLBoardRepository(context);
                context.Boards.Add(newBoard);
                context.SaveChanges();

            }

            //Act
            using (var context = new TrelloDbContext(options))
            {
                var repository = new SQLBoardRepository(context);
                context.Boards.Update(boardToUpdate);
                context.SaveChanges();
                var updatedBoardId = boardToUpdate.BoardId;
                var updatedBoard = repository.GetBoard(updatedBoardId);

                //Assert
                Assert.IsInstanceOf<int>(updatedBoardId);
                Assert.That(updatedBoardId, Is.Not.Null);
                Assert.That(updatedBoard.BoardId, Is.EqualTo(boardToUpdate.BoardId));
                Assert.That(updatedBoard.Title, Is.EqualTo(boardToUpdate.Title));
                Assert.That(updatedBoard.CurrentBackgroundColorId, Is.EqualTo(boardToUpdate.CurrentBackgroundColorId));
            }
        }


        [Test]
        public void Delete_ByDefault_CallsRemoveMethod()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<TrelloDbContext>()
                .UseInMemoryDatabase(databaseName: "Update_ByDefault_ReturnsBoardId")
                .Options;

            Board newBoard = new Board() { Title = "Test", CurrentBackgroundColorId = 2 };

            string idString = DateTime.Now.Ticks.ToString().Substring(0, 9);
            int id = Convert.ToInt32(idString);

            using (var context = new TrelloDbContext(options))
            {
                var repository = new SQLBoardRepository(context);
                context.Boards.Add(newBoard);
                context.SaveChanges();

            }

            //Act
            using (var context = new TrelloDbContext(options))
            {
                var repository = new SQLBoardRepository(context);
                repository.Delete(newBoard.BoardId);
                context.SaveChanges();
               
                //Assert
                Assert.That(repository.deleted, Is.True);
            }
        }
    }
}
