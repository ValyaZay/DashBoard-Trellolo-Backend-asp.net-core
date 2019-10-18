using System;
using System.Collections.Generic;
using System.Text;

namespace TrelloProject.DTOsAndViewModels.Exceptions
{
    public class BoardIsNotCreated : Exception
    {
        public BoardIsNotCreated()
        {

        }

        public BoardIsNotCreated(string message)
            : base(message)
        {


        }

        public BoardIsNotCreated(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}
