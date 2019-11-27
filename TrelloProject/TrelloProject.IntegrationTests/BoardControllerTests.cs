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
using TrelloProject.DTOsAndViewModels.Exceptions;
using TrelloProject.DTOsAndViewModels.ViewModels;
using TrelloProject.WEB.Contracts.V1;
using TrelloProject.WEB.Controllers.V1;
using TrelloProject.WEB.Infrastructure.ApiResponse;
using Xunit;

namespace TrelloProject.IntegrationTests
{
    public class BoardControllerTests : IntegrationTestBaseClass, IDisposable
    {
        
        [Fact]
        public async Task Get_IfTheDBIsNotEmpty_ReturnsOKResponseAndCorrectCount()
        {
            // Arrange
            
            // Act
            var responseMessageGet = await TestClient.GetAsync(ApiRoutes.AdminBoards.GetAll);
            var jsonStringGet = await responseMessageGet.Content.ReadAsStringAsync();
            var apiResponseGet = JsonConvert.DeserializeObject<ApiResponseSuccess>(jsonStringGet);

            var jsonStringResult = apiResponseGet.Result.ToString();
            var apiResult = JsonConvert.DeserializeObject<List<BoardBgViewModel>>(jsonStringResult);
            int apiResultCount = apiResult.Count;

            // Assert
            apiResult.Should().BeOfType<List<BoardBgViewModel>>();
            apiResult.Should().HaveCount(apiResultCount);
            apiResponseGet.StatusCode.Should().Be(200);
            apiResponseGet.CustomCode.Should().Be(0);
            apiResponseGet.Should().NotBeNull();
        }

        [Fact]
        public async Task Get_IfTheDBIsEmpty_ReturnsOK()
        {
            // Arrange
           

            // Act
            var responseMessageGet = await TestClient.GetAsync(ApiRoutes.AdminBoards.GetAll);
            var jsonStringGet = await responseMessageGet.Content.ReadAsStringAsync();
            var apiResponseGet = JsonConvert.DeserializeObject<ApiResponseSuccess>(jsonStringGet);

            var jsonStringResult = apiResponseGet.Result.ToString();
            var apiResult = JsonConvert.DeserializeObject<List<BoardBgViewModel>>(jsonStringResult);
            //int apiResultCount = apiResult.Count;

            // Assert
            apiResult.Should().BeOfType<List<BoardBgViewModel>>();
            apiResult.Should().HaveCount(0);
            apiResponseGet.StatusCode.Should().Be(200);
            apiResponseGet.CustomCode.Should().Be(0);
            apiResponseGet.Should().NotBeNull();
        }

        [Fact]
        public async Task Create_ModelIsValid_ReturnsCreatedResponseAndUri()
        {
            //Arrange
            var titleGuid = Guid.NewGuid().ToString();
            var title = "IntegrTest" + titleGuid;
            BoardCreateViewModel boardCreateViewModel = new BoardCreateViewModel { Title = title, CurrentBackgroundColorId = 1 };
            
            //Act
            var responseMessage = await TestClient.PostAsJsonAsync(ApiRoutes.AdminBoards.Create, boardCreateViewModel);
            var responseMessageAsAstring = await responseMessage.Content.ReadAsStringAsync();
            var apiNotSuccessResponse = JsonConvert.DeserializeObject<ApiResponseNotSuccess>(responseMessageAsAstring);

            //Assert
            apiNotSuccessResponse.StatusCode.Should().Be(201);
            apiNotSuccessResponse.CustomCode.Should().Be(11);

            apiNotSuccessResponse.Should().NotBeNull();
        }

        [Fact]
        public async Task Create_ModelIsNOTValid_ReturnsBadRequestResponse()
        {
            //Arrange
            BoardCreateViewModel boardCreateViewModel = new BoardCreateViewModel { };

            //Act
            var responseMessage = await TestClient.PostAsJsonAsync(ApiRoutes.AdminBoards.Create, boardCreateViewModel);
            var responseAsAString = await responseMessage.Content.ReadAsStringAsync();
            var apiResponseNotSuccess = JsonConvert.DeserializeObject<ApiResponseNotSuccess>(responseAsAString);

            //Assert
            apiResponseNotSuccess.StatusCode.Should().Be(400);
            apiResponseNotSuccess.CustomCode.Should().Be(3);
            apiResponseNotSuccess.Should().NotBeNull();
        }

