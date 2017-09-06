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

        [Editable(false)]
        public int UserId { get; set; }

        public int VendorId { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un Cliente")]
        [Display(Name = "Cliente")]
        public int CustomerId { get; set; }

        public string CustId { get; set; }

        [Editable(false)]
        [Display(Name = "Credito Retenido")]
        public bool CreditHold { get; set; }

        [Display(Name = "Fecha Orden")]
        public DateTime Date { get; set; }

        [Display(Name = "Fecha Necesidad")]
        public DateTime NeedByDate { get; set; }

        [Editable(false)]
        [Display(Name = "Terminos")]
        public string TermsCode { get; set; }

        [Display(Name = "Ship To")]
        public int ShipToId { get; set; }

        [Display(Name = "Contacto")]
        public int ContactId { get; set; }

        public int ConNum { get; set; }   //Epicor       

        public string SalesCategory { get; set; }

        [Display(Name = "Observaciones")]
        public string Observations { get; set; }

        [Display(Name = "Impuesto")]
        public decimal TaxAmt { get; set; }      

        [Display(Name = "Sincronizado Epicor")]
        public bool SincronizadoEpicor { get; set; }

        public string ShipToNum { get; set; }

        public string RowMod { get; set; }  //D:DElete U:Update C:Create

        public List<OrderDetail> Details { get; set; }

        public decimal Total { get { return Details == null ? 0 : Details.Sum(d => d.Total); } }
    }
}