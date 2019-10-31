using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrelloProject.WEB.Infrastructure.ApiResponse
{
    public class ApiResponseNotSuccess : ApiResponseBase
    {
        public string ExceptionMessage { get; set; }
              

        public ApiResponseNotSuccess(int statusCode, string exceptionMessage, int customCode, string customCodeMessage)
        {
            StatusCode = statusCode;
            CustomCode = customCode;
            ExceptionMessage = exceptionMessage;
            CustomCodeMessage = customCodeMessage;
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
