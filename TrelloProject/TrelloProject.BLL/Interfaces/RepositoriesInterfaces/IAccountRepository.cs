using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrelloProject.DTOsAndViewModels.DTOs;

namespace TrelloProject.BLL.Interfaces.RepositoriesInterfaces
{
    public interface IAccountRepository
    {
        Task<string> CreateUser(RegisterDTO registerDTO);
        Task<bool> Login(LoginDTO loginDTO);
    }
}
