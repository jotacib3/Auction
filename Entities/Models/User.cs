using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace Entities.Models
{
    public class User : IdentityUser
    {
        public bool? Enabled { get; set; }

        public EmployeeData EmployeeData { get; set; }
        public State State { get; set; }
        public City City { get; set; }

        [JsonIgnore]
        public List<Offer> Offer { get; set; }
        [JsonIgnore]
        public List<Publication> Publication { get; set; }
    }
}
