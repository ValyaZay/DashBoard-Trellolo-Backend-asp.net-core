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


        // GET: api/Board
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

        //// GET: api/Board/5
        //[HttpGet("{id}", Name = "Get")]
        //public IActionResult GetById(int id)
        //{
        //    BoardDTO boardDTO = _boardDTOService.GetBoardDTO(id);
        //    if(boardDTO == null)
        //    {
        //        return NotFound("The board with ID = " + id + " does not exist");
        //    }
        //    return Ok(boardDTO);
        //}

        //[HttpPost]
        //public IActionResult Create(BoardCreateViewModel boardCreateViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            int id = _boardDTOService.CreateBoardDTO(boardCreateViewModel);
        //            return CreatedAtAction(nameof(GetById), new { id }, boardCreateViewModel);
        //        }

        //        catch (Exception)
        //        {
        //            return BadRequest(error: "Board Title " + boardCreateViewModel.Title + " already exists");
        //        }
                
        //    }
        //    return BadRequest("Insert valid data");
        //}

        //// PUT: api/Board/5
        //[HttpPut("{id}")]
        //public IActionResult Update(int id, BoardUpdateViewModel boardUpdateViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _boardDTOService.UpdateBoardDTO(id, boardUpdateViewModel);
        //            return NoContent();
                    
        //        }
            
        //        catch (NullReferenceException)
        //        {
        //            return NotFound("The item with ID=" + id + " does not exist");
        //        }
        //        catch (Exception)
        //        {
        //            return BadRequest(error: "Board Title " + boardUpdateViewModel.Title + " already exists");
        //        }
                

        //    }
        //    return BadRequest("Insert valid data");

        //}

        //// DELETE: api/Board/5
        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    try
        //    {
        //        _boardDTOService.DeleteBoardDTO(id);
        //        return NoContent();
        //    }
        //    catch (NullReferenceException)
        //    {
        //        return NotFound("The item with ID=" + id + " does not exist");
        //    } 
        //}

    }
}
