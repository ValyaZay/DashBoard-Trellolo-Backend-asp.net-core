using FluentAssertions;
using System;
using System.Net;
using System.Net.Http;
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
        public async Task Get_IfTheDBIsNotEmpty_ReturnsOKResponse()
        {
            // Arrange
            
            // Act
            var response = await TestClient.GetAsync(ApiRoutes.Board.GetAll);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Create_ModelIsValid_ReturnsCreatedResponse()
        {
            //Arrange
            var titleGuid = Guid.NewGuid().ToString();
            var title = "IntegrTest" + titleGuid;
            BoardCreateViewModel boardCreateViewModel = new BoardCreateViewModel { Title = title, CurrentBackgroundColorId = BgColorEnum.Green };
            
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

        [Fact]
        public async Task Create_BoardTitleAlreadyExists_ReturnsBadRequestResponse()
        {
            //Arrange
            var titleGuid = Guid.NewGuid().ToString();
            var title = "IntegrTest" + titleGuid;
            BoardCreateViewModel boardWithATitle = new BoardCreateViewModel { Title = title, CurrentBackgroundColorId = BgColorEnum.Green };
            await TestClient.PostAsJsonAsync(ApiRoutes.Board.Create, boardWithATitle);
                        
            BoardCreateViewModel boardWithTheSameTitle = new BoardCreateViewModel { Title = title };
            
            //Act
            var response = await TestClient.PostAsJsonAsync(ApiRoutes.Board.Create, boardWithTheSameTitle);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            (await response.Content.ReadAsAsync<string>()).Should().BeEquivalentTo("Board Title " + boardWithTheSameTitle.Title + " already exists");
        }

        [Fact]
        public async Task Create_BoardIsCreated_ReturnsBoardIdInLocation()
        {
            //Arrange
            var titleGuid = Guid.NewGuid().ToString();
            var title = "IntegrTest" + titleGuid;
            BoardCreateViewModel boardCreateViewModel = new BoardCreateViewModel { Title = title, CurrentBackgroundColorId = BgColorEnum.Green };

            //Act
            var response = await TestClient.PostAsJsonAsync(ApiRoutes.Board.Create, boardCreateViewModel);

            //Assert
            var createdBoard = await response.Content.ReadAsAsync<BoardDTO>();
            var id = createdBoard.BoardId.ToString();
            response.Headers.Location.ToString().Should().Contain($"api/v1/board/{id}");
        }

        [Fact]
        public async Task GetById_IfBoardExistsInTheDatabase_ReturnsBoard()
        {
            //Arrange
            var titleGuid = Guid.NewGuid().ToString();
            string title = "IntegrTest" + titleGuid;
            var createdBoard = await CreateBoardAsync( new BoardCreateViewModel { Title = title, CurrentBackgroundColorId = BgColorEnum.Green });
            
            //Act
            var response = await TestClient.GetAsync(ApiRoutes.Board.GetById.Replace("{BoardId}", createdBoard.BoardId.ToString()));

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var returnedBoard = await response.Content.ReadAsAsync<BoardDTO>();
            returnedBoard.BoardId.Should().Be(createdBoard.BoardId);
            returnedBoard.Title.Should().Be(title);
            returnedBoard.CurrentBackgroundColorId.Should().Be((int)BgColorEnum.Green);
        }

        [Fact]
        public async Task Update_ModelIsValid_ReturnsNoContentResponse()
        {
            //Arrange
            var titleGuid = Guid.NewGuid().ToString();
            var title = "IntegrTest" + titleGuid;
            BoardCreateViewModel boardCreated = new BoardCreateViewModel { Title = title, CurrentBackgroundColorId = BgColorEnum.Green };
            var responsePost = await TestClient.PostAsJsonAsync(ApiRoutes.Board.Create, boardCreated);

            string boardLocationWithId = responsePost.Headers.Location.ToString();
            var startIndex = boardLocationWithId.LastIndexOf("/");
            var boardCreatedId = boardLocationWithId.Substring(startIndex + 1);

            var responseGetById = await TestClient.GetAsync(ApiRoutes.Board.GetById.Replace("{BoardId}", boardCreatedId.ToString()));
            var returnedBoardById = await responseGetById.Content.ReadAsAsync<BoardDTO>();

            var updatedTitleGuid = Guid.NewGuid().ToString();
            string updatedTitle = "IntegrTest" + updatedTitleGuid;
            BoardDTO boardUpdated = new BoardDTO { Title = updatedTitle, CurrentBackgroundColorId = returnedBoardById.CurrentBackgroundColorId };

            //Act
            var response = await TestClient.PutAsJsonAsync(ApiRoutes.Board.Update.Replace("{BoardId}", boardCreatedId.ToString()), boardUpdated);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Update_BoardDoesNotExist_ReturnsNotFoundResponse()
        {
            //Arrange
            var idDoesNotExist = DateTime.Now.Ticks.ToString().Substring(0, 9);
            string updatedTitle = "IntegrTest" + idDoesNotExist;
            BoardDTO boardUpdated = new BoardDTO { Title = updatedTitle };

            //Act
            var response = await TestClient.PutAsJsonAsync(ApiRoutes.Board.Update.Replace("{BoardId}", idDoesNotExist), boardUpdated);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            (await response.Content.ReadAsAsync<string>()).Should().BeEquivalentTo("The item with ID=" + idDoesNotExist + " does not exist");
        }

        [Fact]
        public async Task Update_ModelIsNOTValid_ReturnsBadRequestResponse()
        {
            //Arrange
            var titleGuid = Guid.NewGuid().ToString();
            var title = "IntegrTest" + titleGuid;
            BoardCreateViewModel boardCreated = new BoardCreateViewModel { Title = title, CurrentBackgroundColorId = BgColorEnum.Green };
            var responsePost = await TestClient.PostAsJsonAsync(ApiRoutes.Board.Create, boardCreated);

            string boardLocationWithId = responsePost.Headers.Location.ToString();
            var startIndex = boardLocationWithId.LastIndexOf("/");
            var boardCreatedId = boardLocationWithId.Substring(startIndex + 1);

            var responseGetById = await TestClient.GetAsync(ApiRoutes.Board.GetById.Replace("{BoardId}", boardCreatedId.ToString()));
                        
            BoardDTO boardUpdated = new BoardDTO {  };

            //Act
            var response = await TestClient.PutAsJsonAsync(ApiRoutes.Board.Update.Replace("{BoardId}", boardCreatedId.ToString()), boardUpdated);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            (await response.Content.ReadAsAsync<string>()).Should().BeEquivalentTo("Insert valid data");
        }

        [Fact]
        public async Task Delete_BoardDeleted_ReturnsNoContentResponse()
        {
            //Arrange
            var titleGuid = Guid.NewGuid().ToString();
            var title = "IntegrTest" + titleGuid;
            BoardCreateViewModel boardCreated = new BoardCreateViewModel { Title = title, CurrentBackgroundColorId = BgColorEnum.Green };
            var responsePost = await TestClient.PostAsJsonAsync(ApiRoutes.Board.Create, boardCreated);

            string boardLocationWithId = responsePost.Headers.Location.ToString();
            var startIndex = boardLocationWithId.LastIndexOf("/");
            var boardCreatedId = boardLocationWithId.Substring(startIndex + 1);

            //Act
            var response = await TestClient.DeleteAsync(ApiRoutes.Board.Delete.Replace("{BoardId}", boardCreatedId));

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Delete_BoardDoesNotExist_ReturnsNotFoundResponse()
        {
            //Arrange
            var idDoesNotExist = DateTime.Now.Ticks.ToString().Substring(0, 9);

            //Act
            var response = await TestClient.DeleteAsync(ApiRoutes.Board.Delete.Replace("{BoardId}", idDoesNotExist));

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        }
    }
}
