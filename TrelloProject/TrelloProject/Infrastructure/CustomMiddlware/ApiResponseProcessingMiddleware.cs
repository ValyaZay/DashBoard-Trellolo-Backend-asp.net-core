using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
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
            var originalBodyStream = context.Response.Body;

            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;

                try
                {
                    await _next.Invoke(context);
                    var body = await FormatResponse(context.Response);
                    await HandleResponse(context, body);
                }
                catch (Exception ex)
                {
                    await HandleException(context, ex);
                }
                finally
                {
                    responseBody.Seek(0, SeekOrigin.Begin);
                    await responseBody.CopyToAsync(originalBodyStream);
                }
            }
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var plainBodyText = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);
            return plainBodyText;
        }

        private Task HandleException(HttpContext context, Exception exception)
        {
            
            int statusCode = 0;
            int customCode = 0;
            object result = null;
            if(exception is ApiException)
            {
                var ex = exception as ApiException;
                customCode = ex.ErrorCode;
                statusCode = ex.StatusCode;
                context.Response.StatusCode = statusCode;
                result = ex.ModelError;
            }
            else
            {
                customCode = 14;
                statusCode = 500;
                context.Response.StatusCode = statusCode;

            }

            ApiCustomCodeEnglishList codeListObj = new ApiCustomCodeEnglishList();
            var list = codeListObj.ApiCodeEnglishList;
            ApiCustomCodeObject apiCustomCodeObject = new ApiCustomCodeObject();
            apiCustomCodeObject = list.Where(item => item.CustomCode == customCode).FirstOrDefault();
            

            string customCodeMessage = "";
            try
            {
                customCodeMessage = apiCustomCodeObject.Message;
            }
            catch(Exception ex)//just an experiment if the error code does not exist in the error list
            {
                string customCodeMes = "An error with such a code does not exist";
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ApiResponseNotSuccess apiRespons = new ApiResponseNotSuccess(statusCode, ex.Message, customCode, customCodeMes);
                var jsond = JsonConvert.SerializeObject(apiRespons);
                context.Response.ContentType = "application/json";
                return context.Response.WriteAsync(jsond);
            }

            ApiResponseNotSuccess apiResponse = new ApiResponseNotSuccess(statusCode, exception.Message, customCode, customCodeMessage, result);
            var json = JsonConvert.SerializeObject(apiResponse);
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(json);
        }

        private Task HandleResponse(HttpContext context, object body)
        {
            context.Response.ContentType = "application/json";

            ApiCustomCodeEnglishList customCodeList = new ApiCustomCodeEnglishList();
            var list = customCodeList.ApiCodeEnglishList;
            ApiCustomCodeObject customCodeObject = new ApiCustomCodeObject();

            string bodyText = body.ToString();
            dynamic parsed = JObject.Parse(bodyText);
            int statusCode = parsed.statusCode;
            object result = parsed.result;
            int customCode = parsed.customCode;

            customCodeObject = list.Where(item => item.CustomCode == customCode).FirstOrDefault();
            string customCodeMessage = customCodeObject.Message; 

            ApiResponseSuccess apiResponse = new ApiResponseSuccess(statusCode, customCode, customCodeMessage, result);
            var json = JsonConvert.SerializeObject(apiResponse);
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(json);
        }
    }
}
