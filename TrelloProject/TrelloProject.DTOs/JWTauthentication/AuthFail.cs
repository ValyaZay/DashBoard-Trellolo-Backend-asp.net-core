using System;
using System.Collections.Generic;
using System.Text;

namespace TrelloProject.DTOsAndViewModels.JWTauthentication
{
    public class AuthFail
    {
        public IEnumerable<string> Errors { get; set; }
    }
}
