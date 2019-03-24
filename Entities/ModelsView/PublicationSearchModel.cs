using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ModelsView
{
    public class PublicationSearchModel
    {
        public int Id { get; set; }
        public int? InvoiceNumber { get; set; }
        public int? MinMileage { get; set; }
        public int? MaxMileage { get; set; }
        public string SerialNumber { get; set; }
        public string InsideColor { get; set; }
        public string OutsideColor { get; set; }
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }

        public int? BrandId { get; set; }
        public int? ModelId { get; set; }
        public int? FuelId { get; set; }
        public int? LocationId { get; set; }
        public int? PackId { get; set; }
        public int? TransmissionId { get; set; }
        public int? VersionId { get; set; }
        public int? YearId { get; set; }
        public int? DoorsNumberId { get; set; }

        public string UserId { get; set; }

    }
}
