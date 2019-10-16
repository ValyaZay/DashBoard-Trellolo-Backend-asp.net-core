using System;
using System.Collections.Generic;
using System.Text;

namespace TrelloProject.DTOsAndViewModels.Exceptions
{
    public class BoardTitleAlreadyExists : Exception
    {
     //override Message somehow  
        public BoardTitleAlreadyExists()
        {

        }

        public BoardTitleAlreadyExists(string message)
            : base(message)
        {
            
        }

    

        public BoardTitleAlreadyExists(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}
