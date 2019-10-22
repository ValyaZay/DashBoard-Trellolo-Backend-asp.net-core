using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrelloProject.BLL.Interfaces.RepositoriesInterfaces;
using TrelloProject.DAL.EF;
using TrelloProject.DAL.Entities;
using TrelloProject.DTOsAndViewModels.DTOs;

namespace TrelloProject.DAL.Repositories
{
    internal class SQLAccountRepository : IAccountRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public SQLAccountRepository(UserManager<User> userManager,
                                   SignInManager<User> signInManager) 
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        

        public async Task<string> CreateUser(RegisterDTO registerDTO)
        {
            User user = new User();
            user.UserName = registerDTO.Email;
            user.Email = registerDTO.Email;
            user.FirstName = registerDTO.FirstName;
            user.LastName = registerDTO.LastName;
            

            string password = registerDTO.Password;

            try
            {
                IdentityResult result = await _userManager.CreateAsync(user, password);
                await _userManager.AddToRoleAsync(user, "User");
            }
            catch (Exception)
            {
                
            }
            return user.Id;
        }

        public async Task<bool> Login(LoginDTO loginDTO)
        {
            User user = new User();
            user.UserName = loginDTO.Email;
            string password = loginDTO.Password;
            try
            {
                SignInResult result = await _signInManager.PasswordSignInAsync(user.UserName, password, false, false);
                
                if (result.Succeeded)
                {
                    return true;
                }
            }
            catch (Exception)
            {

            }

            return false;           
        }
    }

    
}
