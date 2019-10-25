using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrelloProject.WEB.Infrastructure.CustomMiddlware;

namespace TrelloProject.WEB.Infrastructure.Extensions
{
    public static class ApiExtensions
    {
        public static IApplicationBuilder AddApiResponseProcessingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ApiResponseProcessingMiddleware>();
        }
    }
}
