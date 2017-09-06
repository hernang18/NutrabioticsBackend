namespace NutraBioticsBackend.Models
{
    using System;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class InvoiceHeader
    {
        [Key]
        public int InvoiceHeaderId { get; set; }

        public string Company { get; set; }

        public int CustNum { get; set; }

        public string CustID { get; set; }

        public string Names { get; set; }

        public string InvoiceNum { get; set; }

        public string InvoiceType { get; set; }

        public decimal InvoiceAmt { get; set; }

        public decimal InvoiceBal { get; set; }

        public bool CheckSaldo { get; set; }

        public DateTime? InvoiceDate { get; set; }
      
        public DateTime? ClosedDate { get; set; }

        public DateTime? DueDate { get; set; }

        public int DiasVencimiento { get; set; }

        public bool OpenInvoice { get; set; }

        public int VendorId { get; set; }

    }
}