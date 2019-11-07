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
        [ProducesResponseType(typeof(int), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<ApiResponseBase> Create([FromBody] RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                AuthFail modelError = new AuthFail
                {
                    Errors =  ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage))
                };
                throw new ApiException(400, 3, modelError);
            }
            var authResponse = await _accountService.CreateUser(registerViewModel);
            if (!authResponse.Success)
            {
                AuthFail fail = new AuthFail()
                {
                    Errors = authResponse.Errors
                };

                return new ApiResponseBase(400, 16, fail);
            }
            return new ApiResponseBase(200, 15, new AuthSuccess { Token = authResponse.Token });
            
        }

        [HttpPost(ApiRoutes.Account.Login)]
        [ProducesResponseType(typeof(int), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<ApiResponseBase> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                AuthFail modelError = new AuthFail
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage))
                };
                throw new ApiException(400, 3, modelError);
            }
            var authResponse = await _accountService.Login(loginViewModel);
            if (!authResponse.Success)
            {
                AuthFail fail = new AuthFail()
                {
                    Errors = authResponse.Errors
                };

                return new ApiResponseBase(400, 18, fail);
            }
            return new ApiResponseBase(200, 17, new AuthSuccess { Token = authResponse.Token });
        }
    }
}
