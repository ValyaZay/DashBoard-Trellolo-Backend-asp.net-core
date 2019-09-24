using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrelloProject.DTOsAndViewModels.ViewModels
{
    public class BoardCreateViewModel
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Title cannot exceed 50 characters")]
        public string Title { get; set; }
        public int CurrentBackgroundColorId { get; set; }

        
    }
}
