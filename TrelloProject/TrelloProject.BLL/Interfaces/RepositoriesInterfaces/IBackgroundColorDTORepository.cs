using System;
using System.Collections.Generic;
using System.Text;
using TrelloProject.DTOsAndViewModels.DTOs;

namespace TrelloProject.BLL.Interfaces.RepositoriesInterfaces
{
    public interface IBackgroundColorDTORepository
    {
        bool DoesBackgroundColorExist(int id);
    }
}
