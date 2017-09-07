namespace NutraBioticsBackend.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ShipTo
    {
        [Key]
        public int ShipToId { get; set; }

        [Display(Name = "Cliente")]
        public int CustomerId { get; set; }

        public string ShipToNum { get; set; }  //Epicor

        [Editable(false)]
        public int CustNum { get; set; } //epicor

        [Display(Name = "Compañia")]
        public string Company { get; set; }

        [Display(Name = "Nombre Sucursal")]
        public string ShipToName { get; set; }


        [Display(Name = "Territorio")]
        public string TerritoryEpicorId { get; set; }

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

        public string Email { get; set; }

        public int VendorId { get; set; }

        public bool SincronizadoEpicor { get; set; }

        [JsonIgnore]
        public virtual ICollection<Contact> Contacts { get; set; }

        [JsonIgnore]
        public virtual Customer Customer { get; set; }

        [JsonIgnore]
        public virtual ICollection<OrderHeader> OrderHeaders { get; set; }
    }
}