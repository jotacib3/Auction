using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;


namespace Entities.Models
{
    public class User : IdentityUser
    {
        public bool Enabled { get; set; }
        public string Avatar { get; set; } 

        public EmployeeData EmployeeData { get; set; }
        public State State { get; set; }
        public City City { get; set; }

        public List<Offer> Offer { get; set; }
        public List<Publication> Publication { get; set; }
    }
}
