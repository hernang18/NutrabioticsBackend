namespace NutraBioticsBackend.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Customer
    {
        [Key]
        [Display(Name = "Cliente")]
        public int CustomerId { get; set; }

        public string CustId { get; set; }   //epicor

        public int CustNum { get; set; }    //epicor


        [Display(Name = "Compañia")]
        public string Company { get; set; }

        public string ResaleId { get; set; }

        public string TerritoryId { get; set; }

        public string ShipViaCode { get; set; }


        [Display(Name = "Pais")]
        public int CountryId { get; set; }

        [Display(Name = "Pais")]
        public string Country { get; set; }

        [Display(Name = "Departamento")]
        public string State { get; set; }

        [Display(Name = "Ciudad")]
        public string City { get; set; }


        [Display(Name = "Direccion")]
        public string Address { get; set; }

        [Display(Name = "Telefono")]
        public string PhoneNum { get; set; }

        [Display(Name = "Nombre Cliente")]
        public string Names { get; set; }

        public string LastNames { get; set; }

        public bool CreditHold { get; set; }

        public string TermsCode { get; set; }

        public string Terms { get; set; }

        public int VendorId { get; set; }

        //[JsonIgnore]
        //public virtual Vendor Vendor { get; set; }

        [JsonIgnore]
        public virtual ICollection<ShipTo> ShipTos { get; set; }

        //[JsonIgnore]
        //public virtual ICollection<CustomerPriceList> CustomerPriceLists { get; set; }

        [JsonIgnore]
        public virtual ICollection<OrderHeader> OrderHeaders { get; set; }
    }
}