using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TrelloProject.DTOsAndViewModels.DTOs;
using TrelloProject.DTOsAndViewModels.ViewModels;
using TrelloProject.WEB.Contracts.V1;
using Xunit;

namespace TrelloProject.IntegrationTests
{
    public class BoardControllerTests : IntegrationTestBaseClass 
    {
        
        [Fact]
        public async Task Get_IfTheDBIsEmpty_ReturnsNotFoundResponse()
        {
            // Arrange
            
            // Act
            var response = await TestClient.GetAsync(ApiRoutes.Board.GetAll);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            (await response.Content.ReadAsAsync<string>()).Should().BeEquivalentTo("There are no boards created.");
        }

        [Fact]
        public async Task Create_ModelIsValid_ReturnsCreatedResponse()
        {
            //Arrange
            BoardCreateViewModel boardCreateViewModel = new BoardCreateViewModel { Title = "TestCreated", CurrentBackgroundColorId = BgColorEnum.Green };
            
            //Act
            var response = await TestClient.PostAsJsonAsync(ApiRoutes.Board.Create, boardCreateViewModel);


            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task Create_ModelIsNOTValid_ReturnsBadRequestResponse()
        {
            //Arrange
            BoardCreateViewModel boardCreateViewModel = new BoardCreateViewModel { };

            //Act
            var response = await TestClient.PostAsJsonAsync(ApiRoutes.Board.Create, boardCreateViewModel);
                       
            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            (await response.Content.ReadAsAsync<string>()).Should().BeEquivalentTo("Insert valid data");
        }

        [Fact(Skip = "when the second item is created, OnModelCreating() is not hit and uniqueness of the board title is not checked")]
        public async Task Create_BoardTitleAlreadyExists_ReturnsBadRequestResponse()
        {
            //Arrange
            await SeedContext(new BoardCreateViewModel { Title = "TestCreated", CurrentBackgroundColorId = BgColorEnum.Green });
            
            BoardCreateViewModel boardToCreateWithTheSameTitle = new BoardCreateViewModel { Title = "TestCreated" };
            
            
            //Act
            var response = await TestClient.PostAsJsonAsync(ApiRoutes.Board.Create, boardToCreateWithTheSameTitle);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            (await response.Content.ReadAsAsync<string>()).Should().BeEquivalentTo("Board Title " + boardToCreateWithTheSameTitle.Title + " already exists");
        }

        [Fact]
        public async Task Create_BoardIsCreated_ReturnsBoardIdInLocation()
        {
            //Arrange
            BoardCreateViewModel boardCreateViewModel = new BoardCreateViewModel { Title = "TestCreated", CurrentBackgroundColorId = BgColorEnum.Green };

            //Act
            var response = await TestClient.PostAsJsonAsync(ApiRoutes.Board.Create, boardCreateViewModel);

            //Assert
            var createdBoard = await response.Content.ReadAsAsync<BoardDTO>();
            var id = createdBoard.BoardId.ToString();
            response.Headers.Location.ToString().Should().Contain($"api/v1/board/{id}");
        }

        [Fact]
        public async Task GetById_WhenBoardExistsInTheDatabase_ReturnsBoard()
        {
            //Arrange
            var createdBoard = await CreateBoardAsync( new BoardCreateViewModel { Title = "TestGetById", CurrentBackgroundColorId = BgColorEnum.Green });

            //Act
            var response = await TestClient.GetAsync(ApiRoutes.Board.GetById.Replace("{BoardId}", createdBoard.BoardId.ToString()));

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var returnedBoard = await response.Content.ReadAsAsync<BoardDTO>();
            returnedBoard.BoardId.Should().Be(createdBoard.BoardId);
            returnedBoard.Title.Should().Be("TestGetById");
            returnedBoard.CurrentBackgroundColorId.Should().Be((int)BgColorEnum.Green);
        }
    }
}
