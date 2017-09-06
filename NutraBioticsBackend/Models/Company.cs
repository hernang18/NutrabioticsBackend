namespace NutraBioticsBackend.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }

        [Display(Name = "Compañia Epicor")]
        public string CompanyEpicor { get; set; }

        [Display(Name = "Compañia")]
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}