        [Fact]
        public async Task Create_BoardTitleAlreadyExists_ReturnsBadRequestResponse()
        {
            //Arrange
            var titleGuid = Guid.NewGuid().ToString();
            var title = "IntegrTest" + titleGuid;
            BoardCreateViewModel boardWithATitle = new BoardCreateViewModel { Title = title, CurrentBackgroundColorId = 2 };

            await TestClient.PostAsJsonAsync(ApiRoutes.AdminBoards.Create, boardWithATitle);
             
            BoardCreateViewModel boardWithTheSameTitle = new BoardCreateViewModel { Title = title };
            
            //Act
            var responseMessage = await TestClient.PostAsJsonAsync(ApiRoutes.AdminBoards.Create, boardWithTheSameTitle);
            var responseAsAString = await responseMessage.Content.ReadAsStringAsync();
            var apiResponseNotSuccess = JsonConvert.DeserializeObject<ApiResponseNotSuccess>(responseAsAString);

            //Assert
            apiResponseNotSuccess.StatusCode.Should().Be(400);
            apiResponseNotSuccess.CustomCode.Should().Be(1);
            apiResponseNotSuccess.Should().NotBeNull();
        }

        [Fact]
        public async Task GetById_IfBoardExistsInTheDatabase_ReturnsOkAndBoard()
        {
            //Arrange
            var titleGuid = Guid.NewGuid().ToString();
            string title = "IntegrTest" + titleGuid;
            BoardCreateViewModel model = new BoardCreateViewModel { Title = title, CurrentBackgroundColorId = 3 };
            
            var responseMessagePost = await TestClient.PostAsJsonAsync(ApiRoutes.AdminBoards.Create, model);
            var jsonStringPost = await responseMessagePost.Content.ReadAsStringAsync();
            var apiResponsePost = JsonConvert.DeserializeObject<ApiResponseSuccess>(jsonStringPost);

            string urlOfCreatedBoard = apiResponsePost.Result.ToString();
            int slashIndex = urlOfCreatedBoard.LastIndexOf("/");
            string id = urlOfCreatedBoard.Substring(slashIndex + 1); 

            //Act
            var responseMessageGet = await TestClient.GetAsync(ApiRoutes.AdminBoards.GetById.Replace("{id}", id));
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
            var response = await TestClient.GetAsync(ApiRoutes.AdminBoards.GetById.Replace("{id}", dummyIdString));

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
            var responseMessagePost = await TestClient.PostAsJsonAsync(ApiRoutes.AdminBoards.Create, boardCreated);

            var responseMessagePostString = await responseMessagePost.Content.ReadAsStringAsync();
            var apiSuccessResponse = JsonConvert.DeserializeObject<ApiResponseSuccess>(responseMessagePostString);
            var url = apiSuccessResponse.Result.ToString();
            int slashIndex = url.LastIndexOf("/");
            string id = url.Substring(slashIndex + 1);

            var updatedTitleGuid = Guid.NewGuid().ToString();
            string updatedTitle = "IntegrTestUpd" + updatedTitleGuid;
            BoardUpdateViewModel boardUpdated = new BoardUpdateViewModel { Id = Convert.ToInt32(id), Title = updatedTitle, CurrentBackgroundColorId = boardCreated.CurrentBackgroundColorId };

            //Act
            var responseMessagePut = await TestClient.PutAsJsonAsync(ApiRoutes.AdminBoards.Update, boardUpdated);
            var responseMessagePutAsString = await responseMessagePut.Content.ReadAsStringAsync();
            var apiSuccessResponsePut = JsonConvert.DeserializeObject<ApiResponseSuccess>(responseMessagePutAsString);
            var apiResult = apiSuccessResponsePut.Result.ToString().ToLower();

            //Assert
            apiSuccessResponsePut.StatusCode.Should().Be(204);
            apiResult.Should().Be("true");
            apiSuccessResponsePut.Should().NotBeNull();
        }

        [Fact]
        public async Task Update_BoardDoesNotExist_ReturnsNotFoundResponse()
        {
            //Arrange
            var idDoesNotExist = DateTime.Now.Ticks.ToString().Substring(0, 9);
            string updatedTitle = "IntegrTest" + idDoesNotExist;
            BoardUpdateViewModel boardUpdated = new BoardUpdateViewModel { Id = Convert.ToInt32(idDoesNotExist), Title = updatedTitle };

            //Act
            var responseMessage = await TestClient.PutAsJsonAsync(ApiRoutes.AdminBoards.Update, boardUpdated);
            var responseMessageAsString = await responseMessage.Content.ReadAsStringAsync();
            var apiNotSuccessResponse = JsonConvert.DeserializeObject<ApiResponseNotSuccess>(responseMessageAsString);

            //Assert
            apiNotSuccessResponse.StatusCode.Should().Be(404);
            apiNotSuccessResponse.CustomCode.Should().Be(6);
            apiNotSuccessResponse.Should().NotBeNull();
        }
        
