using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ModelsView
{
    public class PublicationSearchModel
    {
        public Guid Id { get; set; }
        public int? InvoiceNumber { get; set; }
        public int? MinMileage { get; set; }
        public int? MaxMileage { get; set; }
        public string SerialNumber { get; set; }
        public string InsideColor { get; set; }
        public string OutsideColor { get; set; }
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }

        public string BrandId { get; set; }
        public string ModelId { get; set; }
        public string FuelId { get; set; }
        public string LocationId { get; set; }
        public string PackId { get; set; }
        public string TransmissionId { get; set; }
        public string VersionId { get; set; }
        public string YearId { get; set; }
        public string DoorsNumberId { get; set; }

        public string UserId { get; set; }

    }
}
