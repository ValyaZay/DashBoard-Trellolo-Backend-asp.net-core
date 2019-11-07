using System;
using System.Collections.Generic;
using System.Text;

namespace TrelloProject.DTOsAndViewModels.Exceptions
{
    public class ApiException : Exception
    {
        public int StatusCode { get; set; }
        //add message here to throw it with apiException and show it

        public int ErrorCode { get; set; }
        public object  ModelError { get; set; }

        public ApiException(int statuscode, Exception ex, int errorcode, object modelError)
            : base(ex.Message)
        {
            StatusCode = statuscode;
            ErrorCode = errorcode;
            ModelError = modelError;
        }

        public ApiException(int statuscode, Exception ex, int errorcode)
            : base(ex.Message)
        {
            StatusCode = statuscode;
            ErrorCode = errorcode;
           
        }

        public ApiException(int statuscode, int errorcode, object modelError)
            : base()
        {
            StatusCode = statuscode;
            ErrorCode = errorcode;
            ModelError = modelError;

        }

        public ApiException(int statuscode, int errorcode)
            : base()
        {
            StatusCode = statuscode;
            ErrorCode = errorcode;
            
        }

        public ApiException()
        {

        }

        public ApiException(string message)
            : base(message)
        {

        }

        public ApiException(string message, Exception inner, object modelError)
            : base(message, inner)
        {

        }
    }
}
