using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrelloProject.WEB.Infrastructure.ApiError;

namespace TrelloProject.WEB.Infrastructure.ApiResponse
{
    public class ApiResponseSuccess : ApiResponseBase
    {
        public object Result { get; set; }

        public ApiResponseSuccess(int statusCode, int customCode, string customCodeMessage, object result)
        {
            StatusCode = statusCode;
            CustomCode = customCode;
            CustomCodeMessage = customCodeMessage;
            Result = result;
        }

        public ApiResponseSuccess(int statuscode, int customCode, object result)
        {
            StatusCode = statuscode;
            CustomCode = customCode;
            Result = result;
        }
        public ApiResponseSuccess()
        {
           
        }
    }
}
