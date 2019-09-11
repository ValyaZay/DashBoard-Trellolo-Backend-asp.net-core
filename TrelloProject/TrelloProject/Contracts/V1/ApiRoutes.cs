using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrelloProject.WEB.Contracts.V1
{
    public static class ApiRoutes
    {
        public const string Root = "api";

        public const string Version = "v1";

        public const string Base = Root + "/" + Version;
        public static class Board
        {
            public const string GetAll = Base + "/board";
            //public const string GetById = Base + "/board" + {BoardId}
        }
    }
}
