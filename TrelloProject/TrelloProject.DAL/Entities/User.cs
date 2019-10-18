using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace TrelloProject.DAL.Entities
{
    internal class User : IdentityUser
    {
        //public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public string Email { get; set; }

        public IList<UserBoard> UserBoards { get; set; }

        public IList<Card> CardsCreated { get; set; }
        public IList<Card> CardsAssigned { get; set; }

        public IList<CardComment> CardComments { get; set; }
    }
}
