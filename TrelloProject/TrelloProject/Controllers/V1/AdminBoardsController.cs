using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrelloProject.BLL.Interfaces.ServicesInterfaces;
using TrelloProject.DTOsAndViewModels.Exceptions;
using TrelloProject.DTOsAndViewModels.ViewModels;
using TrelloProject.WEB.Contracts.V1;
using TrelloProject.WEB.Infrastructure.ApiResponse;

namespace TrelloProject.WEB.Controllers.V1
{
    [Produces("application/json")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public class AdminBoardsController : Controller
    {
        private readonly IBoardDTOService _boardDTOService;

        public AdminBoardsController(IBoardDTOService boardDTOService)
        {
            _boardDTOService = boardDTOService;
        }


        // GET: api/v1/Board
        [HttpGet(ApiRoutes.AdminBoards.GetAll)]
        [ProducesResponseType(typeof(ApiResponseSuccess), 200)]
        [ProducesResponseType(typeof(ApiResponseNotSuccess), 400)]
        public ApiResponseSuccess Get()
        {
            List<BoardBgViewModel> boardList = _boardDTOService.GetAllBoards();
            return new ApiResponseSuccess(200, 0, boardList);
        }

        //GET: api/v1/Board/5
        [HttpGet(ApiRoutes.AdminBoards.GetById)]
        [ProducesResponseType(typeof(ApiResponseSuccess), 200)]
        [ProducesResponseType(typeof(ApiResponseNotSuccess), 400)]
        public ApiResponseSuccess GetById(int id)
        {
            BoardBgViewModel board = _boardDTOService.GetBoard(id);
            return new ApiResponseSuccess(200, 0, board);
        }

        //POST: api/v1/Board
        [HttpPost(ApiRoutes.AdminBoards.Create)]
        [ProducesResponseType(typeof(ApiResponseSuccess), 200)]
        [ProducesResponseType(typeof(ApiResponseNotSuccess), 400)]
        public ApiResponseSuccess Create([FromBody] BoardCreateViewModel boardCreateViewModel)
        {
            var userId = User.Claims.Single(x => x.Type == "id").Value;
            if (userId == null)
            {
                throw new ApiException(400, 19);
            }

            if (!ModelState.IsValid)
            {
                throw new ApiException(400, 3);
            }
            int id = _boardDTOService.CreateBoardDTO(boardCreateViewModel, userId);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.UserBoards.GetById.Replace("{id}", id.ToString()).Replace("{userId}", userId);

            return new ApiResponseSuccess(201, 11, locationUri);
        }

        // PUT: api/v1/Board/5
        [HttpPut(ApiRoutes.AdminBoards.Update)]
        [ProducesResponseType(typeof(ApiResponseSuccess), 200)]
        [ProducesResponseType(typeof(ApiResponseNotSuccess), 400)]
        public ApiResponseSuccess Update([FromBody] BoardUpdateViewModel boardUpdateViewModel)
        {
            if (!ModelState.IsValid)
            {
                throw new ApiException(400, 3);
            }
            var status = _boardDTOService.UpdateBoardDTO(boardUpdateViewModel);
            return new ApiResponseSuccess(204, 12, status.ToString());
        }

        // DELETE: api/v1/Board/5
        [HttpDelete(ApiRoutes.AdminBoards.Delete)]
        [ProducesResponseType(typeof(ApiResponseSuccess), 200)]
        [ProducesResponseType(typeof(ApiResponseNotSuccess), 400)]
        public ApiResponseSuccess Delete(int id)
        {
            var status = _boardDTOService.DeleteBoardDTO(id);
            return new ApiResponseSuccess(204, 13, status.ToString());

        }
    }
}
