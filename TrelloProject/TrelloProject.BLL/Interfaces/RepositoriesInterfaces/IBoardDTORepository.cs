using System.Collections.Generic;
using TrelloProject.DTOs;

namespace TrelloProject.BLL.Interfaces.RepositoriesInterfaces
{
    public interface IBoardDTORepository
    {
       
        BoardDTO GetBoard(int Id);
        List<BoardDTO> GetAllBoards();
    }
}
