using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Text;
using TrelloProject.DTOsAndViewModels.DTOs;

namespace TrelloProject.DTOsAndViewModels.OtherModels
{
    public class ApiResponse
    {
        public bool Status { get; set; }
        //public BoardDTO BoardDTO { get; set; }
        public ModelStateDictionary ModelState { get; set; }
    }
}
