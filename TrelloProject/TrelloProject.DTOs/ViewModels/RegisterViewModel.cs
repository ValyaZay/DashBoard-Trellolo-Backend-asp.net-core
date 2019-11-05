using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TrelloProject.DTOsAndViewModels.ViewModels
{
    public class RegisterViewModel
    {
        public string Id { get; set; }
       
        [Required]
        [EmailAddress]
        public string  Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
