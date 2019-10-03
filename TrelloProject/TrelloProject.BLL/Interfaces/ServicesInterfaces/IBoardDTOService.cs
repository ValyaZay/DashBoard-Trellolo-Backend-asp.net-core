using System.Collections.Generic;
using TrelloProject.DTOsAndViewModels.DTOs;
using TrelloProject.DTOsAndViewModels.ViewModels;

namespace TrelloProject.BLL.Interfaces.ServicesInterfaces
{
    public interface IBoardDTOService
    {
        BoardViewModel GetBoard(int id);
        List<BoardViewModel> GetAllBoards();
        int CreateBoardDTO(BoardCreateViewModel boardCreateViewModel);
        int UpdateBoardDTO(int id, BoardUpdateViewModel boardUpdateViewModel);
        void DeleteBoardDTO(int id);
    }
}
