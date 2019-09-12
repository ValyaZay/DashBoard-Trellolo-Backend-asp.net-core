using System.Collections.Generic;
using System.Linq;


using TrelloProject.BLL.Interfaces.ServicesInterfaces;
using TrelloProject.DTOsAndViewModels.ViewModels;
using TrelloProject.DTOsAndViewModels.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using TrelloProject.WEB.Contracts.V1;

namespace TrelloProject.WEB.Controllers.V1
{
    [Produces("application/json")]
    //[Route("api/[controller]")]
    //[ApiController]
    public class BoardController : Controller
    {
        private readonly IBoardDTOService _boardDTOService;

        public BoardController(IBoardDTOService boardDTOService)
        {
            _boardDTOService = boardDTOService;
        }


        // GET: api/v1/Board
        [HttpGet(ApiRoutes.Board.GetAll)]
        public IActionResult Get()
        {
            List<BoardDTO> boardsDTO = _boardDTOService.GetAllBoardsDTO();
            int boardsCount = boardsDTO.Count();
            if(boardsCount == 0 )
            {
                return NotFound("There are no boards created.");
            }
            else
            {
                return Ok(boardsDTO);

            }
            
        }

        //GET: api/v1/Board/5
        [HttpGet(ApiRoutes.Board.GetById)]
        public IActionResult GetById([FromRoute] int BoardId)
        {
            BoardDTO boardDTO = _boardDTOService.GetBoardDTO(BoardId);
            if (boardDTO == null)
            {
                return NotFound("The board with ID = " + BoardId + " does not exist");
            }
            return Ok(boardDTO);
        }

        //POST: api/v1/Board
        [HttpPost(ApiRoutes.Board.Create)]
        public IActionResult Create([FromBody] BoardCreateViewModel boardCreateViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int id = _boardDTOService.CreateBoardDTO(boardCreateViewModel);
                    var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
                    var locationUri = baseUrl + "/" + ApiRoutes.Board.GetById.Replace("{BoardId}", id.ToString());
                    return Created(locationUri, new BoardDTO { BoardId = id, Title = boardCreateViewModel.Title, CurrentBackgroundColorId = (int)boardCreateViewModel.CurrentBackgroundColorId });
                    //return CreatedAtAction(nameof(GetById), new { id }, boardCreateViewModel);
                }

                catch (Exception)
                {
                    return BadRequest(error: "Board Title " + boardCreateViewModel.Title + " already exists");
                }

            }
            return BadRequest("Insert valid data");
        }

        // PUT: api/v1/Board/5
        [HttpPut(ApiRoutes.Board.Update)]
        public IActionResult Update([FromRoute] int BoardId, [FromBody] BoardUpdateViewModel boardUpdateViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _boardDTOService.UpdateBoardDTO(BoardId, boardUpdateViewModel);
                    return NoContent();

                }

                catch (NullReferenceException)
                {
                    return NotFound("The item with ID=" + BoardId + " does not exist");
                }
                catch (Exception)
                {
                    return BadRequest(error: "Board Title " + boardUpdateViewModel.Title + " already exists");
                }


            }
            return BadRequest("Insert valid data");

        }

        // DELETE: api/v1/Board/5
        [HttpDelete(ApiRoutes.Board.Delete)]
        public IActionResult Delete([FromRoute] int BoardId)
        {
            try
            {
                _boardDTOService.DeleteBoardDTO(BoardId);
                return NoContent();
            }
            catch (NullReferenceException)
            {
                return NotFound("The item with ID=" + BoardId + " does not exist");
            }
        }

    }
}
