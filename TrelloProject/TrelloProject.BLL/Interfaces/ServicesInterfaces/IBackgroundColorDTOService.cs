using System;
using System.Collections.Generic;
using System.Text;
using TrelloProject.DTOsAndViewModels.DTOs;
using TrelloProject.DTOsAndViewModels.ViewModels;

namespace TrelloProject.BLL.Interfaces.ServicesInterfaces
{
    public interface IBackgroundColorDTOService
    {
        List<BgColorViewModel> GetAllBgColors();
    }
}
