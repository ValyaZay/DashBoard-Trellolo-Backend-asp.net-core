using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TrelloProject.DAL.Entities
{
    public class Card
    {
        public int CardId { get; set; }
        public string  Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Hidden { get; set; }

        public CardList CardList { get; set; }
        public int CardListId { get; set; }

        public IList<CardComment> CardComments { get; set; }

        [ForeignKey("CreatedById")]
        public User CreatedBy { get; set; }
        
        public int CreatedById { get; set; }

        [ForeignKey("AssigneeId")]
        public User Assignee { get; set; }
        public int? AssigneeId { get; set; }

    }
}
