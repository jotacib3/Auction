using System;
using System.Collections.Generic;
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
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public string BrandId { get; set; }
        public string ModelId { get; set; }
        public string FuelId { get; set; }
        public string LocationId { get; set; }
        public string PackId { get; set; }
        public string TransmissionId { get; set; }
        public string VersionId { get; set; }
        public string YearId { get; set; }
        public string DoorsNumberId { get; set; }

        public Guid FrontSideImageId{ get; set; }
        public Guid RightSideImage { get; set; }
        public Guid LeftSideImage { get; set; }
        public Guid BackSideImage { get; set; }
        public Guid InsideImage { get; set; }


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

        public List<Offer> Offer { get; set; }
    }
}
