using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrelloProject.BLL.Interfaces.RepositoriesInterfaces;
using TrelloProject.DTOsAndViewModels.DTOs;

namespace TrelloProject.DAL.Repositories
{
    internal class SQLAdministrationRepository : IAdministrationRepository
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public SQLAdministrationRepository(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        public async Task<string> CreateRole(CreateRoleDTO createRoleDTO)
        {
            IdentityRole role = new IdentityRole();
            role.Name = createRoleDTO.Name;

            try
            {
                IdentityResult result = await roleManager.CreateAsync(role);
                if(result.Succeeded)
                {
                    return role.Id;
                }
            }
            catch (Exception)
            {

            }
            return "Role is not created";
        }
    }
}
