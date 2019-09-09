using System.Collections.Generic;
using TrelloProject.DTOsAndViewModels.DTOs;

namespace TrelloProject.BLL.Interfaces.RepositoriesInterfaces
{
    public interface IBoardDTORepository
    {
       
        BoardDTO GetBoard(int Id);
        List<BoardDTO> GetAllBoards();
        int Create(BoardDTO newBoardDTO);
    }
}
