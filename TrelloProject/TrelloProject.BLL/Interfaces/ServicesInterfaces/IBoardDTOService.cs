using System.Collections.Generic;
using TrelloProject.DTOs;

namespace TrelloProject.BLL.Interfaces.ServicesInterfaces
{
    public interface IBoardDTOService
    {
        BoardDTO GetBoardDTO(int Id);
        List<BoardDTO> GetAllBoardsDTO();
    }
}
