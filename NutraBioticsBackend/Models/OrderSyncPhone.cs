using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NutraBioticsBackend.Models
{
    public class OrderSyncPhone
    {

        public int SalesOrderHeaderPhoneId { get; set; }

        public int SalesOrderHeaderInterId { get; set; }

        public decimal TaxAmt { get; set; }

        public decimal Total { get; set; }

        public int OrderNum { get; set; }  

        public int InvoiceNum { get; set; }

        public bool Facturado { get; set; }
    }
}