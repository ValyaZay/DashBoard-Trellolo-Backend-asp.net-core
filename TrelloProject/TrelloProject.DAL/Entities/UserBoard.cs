﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TrelloProject.DAL.Entities
{
    public class UserBoard
    {
        
        public int UserId { get; set; }
        public User User { get; set; }
        

        public int BoardId { get; set; }
        public Board Board { get; set; }
        
    }
}
