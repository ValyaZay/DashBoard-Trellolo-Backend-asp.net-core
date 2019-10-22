using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrelloProject.DTOsAndViewModels.ViewModels;

namespace TrelloProject.BLL.Interfaces.ServicesInterfaces
{
    public interface IAccountService
    {
        Task<string> CreateUser(RegisterViewModel registerViewModel);

        Task<bool> Login(LoginViewModel loginViewModel);
    }
}
