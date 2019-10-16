using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrelloProject.DTOsAndViewModels.Exceptions
{
    public class BgColorDoesNotExistException : Exception
    {
        public BgColorDoesNotExistException()
        {
          
        }

        public BgColorDoesNotExistException(string message)
            : base(message)
        {
                       
        }

        public BgColorDoesNotExistException(string message, Exception inner)
            : base(message, inner)
        {
        
        }
    }
}
