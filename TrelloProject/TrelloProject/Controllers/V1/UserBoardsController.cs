using System.Collections.Generic;
using System.Linq;


using TrelloProject.BLL.Interfaces.ServicesInterfaces;
using TrelloProject.DTOsAndViewModels.ViewModels;
using TrelloProject.DTOsAndViewModels.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using TrelloProject.WEB.Contracts.V1;
using TrelloProject.DTOsAndViewModels.Exceptions;
using System.Threading.Tasks;
using TrelloProject.WEB.Infrastructure.ApiResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace TrelloProject.WEB.Controllers.V1
{
    [Produces("application/json")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin, User")]
    public class UserBoardsController : Controller
    {
        private readonly IBoardDTOService _boardDTOService;

        public UserBoardsController(IBoardDTOService boardDTOService)
        {
            _boardDTOService = boardDTOService;
        }


        // GET: api/v1/users/{userId}/board
        [HttpGet(ApiRoutes.UserBoards.GetAll)]
        [ProducesResponseType(typeof(ApiResponseSuccess), 200)]
        [ProducesResponseType(typeof(ApiResponseNotSuccess), 400)]
        public ApiResponseSuccess Get()
        {
            var userId = User.Claims.Single(x => x.Type == "id").Value;
            if (userId == null)
            {
                throw new ApiException(400, 19);
            }
            List <BoardBgViewModel> boardList = _boardDTOService.GetAllBoards(userId);
            return new ApiResponseSuccess(200, 0, boardList);

        }

        //GET: api/v1/users/{userId}/board/5
        [HttpGet(ApiRoutes.UserBoards.GetById)]
        [ProducesResponseType(typeof(ApiResponseSuccess), 200)]
        [ProducesResponseType(typeof(ApiResponseNotSuccess), 400)]
        public ApiResponseSuccess GetById(int id)
        {
            var userId = User.Claims.Single(x => x.Type == "id").Value;
            if (userId == null)
            {
                throw new ApiException(400, 19);
            }
            var userOwnsBoard = _boardDTOService.UserOwnsBoard(id, userId);
            if (!userOwnsBoard)
            {
                throw new ApiException(400, 20);
            }

            BoardBgViewModel board = _boardDTOService.GetBoard(id);
            return new ApiResponseSuccess(200, 0, board);
        }

        //POST: api/v1/users/{userId}/board
        [HttpPost(ApiRoutes.UserBoards.Create)]
        [ProducesResponseType(typeof(ApiResponseSuccess), 200)]
        [ProducesResponseType(typeof(ApiResponseNotSuccess), 400)]
        public ApiResponseSuccess Create([FromBody] BoardCreateViewModel boardCreateViewModel)
        {
            var userId = User.Claims.Single(x => x.Type == "id").Value;
            if(userId == null)
            {
                throw new ApiException(400, 19);
            }

            if(!ModelState.IsValid)
            {
                throw new ApiException(400, 3);
            }
            int id = _boardDTOService.CreateBoardDTO(boardCreateViewModel, userId);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.UserBoards.GetById.Replace("{id}", id.ToString()).Replace("{userId}", userId);
            
            return new ApiResponseSuccess(201, 11, locationUri);
        }

        // PUT: api/v1/users/{userId}/board/5
        [HttpPut(ApiRoutes.UserBoards.Update)]
        [ProducesResponseType(typeof(ApiResponseSuccess), 200)]
        [ProducesResponseType(typeof(ApiResponseNotSuccess), 400)]
        public ApiResponseSuccess Update([FromBody] BoardUpdateViewModel boardUpdateViewModel)
        {
            var userId = User.Claims.Single(x => x.Type == "id").Value;
            if (userId == null)
            {
                throw new ApiException(400, 19);
            }
            var userOwnsBoard = _boardDTOService.UserOwnsBoard(boardUpdateViewModel.Id, userId);
            if (!userOwnsBoard)
            {
                throw new ApiException(400, 20);
            }
            if (!ModelState.IsValid)
            {
                throw new ApiException(400, 3);
            }
            var status = _boardDTOService.UpdateBoardDTO(boardUpdateViewModel); 
            return new ApiResponseSuccess(204, 12, status.ToString());
        }

        // DELETE: api/v1/users/{userId}/board/5
        [HttpDelete(ApiRoutes.UserBoards.Delete)]
        [ProducesResponseType(typeof(ApiResponseSuccess), 200)]
        [ProducesResponseType(typeof(ApiResponseNotSuccess), 400)]
        public ApiResponseSuccess Delete(int id)
        {
            var userId = User.Claims.Single(x => x.Type == "id").Value;
            if (userId == null)
            {
                throw new ApiException(400, 19);
            }
            var userOwnsBoard = _boardDTOService.UserOwnsBoard(id, userId);
            if (!userOwnsBoard)
            {
                throw new ApiException(400, 20);
            }
            var status = _boardDTOService.DeleteBoardDTO(id);
            return new ApiResponseSuccess(204, 13, status.ToString());

        }
    }
}
