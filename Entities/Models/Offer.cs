using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class Offer : IEntity
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public DateTime? Created { get; set; }
        public Guid VehicleId { get; set; }
        public string UserId { get; set; }
        public bool? Enabled { get; set; }

        public Guid PublicationId { get; set; }
        public User User { get; set; }
    }
}
