using System;
using System.Collections.Generic;
using System.Text;

namespace TrelloProject.DTOsAndViewModels.DTOs
{
    public class BoardBgDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int BgColorId { get; set; }
        public string BgColorName { get; set; }
        public string BgColorHex { get; set; }
    }
}
