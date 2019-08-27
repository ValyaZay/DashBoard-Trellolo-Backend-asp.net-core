using System;
using System.Collections.Generic;
using System.Text;

namespace TrelloProject.DAL.Entities
{
    public class CardList
    {
        public int CardListId { get; set; }
        public string Title { get; set; }
        public int Order { get; set; }

        public int BoardId { get; set; }
        public Board Board { get; set; }

        public IList<Card> Cards { get; set; }
    }
}
