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
        public SQLAccountRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
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
    }
}
