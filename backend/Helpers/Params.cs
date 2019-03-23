using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Helpers
{
    public class Params
    {
        //Roles Params
        public const string ROLE_EMPLOYEE = "EMPLOYEE";
        public const string ROLE_DISTRIBUTOR = "DISTRIBUTOR";
        public const string ROLE_ADMIN = "ADMINAMDGM";
        public const string ROLE_ADMINAMDGM = "ADMINAMDGM";

        //PAgianton Params
        public const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int pageSize = 10;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize) ? pageSize : value; }
        }
    }
}
