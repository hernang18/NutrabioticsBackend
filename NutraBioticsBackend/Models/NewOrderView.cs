namespace NutraBioticsBackend.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class NewOrderView
    {
        [Key]
        public int SalesOrderHeaderId { get; set; }

        [Editable(false)]
        public int SalesOrderHeaderInterId { get; set; }

        [Editable(false)]
        [Display(Name = "OrderNum Epicor")]
        public int OrderNum { get; set; }  //Epicor

        [Display(Name = "Usuario Id")]
        [Editable(false)]
        public int UserId { get; set; }

        public int VendorId { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un Cliente")]
        [Display(Name = "Cliente")]
        public int CustomerId { get; set; }

        [Display(Name = "Cliente Id")]
        public string CustId { get; set; }

        [Editable(false)]
        [Display(Name = "Credito Retenido")]
        public bool CreditHold { get; set; }

        [Display(Name = "Fecha Orden")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [Display(Name = "Fecha Necesidad")]
        [DataType(DataType.DateTime)]
        public DateTime NeedByDate { get; set; }

        [Editable(false)]
        [Display(Name = "Terminos")]
        public string TermsCode { get; set; }

        [Display(Name = "Ship To")]
        public int ShipToId { get; set; }

        [Display(Name = "Contacto")]
        public int ContactId { get; set; }

        [Display(Name = "Numero Contacto")]
        public int ConNum { get; set; }   //Epicor       

        public string SalesCategory { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Observaciones")]
        public string Observations { get; set; }

        [Display(Name = "Impuesto")]
        public decimal TaxAmt { get; set; }

        public decimal Total { get { return OrderDetails == null ? 0 : OrderDetails.Sum(d => d.Total); } }

        [Display(Name = "Sincronizado Epicor")]
        public bool SincronizadoEpicor { get; set; }

        public string ShipToNum { get; set; }

        public string RowMod { get; set; }  //D:DElete U:Update C:Create

        public string Platform { get; set; }

        public int PriceListId { get; set; }

        public List<OrderDetailTmp> OrderDetails { get; set; }

      
    }
}