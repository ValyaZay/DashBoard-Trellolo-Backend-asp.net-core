using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TrelloProject.DTOsAndViewModels.ViewModels
{
    public class BoardUpdateViewModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Title cannot exceed 50 characters")]
        public string Title { get; set; }
        public int CurrentBackgroundColorId { get; set; }
    }
}
