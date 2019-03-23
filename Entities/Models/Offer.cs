using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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
        public int PublicationId { get; set; }
        public string UserId { get; set; }
        public bool? Enabled { get; set; }

        public Publication Publication { get; set; }
        public User User { get; set; }


    }
}
