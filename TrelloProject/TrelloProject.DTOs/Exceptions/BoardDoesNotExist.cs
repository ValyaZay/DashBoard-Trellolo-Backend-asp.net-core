using System;
using System.Collections.Generic;
using System.Text;

namespace TrelloProject.DTOsAndViewModels.Exceptions
{
    public class BoardDoesNotExistException : Exception
    {
        private string customMessage;
        

        public override string Message
        {
            get { return "The background color with ID = ... does not exist"; }
        }

        public BoardDoesNotExistException()
        {
            
        }

        public BoardDoesNotExistException(string message)
            : base(message)
        {
            customMessage = message;
            
        }

        public BoardDoesNotExistException(string message, Exception inner)
            : base(message, inner)
        {
            customMessage = message;
            
        }
    }
}
