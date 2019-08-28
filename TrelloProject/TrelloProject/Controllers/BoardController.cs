using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrelloProject.BLL.Interfaces;
using TrelloProject.BLL.DTO;

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

        //// POST: api/Board
        //[HttpPost]
        //public void Post([FromBody] BoardDTO boardDTO)
        //{
        //    try
        //    {
        //        int id = _boardRepository.Create(boardDTO);
        //    }
        //    catch (DbUpdateException)
        //}

        //// PUT: api/Board/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
