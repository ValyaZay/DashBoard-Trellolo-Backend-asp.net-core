using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TrelloProject.DTOsAndViewModels.Exceptions;
using TrelloProject.WEB.Infrastructure.ApiError;
using TrelloProject.WEB.Infrastructure.ApiResponse;

namespace TrelloProject.WEB.Infrastructure.CustomMiddlware
{
    public class ApiResponseProcessingMiddleware
    {
        private readonly RequestDelegate _next;
        

        public ApiResponseProcessingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var responseBody = context.Response.Body;
            int errorcode = 0;//how to pass correct error code from controller here?
            try
            {
                await _next.Invoke(context);
                await HandleResponse(context, errorcode);
            }
            catch(Exception ex)
            {
                await HandleException(context, ex);
            }
            //finally?
        }

        private Task HandleException(HttpContext context, Exception exception)
        {
            
            int statusCode = 0;
            int errorcode = 0;
            if(exception is ApiException)
            {
                var ex = exception as ApiException;
                errorcode = ex.ErrorCode;
                statusCode = ex.StatusCode;
                context.Response.StatusCode = statusCode;
            }
            
            ApiErrorsEnglishList errorList = new ApiErrorsEnglishList();
            var list = errorList.ApiErrorEnglishList;
            ApiErrorObject apiErrorObject = new ApiErrorObject();
            apiErrorObject = list.Where(item => item.ErrorCode == errorcode).FirstOrDefault();


            string errorvalue = "";
            try
            {
                errorvalue = apiErrorObject.Message;
            }
            catch(Exception ex)//just an expetiment if the error code does not exis in the error list
            {
                string erroval = "An error with such a code does not exist";
                object res = null;
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ApiResponseObject apiRespons = new ApiResponseObject(statusCode, ex.Message, errorcode, erroval, res);
                var jsond = JsonConvert.SerializeObject(apiRespons);
                context.Response.ContentType = "application/json";
                return context.Response.WriteAsync(jsond);
            }

            object result = null;

            ApiResponseObject apiResponse = new ApiResponseObject(statusCode, exception.Message, errorcode, errorvalue, result);
            var json = JsonConvert.SerializeObject(apiResponse);
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(json);
        }

        private Task HandleResponse(HttpContext context, int errorcode)
        {
            int statusCode = context.Response.StatusCode;
            ApiErrorsEnglishList errorList = new ApiErrorsEnglishList();
            var list = errorList.ApiErrorEnglishList;
            ApiErrorObject apiErrorObject = new ApiErrorObject();
            apiErrorObject = list.Where(item => item.ErrorCode == errorcode).FirstOrDefault();
            string errorvalue = apiErrorObject.Message;
            object result = context.Response.Body;

            string message = "";
             
            switch(statusCode)
            {
                case 200: message = "Success";
                    break;
                case 201: message = "Created";
                    break;
                case 204: message = "No Content";
                    break;
                case 400: message = "Bad Request";
                    break;
                case 404: message = "Not Found";
                    break;
                default: message = "Undefined request";
                    break;
            }
            
            ApiResponseObject apiResponse = new ApiResponseObject(statusCode, message, errorcode, errorvalue, result);
            var json = JsonConvert.SerializeObject(apiResponse);
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(json);
        }
    }
}
