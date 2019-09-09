using System.Collections.Generic;
using TrelloProject.DTOsAndViewModels.DTOs;
using TrelloProject.DTOsAndViewModels.ViewModels;

namespace TrelloProject.BLL.Interfaces.ServicesInterfaces
{
    public interface IBoardDTOService
    {
        BoardDTO GetBoardDTO(int Id);
        List<BoardDTO> GetAllBoardsDTO();
        int CreateBoardDTO(BoardCreateViewModel boardCreateViewModel);
        int UpdateBoardDTO(int id, BoardUpdateViewModel boardUpdateViewModel);
        void DeleteBoardDTO(int id);
    }
}
