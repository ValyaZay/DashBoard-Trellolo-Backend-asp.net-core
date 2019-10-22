using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrelloProject.BLL.Interfaces.RepositoriesInterfaces;
using TrelloProject.BLL.Interfaces.ServicesInterfaces;
using TrelloProject.DTOsAndViewModels.DTOs;
using TrelloProject.DTOsAndViewModels.ViewModels;

namespace TrelloProject.BLL.Services
{
    public class AccountService : IAccountService
    {
        private IAccountRepository _accountRepository;
        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<string> CreateUser(RegisterViewModel registerViewModel)
        {
            RegisterDTO registerDTO = new RegisterDTO();
            registerDTO.Email = registerViewModel.Email;
            registerDTO.FirstName = registerViewModel.FirstName;
            registerDTO.LastName = registerViewModel.LastName;
            registerDTO.Password = registerViewModel.Password;

            var createdUserId = await _accountRepository.CreateUser(registerDTO);
            return createdUserId;
        }

        public async Task<bool> Login(LoginViewModel loginViewModel)
        {
            LoginDTO loginDTO = new LoginDTO();

            loginDTO.Email = loginViewModel.Email;
            loginDTO.Password = loginViewModel.Password;

            var result = await _accountRepository.Login(loginDTO);
            return result;
        }

    }
}
