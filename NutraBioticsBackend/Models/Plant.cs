namespace NutraBioticsBackend.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Plant
    {
        [Key]
        public int PlantId { get; set; }

        public string Company { get; set; }

        public string PlantCode { get; set; }

        [Display(Name = "Planta")]
        public string PlantDescription { get; set; }

        [JsonIgnore]
        public virtual ICollection<User> Users { get; set; }

    }
}