namespace NutraBioticsBackend.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class OrderHeader
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

        [Required(ErrorMessage = "Debe seleccionar una Cliente")]
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

        [Required(ErrorMessage = "Debe seleccionar una sucursal")]
        [Display(Name = "Ship To")]
        public int ShipToId { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un Contacto")]
        [Display(Name = "Contacto")]
        public int ContactId { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una Contacto")]
        [Display(Name = "Numero Contacto")]
        public int ConNum { get; set; }   //Epicor       

        public string SalesCategory { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Observaciones")]
        public string Observations { get; set; }

        [Display(Name = "Impuesto")]
        public decimal TaxAmt { get; set; }

        public decimal Total { get; set; }

        [Display(Name = "Sincronizado Epicor")]
        public bool SincronizadoEpicor { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una sucursal")]
        public string ShipToNum { get; set; }

        public string RowMod { get; set; }  //D:DElete U:Update C:Create

        public string Platform { get; set; }

        [JsonIgnore]
        public virtual User User { get; set; }

        [JsonIgnore]
        public virtual Customer Customer { get; set; }

        [JsonIgnore]
        public virtual ShipTo ShipTo { get; set; }

        [JsonIgnore]
        public virtual Contact Contact { get; set; }

        [JsonIgnore]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}