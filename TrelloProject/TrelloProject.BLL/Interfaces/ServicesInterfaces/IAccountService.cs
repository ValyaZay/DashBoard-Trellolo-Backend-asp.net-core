using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrelloProject.DTOsAndViewModels.JWTauthentication;
using TrelloProject.DTOsAndViewModels.ViewModels;

namespace TrelloProject.BLL.Interfaces.ServicesInterfaces
{
    public interface IAccountService
    {
        Task<AuthenticationResult> CreateUser(RegisterViewModel registerViewModel);

        Task<bool> Login(LoginViewModel loginViewModel);
    }
}
