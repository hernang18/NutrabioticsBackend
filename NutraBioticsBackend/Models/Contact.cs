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


        [Editable(false)]
        public int ShipToId { get; set; }


        [Editable(false)]
        public string ShipToNum { get; set; }  //Epicor


        [Editable(false)]
        public int CustNum { get; set; } //epicor

        [Display(Name = "Compañia")]
        public string Company { get; set; }

        [Display(Name = "Nombre Contacto")]
        public string Name { get; set; }

        [Display(Name = "Pais")]
        public int CountryId { get; set; }

        [Display(Name = "Pais")]
        public string Country  { get; set; }

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
        public virtual ShipTo ShipTo { get; set; }

        [JsonIgnore]
        public virtual ICollection<OrderHeader> OrderHeaders { get; set; }
    }
}