using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TrelloProject.DAL.Entities
{
    internal class CardComment
    {
        public int CardCommentId { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
       
        public int? RefersToId { get; set; }

        public Card Card { get; set; }
        public int CardId { get; set; }

        public CardComment RefersTo { get; set; }

        [ForeignKey("CreatedById")]
        public User CreatedBy { get; set; }

        public int CreatedById { get; set; }

    }
}
