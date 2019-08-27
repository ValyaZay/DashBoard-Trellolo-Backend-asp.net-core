﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TrelloProject.DAL.Entities
{
    public class Board
    {
        public int BoardId { get; set; }
        public string Title { get; set; }

        public IList<UserBoard> UserBoards { get; set; }

        public int CurrentBackgroundColorId { get; set; }
        public BackgroundColor BackgroundColor { get; set; }

        public IList<CardList> CardLists { get; set; }
    }
}