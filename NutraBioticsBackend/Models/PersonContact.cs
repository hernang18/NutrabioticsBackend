using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NutraBioticsBackend.Models
{
    public class PersonContact
    {
        [Key]
        public int PersonContactId { get; set; }

        public int PerConID { get; set; }

        public string Company { get; set; }

        public string Name { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNum { get; set; }

        public string EMailAddress { get; set; }

        public string CellPhoneNum { get; set; }

        public string Address1 { get; set; }

        public string CountryId { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public int CountryNum { get; set; }

        public bool SincronizadoEpicor { get; set; }

    }
}