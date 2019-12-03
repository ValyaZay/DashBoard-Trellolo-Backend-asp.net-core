using System;
using System.Collections.Generic;
using System.Text;
using TrelloProject.BLL.Interfaces.RepositoriesInterfaces;
using TrelloProject.BLL.Interfaces.ServicesInterfaces;
using TrelloProject.DTOsAndViewModels.DTOs;
using TrelloProject.DTOsAndViewModels.Exceptions;
using TrelloProject.DTOsAndViewModels.ViewModels;

namespace TrelloProject.BLL.Services
{
    public class BackgroundColorDTOService : IBackgroundColorDTOService
    {
        private readonly IBackgroundColorDTORepository backgroundColorDTORepository;

        public BackgroundColorDTOService(IBackgroundColorDTORepository backgroundColorDTORepository)
        {
            this.backgroundColorDTORepository = backgroundColorDTORepository;
        }
        public List<BgColorViewModel> GetAllBgColors()
        {
            List<BgColorViewModel> bgColorViewModels = new List<BgColorViewModel>();
            try
            {
                var bgColorsDTOs = backgroundColorDTORepository.GetAllBgColors();
                foreach(var bgColorDTO in bgColorsDTOs)
                {
                    BgColorViewModel bgColorViewModel = new BgColorViewModel();
                    bgColorViewModel.Id = bgColorDTO.Id;
                    bgColorViewModel.ColorHex = bgColorDTO.ColorHex;
                    bgColorViewModel.ColorName = bgColorDTO.ColorName;
                    bgColorViewModels.Add(bgColorViewModel);
                }
                return bgColorViewModels;
            }
            catch (Exception innerEx)
            {
                throw new ApiException(404, innerEx, 8);
            }
        }
    }
}
