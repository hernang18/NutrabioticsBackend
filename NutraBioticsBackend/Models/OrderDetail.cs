namespace NutraBioticsBackend.Models
{
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;

    public class OrderDetail
    {
        [Key]
        public int SalesOrderDetaliId { get; set; }

        public int SalesOrderHeaderId { get; set; }

        [Display(Name = "OrderNum")]
        [Editable(false)]
        public int OrderNum { get; set; }  //Epicor

        [Display(Name = "ListaPrecios")]
        public int PriceListPartId { get; set; }

        [Display(Name = "Parte")]
        public int PartId { get; set; }


        [Display(Name = "Linea")]
        [Editable(false)]
        public int OrderLine { get; set; }
        
        [Editable(false)]

        [Display(Name = "Referencia")]
        public decimal Reference { get; set; }

        [Display(Name = "Parte")]
        public string PartNum { get; set; }

        [Display(Name = "Cantidad")]
        public decimal OrderQty { get; set; }

        [Display(Name = "Precio Unitario")]
        [DataType(DataType.Currency)]
        public decimal UnitPrice { get; set; }

        [Display(Name = "Impuesto")]
        public decimal TaxAmt { get; set; }

        public decimal Total { get; set; }

        //[JsonIgnore]
        //public virtual PriceListPart PriceListPart { get; set; }

        [JsonIgnore]
        public virtual OrderHeader OrderHeader { get; set; }

        [JsonIgnore]
        public virtual Part Part { get; set; }
    }
}