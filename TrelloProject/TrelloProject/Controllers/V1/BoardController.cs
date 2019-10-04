using System.Collections.Generic;
using System.Linq;


using TrelloProject.BLL.Interfaces.ServicesInterfaces;
using TrelloProject.DTOsAndViewModels.ViewModels;
using TrelloProject.DTOsAndViewModels.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using TrelloProject.WEB.Contracts.V1;
using TrelloProject.DTOsAndViewModels.OtherModels;

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
                if(boardsCount == 0 )
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
            try
            {
                BoardBgViewModel boardBgViewModel = _boardDTOService.GetBoard(id);
                return Ok(boardBgViewModel);
            }
            catch(Exception ex) //custom exception should be caught 
            {
                return NotFound(ex.Message);
            }
            
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
                    var locationUri = baseUrl + "/" + ApiRoutes.Board.GetById.Replace("{id}", id.ToString());
                    return Created(locationUri, new BoardDTO { BoardId = id, Title = boardCreateViewModel.Title, CurrentBackgroundColorId = (int)boardCreateViewModel.CurrentBackgroundColorId });
                    //return CreatedAtAction(nameof(GetById), new { id }, boardCreateViewModel);
                }
                catch (NullReferenceException)
                {
                    return NotFound("The background color with ID=" + boardCreateViewModel.CurrentBackgroundColorId + " does not exist");
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
