using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrelloProject.DTOsAndViewModels.DTOs;
using TrelloProject.DTOsAndViewModels.JWTauthentication;

namespace TrelloProject.BLL.Interfaces.RepositoriesInterfaces
{
    public interface IAccountRepository
    {
        Task<AuthenticationResult> CreateUser(RegisterDTO registerDTO);
        Task<bool> Login(LoginDTO loginDTO);
    }
}
