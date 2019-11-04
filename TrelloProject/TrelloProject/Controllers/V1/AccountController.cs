using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrelloProject.BLL.Interfaces.ServicesInterfaces;
using TrelloProject.DTOsAndViewModels.Exceptions;
using TrelloProject.DTOsAndViewModels.JWTauthentication;
using TrelloProject.DTOsAndViewModels.ViewModels;
using TrelloProject.WEB.Contracts.V1;
using TrelloProject.WEB.Infrastructure.ApiResponse;

namespace TrelloProject.WEB.Controllers.V1
{
    [Produces("application/json")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost(ApiRoutes.Account.Register)]
        [ProducesResponseType(typeof(int), 201)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<ApiResponseSuccess> Create([FromBody] RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                throw new ApiException(400, 3);
            }
            var authResponse = await _accountService.CreateUser(registerViewModel);
            if (!authResponse.Success)
            {
                AuthFail fail = new AuthFail()
                {
                    Errors = authResponse.Errors
                };

                return new ApiResponseSuccess(200, 16, fail);
            }
            return new ApiResponseSuccess(200, 15, new AuthSuccess { Token = authResponse.Token });
            
        }

        [HttpPost(ApiRoutes.Account.Login)]
        [ProducesResponseType(typeof(int), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.Login(loginViewModel);
                if (result)
                {
                    return Ok(loginViewModel);
                }
            }
            return BadRequest("Login or password are not correct");
        }
    }
}
