using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Helpers
{
    public class OfferParams: PagedParams
    {
        public string Role { get; set; }
        public string Id { get; set; }
    }
}
