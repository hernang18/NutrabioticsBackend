namespace NutraBioticsBackend.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web.Mvc;

    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una Compañia")]
        [Display(Name = "Compañia")]
        public int CompanyId { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Usuario Nombres")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caráteres")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Apellidos")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caráteres")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [Index("User_Email_Index", IsUnique = true)]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caráteres")]
        public string Email { get; set; }

        [Display(Name = "Género")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1, 2, ErrorMessage = "Ingrese 1 para Hombres y 2 para mujeres")]
        public int Gender { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "IMEI")]
        [MaxLength(20, ErrorMessage = "El campo {0} debe tener máximo {1} caráteres")]
        public string IMEI { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Pais")]
        public int CountryId { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Departamento")]
        public string State { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Ciudad")]
        public string City { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Direccion")]
        public string Address1 { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Telefono")]
        public string PhoneNumber { get; set; }

        //[Remote("IsExist", "Place", ErrorMessage = "Este vendedor ya esta asociado a otro usuario")]
        [Index("VendorIdIndex", IsUnique =true)]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Vendedor")]
        public int VendorId { get; set; }

        [JsonIgnore]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        [StringLength(10, ErrorMessage = "El campo {0} debe tener entre {1} y {2} carácteres", MinimumLength = 5)]
        public string Password { get; set; }

        [JsonIgnore]
        [Display(Name = "Confirmar contraseña")]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "La contraseña y la confirmción con coinciden")]
        public string PasswordConfirm { get; set; }

        [JsonIgnore]
        [Display(Name = "Planta")]        
        public int PlantId { get; set; }

        [JsonIgnore]
        public virtual ICollection<OrderHeader> OrderHeaders { get; set; }

        [JsonIgnore]
        public virtual Vendor Vendor { get; set; }

        [JsonIgnore]
        public virtual Company Company { get; set; }

        [JsonIgnore]
        public virtual Country Country { get; set; }

        [JsonIgnore]
        public virtual Plant Plant { get; set; }
    }
}