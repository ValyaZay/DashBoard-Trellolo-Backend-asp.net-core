using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrelloProject.WEB.Infrastructure.ApiResponse
{
    public class ApiResponseBase
    {
        public int StatusCode { get; set; }
        public int CustomCode { get; set; }
        public string CustomCodeMessage { get; set; }
        public object Result { get; set; }

        public ApiResponseBase(int statusCode, int customCode, string customCodeMessage, object result)
        {
            StatusCode = statusCode;
            CustomCode = customCode;
            CustomCodeMessage = customCodeMessage;
            Result = result;
        }

        public ApiResponseBase(int statuscode, int customCode, object result)
        {
            StatusCode = statuscode;
            CustomCode = customCode;
            Result = result;
        }
        public ApiResponseBase()
        {

        }
    }
}
