using System;
using System.Collections.Generic;
using System.Text;
using TrelloProject.DAL.Entities;

namespace TrelloProject.BLL.DTO
{
    public class BoardDTO
    {
        public int BoardId { get; set; }
        public string Title { get; set; }
        public int CurrentBackgroundColorId { get; set; }
        

        
    }
}
