using System.Collections.Generic;
using System.Linq;


using TrelloProject.BLL.Interfaces.ServicesInterfaces;
using TrelloProject.DTOsAndViewModels.ViewModels;
using TrelloProject.DTOsAndViewModels.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using TrelloProject.WEB.Contracts.V1;
using TrelloProject.DTOsAndViewModels.Exceptions;

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
        [ProducesResponseType(typeof(List<BoardBgViewModel>), 200)]
        [ProducesResponseType(400)]
        public IActionResult Get()
        {
            try
            {
                var boardBgViewModels = _boardDTOService.GetAllBoards();
                int boardsCount = boardBgViewModels.Count();
                if (boardsCount == 0)
                {
                    return Ok("There are no boards created.");
                }
                else
                {
                    return Ok(boardBgViewModels);

                }
            }
            catch (Exception ex) //custom exception should be caught
            {
                return BadRequest(ex.Message);
            }
        }

        //GET: api/v1/Board/5
        [HttpGet(ApiRoutes.Board.GetById)]
        [ProducesResponseType(typeof(BoardBgViewModel), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetById(int id)
        {
                BoardBgViewModel boardBgViewModel = _boardDTOService.GetBoard(id);
                return Ok(boardBgViewModel);
        }

        //POST: api/v1/Board
        [HttpPost(ApiRoutes.Board.Create)]
        [ProducesResponseType(typeof(int), 201)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult Create([FromBody] BoardCreateViewModel boardCreateViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Insert valid data");//object error should be returned
            }

            int id = _boardDTOService.CreateBoardDTO(boardCreateViewModel);

            if (id == 0)
            {
                return BadRequest("Board is not created");
            }

            else
            {
                var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
                var locationUri = baseUrl + "/" + ApiRoutes.Board.GetById.Replace("{id}", id.ToString());
                return Created(locationUri, id);
            }
        }

        // PUT: api/v1/Board/5
        [HttpPut(ApiRoutes.Board.Update)]
        [ProducesResponseType(typeof(int), 204)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult Update([FromBody] BoardUpdateViewModel boardUpdateViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Insert valid data");
            }
            
            var status = _boardDTOService.UpdateBoardDTO(boardUpdateViewModel); //bool

            if (!status)
            {
                return BadRequest("Board is not updated");
            }

            else
            {
                return NoContent();
            }            
        }

        // DELETE: api/v1/Board/5
        [HttpDelete(ApiRoutes.Board.Delete)]
        [ProducesResponseType(typeof(int), 204)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult Delete(int id)
        {
            var status = _boardDTOService.DeleteBoardDTO(id);
            if (status)
            {
                return NoContent();
            }
            else
            {
                return BadRequest("Board is not deleted");
            }
        }
    }
}
