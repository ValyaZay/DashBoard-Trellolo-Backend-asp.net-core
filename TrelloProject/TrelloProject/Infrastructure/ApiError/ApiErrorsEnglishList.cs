using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrelloProject.WEB.Infrastructure.ApiError
{
    public class ApiErrorsEnglishList
    {
        private List<ApiErrorObject> apiErrorEnglishList;
        public List<ApiErrorObject> ApiErrorEnglishList
        {
            get
            {
                return apiErrorEnglishList;
            }
           
        }

        public ApiErrorsEnglishList()
        {
            apiErrorEnglishList = new List<ApiErrorObject>()
                {
                    new ApiErrorObject{ ErrorCode = 0, Message = "No error checked" },
                    new ApiErrorObject{ ErrorCode = 1, Message = "Board is not created." },
                    new ApiErrorObject{ ErrorCode = 2, Message = "Board title already exists." },
                    new ApiErrorObject{ ErrorCode = 3, Message = "Insert valid data."},
                    new ApiErrorObject{ ErrorCode = 4, Message = "Board is not updated."},
                    new ApiErrorObject{ ErrorCode = 5, Message = "Background color does not exist."},
                    new ApiErrorObject{ ErrorCode = 6, Message = "Board does not exist"},
                    new ApiErrorObject{ ErrorCode = 7, Message = "Board is not deleted"},
                    new ApiErrorObject{ ErrorCode = 8, Message = "No background color is registered"},
                    new ApiErrorObject{ ErrorCode = 9, Message = "Board is not deleted"},
                    new ApiErrorObject{ ErrorCode = 10, Message = "No board exists"}
                };
        }
        
            
    }
}
