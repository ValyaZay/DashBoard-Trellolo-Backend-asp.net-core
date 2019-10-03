using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrelloProject.BLL.Interfaces.ServicesInterfaces;
using TrelloProject.DTOsAndViewModels.DTOs;
using TrelloProject.DTOsAndViewModels.OtherModels;
using TrelloProject.DTOsAndViewModels.ViewModels;
using TrelloProject.WEB.Contracts.V1;

namespace TrelloProject.WEB.Controllers.V1
{
    [Produces("application/json")]
    public class BgColorController : Controller
    {
        private readonly IBackgroundColorDTOService backgroundColorDTOService;

        public BgColorController(IBackgroundColorDTOService backgroundColorDTOService)
        {
            this.backgroundColorDTOService = backgroundColorDTOService;
        }

        [HttpGet(ApiRoutes.BackgroundColor.GetAll)]
        [ProducesResponseType(typeof(List<BgColorViewModel>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public IActionResult Get()
        {
            try
            {
                var bgColorsViewModels = backgroundColorDTOService.GetAllBgColors();
                int bgColorsCount = bgColorsViewModels.Count();
                if (bgColorsCount == 0)
                {
                    return Ok("There are no BgColor created.");
                }
                else
                {
                    return Ok(bgColorsViewModels);

                }
            }
            catch (Exception)
            {
                return BadRequest(new ApiResponse { Status = false });
            }



        }
    }
}
