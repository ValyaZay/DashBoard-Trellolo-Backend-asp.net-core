using System;
using System.Collections.Generic;
using System.Text;
using TrelloProject.BLL.DTO;

namespace TrelloProject.BLL.Interfaces
{
    public interface IBoardDTOService
    {
        BoardDTO GetBoardDTO(int Id);
        List<BoardDTO> GetAllBoardsDTO();
    }
}
