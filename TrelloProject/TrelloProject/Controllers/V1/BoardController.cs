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

namespace TrelloProject.WEB.Controllers.V1
{
    [Produces("application/json")]

    public class BoardController : Controller
    {
        private readonly IBoardDTOService _boardDTOService;

        public BoardController(IBoardDTOService boardDTOService)
        {
            _boardDTOService = boardDTOService;
        }


        // GET: api/v1/Board

        [HttpGet(ApiRoutes.Board.GetAll)]
        [ProducesResponseType(typeof(ApiResponseSuccess), 200)]
        [ProducesResponseType(typeof(ApiResponseNotSuccess), 400)]
        public ApiResponseSuccess Get()
        {
            List <BoardBgViewModel> boardList = _boardDTOService.GetAllBoards();
            return new ApiResponseSuccess(200, 0, boardList);

        }

        //GET: api/v1/Board/5
        [HttpGet(ApiRoutes.Board.GetById)]
        [ProducesResponseType(typeof(ApiResponseSuccess), 200)]
        [ProducesResponseType(typeof(ApiResponseNotSuccess), 400)]
        public ApiResponseSuccess GetById(int id)
        {
            BoardBgViewModel board = _boardDTOService.GetBoard(id);
            return new ApiResponseSuccess(200, 0, board);
        }

        //POST: api/v1/Board
        [HttpPost(ApiRoutes.Board.Create)]
        [ProducesResponseType(typeof(ApiResponseSuccess), 200)]
        [ProducesResponseType(typeof(ApiResponseNotSuccess), 400)]
        public ApiResponseSuccess Create([FromBody] BoardCreateViewModel boardCreateViewModel)
        {
            if(!ModelState.IsValid)
            {
                throw new ApiException(400, 3);
            }
            int id = _boardDTOService.CreateBoardDTO(boardCreateViewModel);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.Board.GetById.Replace("{id}", id.ToString());
            
            return new ApiResponseSuccess(201, 11, locationUri);
        }

        // PUT: api/v1/Board/5
        [HttpPut(ApiRoutes.Board.Update)]
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
        [HttpDelete(ApiRoutes.Board.Delete)]
        [ProducesResponseType(typeof(ApiResponseSuccess), 200)]
        [ProducesResponseType(typeof(ApiResponseNotSuccess), 400)]
        public ApiResponseSuccess Delete(int id)
        {
            var status = _boardDTOService.DeleteBoardDTO(id);
            return new ApiResponseSuccess(204, 13, status.ToString());

        }
    }
}
