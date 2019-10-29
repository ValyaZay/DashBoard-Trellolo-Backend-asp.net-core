using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrelloProject.BLL.Interfaces.ServicesInterfaces;
using TrelloProject.DTOsAndViewModels.DTOs;
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
        
        public List<BgColorViewModel> Get()
        {
            return backgroundColorDTOService.GetAllBgColors();
        }
    }
}
