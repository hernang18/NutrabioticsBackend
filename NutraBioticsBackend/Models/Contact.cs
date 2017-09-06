namespace NutraBioticsBackend.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Contact
    {
        [Key]
        public int ContactId { get; set; }

        [Display(Name = "Numero Contacto")]
        public int ConNum { get; set; }   //Epicor

        public int ShipToId { get; set; }

        public string ShipToNum { get; set; }  //Epicor

        public int CustNum { get; set; } //epicor

        public string Company { get; set; }

        [Display(Name = "Nombre Contacto")]
        public string Name { get; set; }

        public string Country { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public string PhoneNum { get; set; }

        public string Email { get; set; }

        public int VendorId { get; set; }

        public bool SincronizadoEpicor { get; set; }

        [JsonIgnore]
        public virtual ShipTo ShipTo { get; set; }

        [JsonIgnore]
        public virtual ICollection<OrderHeader> OrderHeaders { get; set; }
    }
}