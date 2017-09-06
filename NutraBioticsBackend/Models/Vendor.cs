namespace NutraBioticsBackend.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Vendor
    {
        [Key]
        public int VendorId { get; set; }       

        public string VendorEpicorId { get; set; }  //epicor

       // public string SalesRepCode { get; set; }
        
        [Display(Name = "Vendedor")]
        public string VendorName { get; set; }    
    
        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        //[JsonIgnore]
        //public virtual SalesRep SalesRep { get; set; }

        //[JsonIgnore]
        //public virtual ICollection<User> Users { get; set; }
    }
}