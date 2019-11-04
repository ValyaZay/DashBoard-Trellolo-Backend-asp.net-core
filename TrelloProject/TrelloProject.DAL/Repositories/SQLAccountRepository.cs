using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TrelloProject.BLL.Interfaces.RepositoriesInterfaces;
using TrelloProject.DAL.EF;
using TrelloProject.DAL.Entities;
using TrelloProject.DTOsAndViewModels.DTOs;
using TrelloProject.DTOsAndViewModels.Exceptions;
using TrelloProject.DTOsAndViewModels.JWTauthentication;

namespace TrelloProject.DAL.Repositories
{
    internal class SQLAccountRepository : IAccountRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly JwtSettings _jwtSettings;
        private readonly IConfiguration _configuration;

        public SQLAccountRepository(UserManager<User> userManager,
                                   SignInManager<User> signInManager,
                                   
                                   IConfiguration configuration) 
        {
            _userManager = userManager;
            _signInManager = signInManager;
            
            _configuration = configuration;
        }
        

        public async Task<AuthenticationResult> CreateUser(RegisterDTO registerDTO)
        {
            var existingUser = await _userManager.FindByEmailAsync(registerDTO.Email);

            if(existingUser != null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User with this email already exists." }
                };
            }

            User user = new User();
            user.UserName = registerDTO.Email;
            user.Email = registerDTO.Email;
            user.FirstName = registerDTO.FirstName;
            user.LastName = registerDTO.LastName;
            string password = registerDTO.Password;

            try
            {
                IdentityResult result = await _userManager.CreateAsync(user, password); 
                
                if(!result.Succeeded)
                {
                    return new AuthenticationResult
                    {
                        Errors = result.Errors.Select(x => x.Description)
                    };
                }

                await _userManager.AddToRoleAsync(user, "User");

                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtSettingsFromConfig = _configuration.GetSection("JwtSettings").Get<JwtSettings>();

                var key = Encoding.ASCII.GetBytes(jwtSettingsFromConfig.Secret);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Email, user.Email),
                        new Claim("id", user.Id)
                    }),
                    Expires = DateTime.UtcNow.AddHours(2),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);

                return new AuthenticationResult
                {
                    Success = true,
                    Token = tokenHandler.WriteToken(token),
                };
            }
            catch (Exception innerEx)
            {
                throw new ApiException(400, innerEx, 16); // registering and adding to a role should be in the separate methods?
            }
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
