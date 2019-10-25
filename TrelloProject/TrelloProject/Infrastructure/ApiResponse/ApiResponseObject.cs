using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrelloProject.WEB.Infrastructure.ApiError;

namespace TrelloProject.WEB.Infrastructure.ApiResponse
{
    public class ApiResponseObject
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public int CustomErrorCode { get; set; }
        public string CustomErrorValue { get; set; }
        public object Result { get; set; }

        public ApiResponseObject(int statuscode, string message, int errorcode, string errorvalue, object result)
        {
            StatusCode = statuscode;
            Message = message;
            CustomErrorCode = errorcode;
            CustomErrorValue = errorvalue;
            Result = result;
        }
    }
}
