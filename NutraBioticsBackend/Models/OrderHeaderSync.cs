namespace NutraBioticsBackend.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    public class OrderHeaderSync
    {
      
        public int SalesOrderHeaderId { get; set; }
      
        public int SalesOrderHeaderInterId { get; set; }

        public int SalesOrderHeaderPhoneId { get; set; }

        public int OrderNum { get; set; }  //Epicor

        public int UserId { get; set; }

        public int VendorId { get; set; }

        public int CustomerId { get; set; }

        public string CustId { get; set; }

        public bool CreditHold { get; set; }

        public DateTime Date { get; set; }

        public DateTime NeedByDate { get; set; }

        public string TermsCode { get; set; }

        public int ShipToId { get; set; }

        public int ContactId { get; set; }

        public int ConNum { get; set; }   //Epicor

        public string ShipToNum { get; set; }

        public string SalesCategory { get; set; }

        public string Observations { get; set; }

        public decimal TaxAmt { get; set; }

        public decimal Total { get; set; }

        public bool SincronizadoEpicor { get; set; }

        public string RowMod { get; set; }  //D:DElete U:Update C:Create

        public string Platform { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
    }
}