using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrelloProject.WEB.Infrastructure.ApiResponse
{
    public class ApiResponseNotSuccess : ApiResponseBase
    {
        public string ExceptionMessage { get; set; }
             

        public ApiResponseNotSuccess(int statusCode, string exceptionMessage, int customCode, string customCodeMessage, object result)
        {
            StatusCode = statusCode;
            CustomCode = customCode;
            ExceptionMessage = exceptionMessage;
            CustomCodeMessage = customCodeMessage;
            Result = result;
        }

        public ApiResponseNotSuccess(int statusCode, string exceptionMessage, int customCode, string customCodeMessage)
        {
            StatusCode = statusCode;
            CustomCode = customCode;
            ExceptionMessage = exceptionMessage;
            CustomCodeMessage = customCodeMessage;
           
        }

        public ApiResponseNotSuccess(int statusCode, int customCode, string customCodeMessage, object result)
        {
            StatusCode = statusCode;
            CustomCode = customCode;
            CustomCodeMessage = customCodeMessage;
            Result = result;
        }

        public ApiResponseNotSuccess(int statusCode, int customCode, object result)
        {
            StatusCode = statusCode;
            CustomCode = customCode;
            Result = result;
        }

        public ApiResponseNotSuccess(int statuscode, int customCode)
        {
            StatusCode = statuscode;
            CustomCode = customCode;
        }

        public ApiResponseNotSuccess()
        {

        }
    }
}
