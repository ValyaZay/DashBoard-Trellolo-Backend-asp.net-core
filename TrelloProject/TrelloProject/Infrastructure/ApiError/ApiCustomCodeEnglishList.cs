using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrelloProject.WEB.Infrastructure.ApiError
{
    public class ApiCustomCodeEnglishList
    {
        private List<ApiCustomCodeObject> apiCodeEnglishList;
        public List<ApiCustomCodeObject> ApiCodeEnglishList
        {
            get
            {
                return apiCodeEnglishList;
            }
           
        }

        public ApiCustomCodeEnglishList()
        {
            apiCodeEnglishList = new List<ApiCustomCodeObject>()
                {
                    new ApiCustomCodeObject{ CustomCode = 0, Message = "No error checked" },
                    new ApiCustomCodeObject{ CustomCode = 1, Message = "Board is not created." },
                    new ApiCustomCodeObject{ CustomCode = 2, Message = "Board title already exists." },
                    new ApiCustomCodeObject{ CustomCode = 3, Message = "Insert valid data."},
                    new ApiCustomCodeObject{ CustomCode = 4, Message = "Board is not updated."},
                    new ApiCustomCodeObject{ CustomCode = 5, Message = "Background color does not exist."},
                    new ApiCustomCodeObject{ CustomCode = 6, Message = "Board does not exist"},
                    new ApiCustomCodeObject{ CustomCode = 7, Message = "Board is not deleted"},
                    new ApiCustomCodeObject{ CustomCode = 8, Message = "No background color is registered"},
                    new ApiCustomCodeObject{ CustomCode = 9, Message = "Board is not deleted"},
                    new ApiCustomCodeObject{ CustomCode = 10, Message = "No board exists"},
                    new ApiCustomCodeObject{ CustomCode = 11, Message = "Board is created."},
                    new ApiCustomCodeObject{ CustomCode = 12, Message = "Board is updated."},
                    new ApiCustomCodeObject{ CustomCode = 13, Message = "Board is deleted."},
                    new ApiCustomCodeObject{ CustomCode = 14, Message = "Internal server error"},
                    new ApiCustomCodeObject{ CustomCode = 15, Message = "User is registered successfully."},
                    new ApiCustomCodeObject{ CustomCode = 16, Message = "User is not registered."},
                    new ApiCustomCodeObject{ CustomCode = 17, Message = "User logged in successfully"},
                    new ApiCustomCodeObject{ CustomCode = 18, Message = "User is not logged in"},
                    new ApiCustomCodeObject{ CustomCode = 19, Message = "User is not authorized"},
                    new ApiCustomCodeObject{ CustomCode = 20, Message = "You do not own this board"}
                };
        }
        
            
    }
}
