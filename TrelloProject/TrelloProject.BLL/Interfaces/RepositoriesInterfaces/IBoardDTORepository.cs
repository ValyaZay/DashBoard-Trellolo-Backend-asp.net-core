using System.Collections.Generic;
using TrelloProject.DTOsAndViewModels.DTOs;

namespace TrelloProject.BLL.Interfaces.RepositoriesInterfaces
{
    public interface IBoardDTORepository
    {
       
        BoardBgDTO GetBoard(int id);
        List<BoardBgDTO> GetAllBoards(string userId);
        int Create(BoardDTO newBoardDTO, string userId);
        bool Update(BoardDTO updatedBoardDTO);
        bool Delete(int id);

        string GetUserIdFromContext(int boardId);

        List<BoardBgDTO> GetAllBoards();
       
    }
}
