using System.Collections.Generic;
using System.Linq;


using TrelloProject.BLL.Interfaces.ServicesInterfaces;
using TrelloProject.DTOsAndViewModels.ViewModels;
using TrelloProject.DTOsAndViewModels.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace TrelloProject.WEB.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class BoardController : ControllerBase
    {
        private readonly IBoardDTOService _boardDTOService;

        public BoardController(IBoardDTOService boardDTOService)
        {
            _boardDTOService = boardDTOService;
        }


        // GET: api/Board
        [HttpGet]
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

        // GET: api/Board/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult GetById(int id)
        {
            BoardDTO boardDTO = _boardDTOService.GetBoardDTO(id);
            if(boardDTO == null)
            {
                return NotFound("The board with ID = " + id + " does not exist");
            }
            return Ok(boardDTO);
        }

        [HttpPost]
        public IActionResult Create(BoardCreateViewModel boardCreateViewModel)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    int id = _boardDTOService.CreateBoardDTO(boardCreateViewModel);
                    return Ok(id);
                }

                catch
                {
                    return BadRequest(error: "Board Title " + boardCreateViewModel.Title + " already exists");
                }
                
            }
            return BadRequest("Insert the Board Title");


        }
    }
}
