namespace NutraBioticsBackend.Models
{
    using System;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Calendar
    {
        [Key]
        public int CalendarId { get; set; }

        public string Company { get; set; }

        public string FiscalCalendarID { get; set; }//hrn

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

    }
}