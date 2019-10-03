using System;
using System.Collections.Generic;
using System.Text;

namespace TrelloProject.DTOsAndViewModels.ViewModels
{
    public class BoardBgViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string BgColorName { get; set; }
        public string BgColorHex { get; set; }
    }
}
