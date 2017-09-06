
namespace NutraBioticsBackend.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class SalesRep
    {
        public int SalesRepId { get; set; }

        public string SalesRepCode { get; set; }

        public string  SalesRepName { get; set; }

        public string  Company { get; set; }

        public int VendorId { get; set; }

        //[JsonIgnore]
        //public virtual ICollection<Vendor> Vendors { get; set; }
    }
}