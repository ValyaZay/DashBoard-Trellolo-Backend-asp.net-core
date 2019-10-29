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
    }
}
