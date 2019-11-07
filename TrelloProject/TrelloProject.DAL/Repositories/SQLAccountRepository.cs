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
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SQLAccountRepository(UserManager<User> userManager,
                                   SignInManager<User> signInManager,
                                   RoleManager<IdentityRole> roleManager,
                                   IConfiguration configuration) 
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
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
                                

                if (!result.Succeeded)
                {
                    return new AuthenticationResult
                    {
                        Errors = result.Errors.Select(x => x.Description)
                    };
                }
                //here may be added a custom Claim in order to Authorize by a custom claim

                await _userManager.AddToRoleAsync(user, "User");
                return await GenerateAuthenticationResultForUser(user);
            }
            catch (Exception innerEx)
            {
                throw new ApiException(400, innerEx, 16); // registering and adding to a role should be in the separate methods?
            }
        }

        private async Task<AuthenticationResult> GenerateAuthenticationResultForUser(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtSettingsFromConfig = _configuration.GetSection("JwtSettings").Get<JwtSettings>();

            var key = Encoding.ASCII.GetBytes(jwtSettingsFromConfig.Secret);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("id", user.Id)
            };

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
                var role = await _roleManager.FindByNameAsync(userRole);
                if (role == null) continue;
                var roleClaims = await _roleManager.GetClaimsAsync(role);

                foreach (var roleClaim in roleClaims)
                {
                    if (claims.Contains(roleClaim))
                        continue;

                    claims.Add(roleClaim);
                }
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
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

        public async Task<AuthenticationResult> Login(LoginDTO loginDTO)
        {
            var existingUser = await _userManager.FindByEmailAsync(loginDTO.Email);

            if (existingUser == null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User with this email does not exist" }
                };
            }

            var userHasValidPassword = await _userManager.CheckPasswordAsync(existingUser, loginDTO.Password);

            if (!userHasValidPassword)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User/password combination is wrong" }
                };
            }

            return await GenerateAuthenticationResultForUser(existingUser);
        }
    }

    
}
