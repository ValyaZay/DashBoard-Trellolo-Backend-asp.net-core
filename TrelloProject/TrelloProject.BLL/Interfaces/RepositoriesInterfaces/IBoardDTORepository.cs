using System.Collections.Generic;
using TrelloProject.DTOsAndViewModels.DTOs;

namespace TrelloProject.BLL.Interfaces.RepositoriesInterfaces
{
    public interface IBoardDTORepository
    {
       
        BoardBgDTO GetBoard(int Id);
        List<BoardBgDTO> GetAllBoards();
        int Create(BoardDTO newBoardDTO);
        bool Update(BoardDTO updatedBoardDTO);
        void Delete(int id);
    }
}
