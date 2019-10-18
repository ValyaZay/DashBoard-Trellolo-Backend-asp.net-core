using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrelloProject.BLL.Interfaces.RepositoriesInterfaces;
using TrelloProject.BLL.Interfaces.ServicesInterfaces;
using TrelloProject.DTOsAndViewModels.DTOs;
using TrelloProject.DTOsAndViewModels.ViewModels;

namespace TrelloProject.BLL.Services
{
    public class AdministrationService : IAdministrationService
    {
        private readonly IAdministrationRepository administrationRepository;

        public AdministrationService(IAdministrationRepository administrationRepository)
        {
            this.administrationRepository = administrationRepository;
        }
        public async Task<string> CreateRole(CreateRoleViewModel createRoleViewModel)
        {
            CreateRoleDTO createRoleDTO = new CreateRoleDTO();
            createRoleDTO.Name = createRoleViewModel.Name;

            string id = await administrationRepository.CreateRole(createRoleDTO);
            return id;
        }
    }
}
