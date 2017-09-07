using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NutraBioticsBackend.Models
{
    public class OrderDetailTmp
    {
        [Key]
        public int OrderDetailTmpId { get; set; }        

        [Display(Name = "ListaPrecios")]
        public int PriceListPartId { get; set; }

        [Display(Name = "Parte")]
        public int PartId { get; set; }

        [Editable(false)]
        [Display(Name = "Referencia")]
        public decimal Reference { get; set; }

        [Display(Name = "Parte")]
        public string PartNum { get; set; }

        [Display(Name = "Descripcion")]
        public string PartDescription { get; set; }

        [Display(Name = "Cantidad")]
        public decimal OrderQty { get; set; }

        [Display(Name = "Precio Unitario")]
        [DataType(DataType.Currency)]
        public decimal UnitPrice { get; set; }

        [Display(Name = "Impuesto")]
        public decimal TaxAmt { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Total { get; set; }

        public int UserId { get; set; }
    }
}