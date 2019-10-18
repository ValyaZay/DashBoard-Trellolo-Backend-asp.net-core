using Microsoft.AspNetCore.Identity;
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
    public class AdministrationController : Controller
    {
        private readonly IAdministrationService administrationService;

        public AdministrationController(IAdministrationService administrationService)
        {
            this.administrationService = administrationService;
        }

        [HttpPost(ApiRoutes.Administration.CreateRole)]
        [ProducesResponseType(typeof(int), 201)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleViewModel createRoleViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Insert valid data");//object error should be returned
            }
            try
            {
                string id = await administrationService.CreateRole(createRoleViewModel);
                if (id == null)
                {
                    return BadRequest("Role is not created");
                }
                else
                {
                    var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
                    var locationUri = baseUrl + "/" + ApiRoutes.Administration.GetRoleById.Replace("{id}", id);
                    return Created(locationUri, id);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
