using System.Collections.Generic;
using TrelloProject.DTOsAndViewModels.DTOs;
using TrelloProject.DTOsAndViewModels.ViewModels;

namespace TrelloProject.BLL.Interfaces.ServicesInterfaces
{
    public interface IBoardDTOService
    {
        BoardBgViewModel GetBoard(int id);
        List<BoardBgViewModel> GetAllBoards();
        int CreateBoardDTO(BoardCreateViewModel boardCreateViewModel);
        bool UpdateBoardDTO(BoardUpdateViewModel boardUpdateViewModel);
        bool DeleteBoardDTO(int id);
    }
}
