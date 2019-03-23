using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class EmployeeData : IEntity
    {
        public int Id { get; set; }
        public string NoEmployee { get; set; }
        public string Name { get; set; }
        public string FatherSurname { get; set; }
        public string MotherSurname { get; set; }
        public string FixNumber { get; set; }
        public string CellNumber { get; set; }
        public string Street { get; set; }
        public string Colony { get; set; }
        public string OutsideNumber { get; set; }
        public string InsideNumber { get; set; }
        public string ZipCode { get; set; }

        public int StateId { get; set; }
        public int PhotoId { get; set; }
        public int CityId { get; set; }
        public string UserId { get; set; }

        public Photo Photo { get; set; }
        public State State { get; set; }
        public City City { get; set; }
        [JsonIgnore]
        public User User { get; set; }

    }
}
