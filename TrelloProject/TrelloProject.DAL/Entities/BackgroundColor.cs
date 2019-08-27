﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TrelloProject.DAL.Entities
{
    public class BackgroundColor
    {
        public int BackgroundColorId { get; set; }
        public string ColorHex { get; set; }
        public IList<Board> Boards { get; set; }
    }
}