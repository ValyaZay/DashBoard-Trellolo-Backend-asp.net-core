using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrelloProject.DTOsAndViewModels.DTOs;

namespace TrelloProject.BLL.Interfaces.RepositoriesInterfaces
{
    public interface IAdministrationRepository
    {
        Task<string> CreateRole(CreateRoleDTO createRoleDTO);
    }
}
