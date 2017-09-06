namespace NutraBioticsBackend.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class Territory
    {
        [Key]
        public int TerritoryID { get; set; }

        public string Company { get; set; }

        public string TerritoryEpicorID { get; set; }

        public string TerritoryDesc { get; set; }

        public string RegionCode { get; set; }
    }
}