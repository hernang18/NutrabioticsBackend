using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NutraBioticsBackend.Models
{
    public class AddproductView
    {

        [Display(Name = "ParteId")]
        public int PartId { get; set; }

        [Display(Name = "Cantidad")]
        public decimal OrderQty { get; set; }

        [Display(Name = "Precio Unitario")]
        [DataType(DataType.Currency)]
        public decimal UnitPrice { get; set; }
    }
}