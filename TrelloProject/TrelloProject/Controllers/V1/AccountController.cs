using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrelloProject.BLL.Interfaces.ServicesInterfaces;
using TrelloProject.DTOsAndViewModels.ViewModels;
using TrelloProject.WEB.Contracts.V1;

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
        public async Task<IActionResult> Create([FromBody] RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Insert valid data");//object error should be returned
            }
            try
            {
                string id = await _accountService.CreateUser(registerViewModel);
                if (id == null)
                {
                    return BadRequest("User is not created");
                }
                else
                {
                    var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
                    var locationUri = baseUrl + "/" + ApiRoutes.Account.GetRegisteredUserById.Replace("{id}", id);
                    return Created(locationUri, id);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
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
