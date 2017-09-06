namespace NutraBioticsBackend.Models
{
    using System;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class InvoiceDetail
    {
        [Key]
        public int InvoiceDetailId { get; set; }

        public int InvoiceHeaderId { get; set; }

        public string InvoiceType { get; set; }

        public string InvoiceNum { get; set; }

        public int InvoiceLine { get; set; }

        public string PartNum { get; set; }

        public string PartDescription { get; set; }

        public decimal OurShipQty { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal ExtPrice { get; set; }

        public decimal TaxAmt { get; set; }

        public decimal DocTaxAmt { get; set; }

        public int VendorId { get; set; }
    }
}