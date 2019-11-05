using System.Collections.Generic;
using TrelloProject.DTOsAndViewModels.DTOs;
using TrelloProject.DTOsAndViewModels.ViewModels;

namespace TrelloProject.BLL.Interfaces.ServicesInterfaces
{
    public interface IBoardDTOService
    {
        BoardBgViewModel GetBoard(int id);
        List<BoardBgViewModel> GetAllBoards(string userId);
        int CreateBoardDTO(BoardCreateViewModel boardCreateViewModel, string userId);
        bool UpdateBoardDTO(BoardUpdateViewModel boardUpdateViewModel);
        bool DeleteBoardDTO(int id);
        bool UserOwnsBoard(int boardId, string userId);
    }
}
