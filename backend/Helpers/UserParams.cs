using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Helpers
{
    public class UserParams: PagedParams
    {
        //Roles Params
        public const string ROLE_EMPLOYEE = "EMPLOYEE";
        public const string ROLE_DISTRIBUTOR = "DISTRIBUTOR";
        public const string ROLE_ADMIN = "ADMINAMDGM";
        public const string ROLE_ADMINAMDGM = "ADMINAMDGM";

        //Filter
        public string Role { get; set; }
    }
}
