namespace NutraBioticsBackend.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Country
    {
        [Key]
        public int CountryId { get; set; }

        public string Company { get; set; }

        public int CountryNum { get; set; }

        public string FiscalCode { get; set; }

        [Display(Name = "Pais")]
        public string Description { get; set; }

        [JsonIgnore]
        public virtual ICollection<User> Users { get; set; }
    }
}