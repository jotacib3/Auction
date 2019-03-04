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

        public Guid StateId { get; set; }
        public Guid CityId { get; set; }

        public string UserId { get; set; }

    }
}