        [Fact]
        public async Task Update_ModelIsNOTValid_ReturnsBadRequestResponse()
        {
            //Arrange
            var titleGuid = Guid.NewGuid().ToString();
            var title = "IntegrTest" + titleGuid;
            BoardCreateViewModel boardCreated = new BoardCreateViewModel { Title = title, CurrentBackgroundColorId = 3 };
            var responseMessagePost = await TestClient.PostAsJsonAsync(ApiRoutes.AdminBoards.Create, boardCreated);

            var responseMessagePostAsString = await responseMessagePost.Content.ReadAsStringAsync();
            var apiSuccessResponse = JsonConvert.DeserializeObject<ApiResponseSuccess>(responseMessagePostAsString);

            string url = apiSuccessResponse.Result.ToString();
            var indexOfSlash = url.LastIndexOf("/");
            var id = url.Substring(indexOfSlash + 1);

            var notValidTitleGuid = Guid.NewGuid().ToString();
            var notValidTitle = "IntegrTest" + notValidTitleGuid + notValidTitleGuid;
            BoardUpdateViewModel boardUpdated = new BoardUpdateViewModel { Id = Convert.ToInt32(id), Title = notValidTitle };

            //Act
            var responseMessagePut = await TestClient.PutAsJsonAsync((ApiRoutes.AdminBoards.Update), boardUpdated);
            var responseMessagePutAsString = await responseMessagePut.Content.ReadAsStringAsync();
            var apiNotSuccessResponse = JsonConvert.DeserializeObject<ApiResponseNotSuccess>(responseMessagePutAsString);

            //Assert
            apiNotSuccessResponse.StatusCode.Should().Be(400);
            apiNotSuccessResponse.CustomCode.Should().Be(3);
        }

        [Fact]
        public async Task Delete_BoardDeleted_ReturnsNoContentResponse()
        {
            //Arrange
            var titleGuid = Guid.NewGuid().ToString();
            string title = "IntegrTest" + titleGuid;
            BoardCreateViewModel model = new BoardCreateViewModel { Title = title, CurrentBackgroundColorId = 3 };

            var responseMessagePost = await TestClient.PostAsJsonAsync(ApiRoutes.AdminBoards.Create, model);
            var jsonStringPost = await responseMessagePost.Content.ReadAsStringAsync();
            var apiResponsePost = JsonConvert.DeserializeObject<ApiResponseSuccess>(jsonStringPost);

            string urlOfCreatedBoard = apiResponsePost.Result.ToString();
            int slashIndex = urlOfCreatedBoard.LastIndexOf("/");
            string id = urlOfCreatedBoard.Substring(slashIndex + 1);

            //Act
            var responseMessageGetDeletedBoard = await TestClient.DeleteAsync(ApiRoutes.AdminBoards.Delete.Replace("{id}", id));

            var jsonStringGet = await responseMessageGetDeletedBoard.Content.ReadAsStringAsync();
            var apiResponseGetDeleted = JsonConvert.DeserializeObject<ApiResponseSuccess>(jsonStringGet);

            var jsonStringResult = apiResponseGetDeleted.Result.ToString().ToLower();
                   
            //Assert
            jsonStringResult.Should().Be("true");
            apiResponseGetDeleted.StatusCode.Should().Be(204);
            apiResponseGetDeleted.CustomCode.Should().Be(13);
            apiResponseGetDeleted.CustomCodeMessage.Should().BeOfType<string>();
        }

        [Fact]
        public async Task Delete_BoardDoesNotExist_ReturnsNotFoundResponse()
        {
            //Arrange
            var idDoesNotExist = DateTime.Now.Ticks.ToString().Substring(0, 9);

            //Act
            var response = await TestClient.DeleteAsync(ApiRoutes.AdminBoards.Delete.Replace("{id}", idDoesNotExist));
            var responseAsString = await response.Content.ReadAsStringAsync();

            var apiResponse = JsonConvert.DeserializeObject<ApiResponseSuccess>(responseAsString);

            //Assert
            apiResponse.StatusCode.Should().Be(404);
            apiResponse.CustomCode.Should().Be(6);
            apiResponse.CustomCodeMessage.Should().Contain("not exist");
        }

        
        public void Dispose()
        {
            var _context = GetContext();
            _context.Database.EnsureDeleted(); // where to get context 
        }
    }
}
