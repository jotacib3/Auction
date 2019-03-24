using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    public class Publication : IEntity
    {
        public int Id { get; set; }
        public int InvoiceNumber { get; set; }
        public int Mileage { get; set; }
        public string SerialNumber { get; set; }
        public string InsideColor { get; set; }
        public string OutsideColor { get; set; }
        public double Price { get; set; }
        public string EquipmentDetails { get; set; }
        public bool? Enabled { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }

        public int BrandId { get; set; }
        public int ModelId { get; set; }
        public int FuelId { get; set; }
        public int LocationId { get; set; }
        public int PackId { get; set; }
        public int TransmissionId { get; set; }
        public int VersionId { get; set; }
        public int YearId { get; set; }
        public int DoorsNumberId { get; set; }

        public string UserId { get; set; }

        public Brand Brand { get; set; }
        public Model Model { get; set; }
        public Fuel Fuel { get; set; }
        public Location Location { get; set; }
        public Pack Pack { get; set; }
        public Transmission Transmission { get; set; }
        public Version Version { get; set; }
        public Year Year { get; set; }
        public DoorsNumber DoorsNumber { get; set; }
        public List<Photo> Photos { get; set; }
        public User User { get; set; }
        public List<Offer> Offer { get; set; }
    }
}
