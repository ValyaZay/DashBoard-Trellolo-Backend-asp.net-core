using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TrelloProject.DAL.EF;
using TrelloProject.DAL.Entities;
using TrelloProject.DTOsAndViewModels.DTOs;
using TrelloProject.DTOsAndViewModels.ViewModels;
using TrelloProject.WEB.Contracts.V1;
using TrelloProject.WEB.Controllers.V1;
using TrelloProject.WEB.Infrastructure.ApiResponse;
using Xunit;

namespace TrelloProject.IntegrationTests
{
    public class BoardControllerTests : IntegrationTestBaseClass 
    {
        
        [Fact]
        public async Task Get_IfTheDBIsNotEmpty_ReturnsOKResponseAndCorrectCount()
        {
            // Arrange
            
            // Act
            var response = await TestClient.GetAsync(ApiRoutes.Board.GetAll);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var returnedCollection = await response.Content.ReadAsAsync<List<BoardDTO>>();
            returnedCollection.Count.Should().BeGreaterThan(0);
            returnedCollection.Should().NotBeNullOrEmpty();
            returnedCollection.Should().BeOfType<List<BoardDTO>>();
            returnedCollection.Should().NotContainNulls();
        }

        private HttpClient TestClientInMemory;
        [Fact]
        public async Task Get_IfTheDBIsEmpty_ReturnsOKResponseWithMessage()
        {
            // Arrange
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.RemoveAll(typeof(TrelloDbContext));
                        services.AddDbContext<TrelloDbContext>(options =>
                        {
                            options.UseInMemoryDatabase("Get_IfTheDBIsEmpty_ReturnsOKResponseWithMessage");
                        });
                    });
                });
            TestClientInMemory = appFactory.CreateClient();

            // Act
            var response = await TestClientInMemory.GetAsync(ApiRoutes.Board.GetAll);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<string>()).Should().BeEquivalentTo("There are no boards created.");
        }

        [Fact]
        public async Task Create_ModelIsValid_ReturnsCreatedResponse()
        {
            //Arrange
            var titleGuid = Guid.NewGuid().ToString();
            var title = "IntegrTest" + titleGuid;
            BoardCreateViewModel boardCreateViewModel = new BoardCreateViewModel { Title = title, CurrentBackgroundColorId = 1 };
            
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
            BoardCreateViewModel boardWithATitle = new BoardCreateViewModel { Title = title, CurrentBackgroundColorId = 2 };
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
            BoardCreateViewModel boardCreateViewModel = new BoardCreateViewModel { Title = title, CurrentBackgroundColorId = 2 };

            //Act
            var response = await TestClient.PostAsJsonAsync(ApiRoutes.Board.Create, boardCreateViewModel);

            //Assert
            var createdBoardId = await response.Content.ReadAsAsync<int>();
           
            response.Headers.Location.ToString().Should().Contain($"api/v1/board/{createdBoardId}");

            createdBoardId.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task GetById_IfBoardExistsInTheDatabase_ReturnsOkAndBoard()
        {
            //Arrange
            var titleGuid = Guid.NewGuid().ToString();
            string title = "IntegrTest" + titleGuid;
            BoardCreateViewModel model = new BoardCreateViewModel { Title = title, CurrentBackgroundColorId = 3 };
            
            var responseMessagePost = await TestClient.PostAsJsonAsync(ApiRoutes.Board.Create, model);
            var jsonStringPost = await responseMessagePost.Content.ReadAsStringAsync();
            var apiResponsePost = JsonConvert.DeserializeObject<ApiResponseSuccess>(jsonStringPost);

            string urlOfCreatedBoard = apiResponsePost.Result.ToString();
            int slashIndex = urlOfCreatedBoard.LastIndexOf("/");
            string id = urlOfCreatedBoard.Substring(slashIndex + 1); 

            //Act
            var responseMessageGet = await TestClient.GetAsync(ApiRoutes.Board.GetById.Replace("{id}", id));
            var jsonStringGet = await responseMessageGet.Content.ReadAsStringAsync();
            var apiResponseGet = JsonConvert.DeserializeObject<ApiResponseSuccess>(jsonStringGet);

            var jsonStringResult = apiResponseGet.Result.ToString();
            var apiResult = JsonConvert.DeserializeObject<BoardBgViewModel>(jsonStringResult);

            //Assert
            responseMessageGet.StatusCode.Should().Be(HttpStatusCode.OK);

            apiResult.Id.Should().Be(Convert.ToInt32(id));
            apiResponseGet.StatusCode.Should().Be(200);
            apiResponseGet.CustomCode.Should().Be(0);
            apiResponseGet.Should().NotBeNull();
        }

        [Fact]
        public async Task GetById_IfBoardDoesNOTExistInTheDatabase_ReturnsNotFoundAndMessage()
        {
            //Arrange
            string dummyIdString  = DateTime.Now.Ticks.ToString().Substring(0, 9);
            
            //Act
            var response = await TestClient.GetAsync(ApiRoutes.Board.GetById.Replace("{BoardId}", dummyIdString));

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            
        }

        [Fact]
        public async Task Update_ModelIsValid_ReturnsNoContentResponse()
        {
            //Arrange
            var titleGuid = Guid.NewGuid().ToString();
            var title = "IntegrTest" + titleGuid;
            BoardCreateViewModel boardCreated = new BoardCreateViewModel { Title = title, CurrentBackgroundColorId = 4 };
            var responsePost = await TestClient.PostAsJsonAsync(ApiRoutes.Board.Create, boardCreated);

            string boardLocationWithId = responsePost.Headers.Location.ToString();
            var startIndex = boardLocationWithId.LastIndexOf("/");
            var boardCreatedId = boardLocationWithId.Substring(startIndex + 1);

            var responseGetById = await TestClient.GetAsync(ApiRoutes.Board.GetById.Replace("{id}", boardCreatedId.ToString()));
            var returnedBoardById = await responseGetById.Content.ReadAsAsync<BoardBgViewModel>();

            var updatedTitleGuid = Guid.NewGuid().ToString();
            string updatedTitle = "IntegrTestUpd" + updatedTitleGuid;
            BoardUpdateViewModel boardUpdated = new BoardUpdateViewModel { Id = returnedBoardById.Id, Title = updatedTitle, CurrentBackgroundColorId = returnedBoardById.BgColorId };

            //Act
            var response = await TestClient.PutAsJsonAsync(ApiRoutes.Board.Update, boardUpdated);
            
            var responseGetUpdatedBoard = await TestClient.GetAsync(ApiRoutes.Board.GetById.Replace("{id}", boardUpdated.Id.ToString()));

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
            var returnedUpdatedBoard = await responseGetUpdatedBoard.Content.ReadAsAsync<BoardBgViewModel>();
            returnedUpdatedBoard.Id.Should().Be(boardUpdated.Id);
            returnedUpdatedBoard.Title.Should().Be(boardUpdated.Title);
            returnedUpdatedBoard.BgColorId.Should().Be((int)boardUpdated.CurrentBackgroundColorId);
            returnedUpdatedBoard.Should().NotBeNull();
        }

        [Fact]
        public async Task Update_BoardDoesNotExist_ReturnsNotFoundResponse()
        {
            //Arrange
            var idDoesNotExist = DateTime.Now.Ticks.ToString().Substring(0, 9);
            string updatedTitle = "IntegrTest" + idDoesNotExist;
            BoardUpdateViewModel boardUpdated = new BoardUpdateViewModel { Id = Convert.ToInt32(idDoesNotExist), Title = updatedTitle };

            //Act
            var response = await TestClient.PutAsJsonAsync(ApiRoutes.Board.Update, boardUpdated);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            (await response.Content.ReadAsAsync<string>()).Should().BeEquivalentTo("The background color with ID = ... does not exist");
        }
        
        [Fact]
        public async Task Update_ModelIsNOTValid_ReturnsBadRequestResponse()
        {
            //Arrange
            var titleGuid = Guid.NewGuid().ToString();
            var title = "IntegrTest" + titleGuid;
            BoardCreateViewModel boardCreated = new BoardCreateViewModel { Title = title, CurrentBackgroundColorId = 3 };
            var responsePost = await TestClient.PostAsJsonAsync(ApiRoutes.Board.Create, boardCreated);

            string boardLocationWithId = responsePost.Headers.Location.ToString();
            var startIndex = boardLocationWithId.LastIndexOf("/");
            var boardCreatedId = boardLocationWithId.Substring(startIndex + 1);

            var notValidTitleGuid = Guid.NewGuid().ToString();
            var notValidTitle = "IntegrTest" + notValidTitleGuid + notValidTitleGuid;
            BoardDTO boardUpdated = new BoardDTO { Title = notValidTitle };

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
            BoardCreateViewModel boardCreated = new BoardCreateViewModel { Title = title, CurrentBackgroundColorId = 3 };
            var responsePost = await TestClient.PostAsJsonAsync(ApiRoutes.Board.Create, boardCreated);

            string boardLocationWithId = responsePost.Headers.Location.ToString();
            var startIndex = boardLocationWithId.LastIndexOf("/");
            var boardCreatedId = boardLocationWithId.Substring(startIndex + 1);

            //Act
            var response = await TestClient.DeleteAsync(ApiRoutes.Board.Delete.Replace("{id}", boardCreatedId));
            var responseGetDeletedBoard = await TestClient.GetAsync(ApiRoutes.Board.GetById.Replace("{id}", boardCreatedId));

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
            (await responseGetDeletedBoard.Content.ReadAsAsync<string>()).Should().BeEquivalentTo("The board with ID = " + boardCreatedId + " does not exist");
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
            (await response.Content.ReadAsAsync<string>()).Should().BeEquivalentTo("The item with ID=" + idDoesNotExist + " does not exist");
        }
    }
}
