using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrelloProject.DTOsAndViewModels.ViewModels;

namespace TrelloProject.BLL.Interfaces.ServicesInterfaces
{
    public interface IAdministrationService
    {
        Task<string> CreateRole(CreateRoleViewModel createRoleViewModel);
    }
}
