﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NutraBioticsBackend.Models
{
    public class CustomerView
    {
        [Key]
        public int CustomerId { get; set; }

        public string CustId { get; set; }   //epicor

        public int CustNum { get; set; }    //epicor

        public string Company { get; set; }

        public string ResaleId { get; set; }

        public string TerritoryId { get; set; }

        public string ShipViaCode { get; set; }

        public string Country { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public string PhoneNum { get; set; }

        public string Names { get; set; }

        public string LastNames { get; set; }

        public bool CreditHold { get; set; }

        public string TermsCode { get; set; }

        public string Terms { get; set; }

        public int VendorId { get; set; }

        public int ShipToId { get; set; }

        public string ShipToNum { get; set; }  //Epicor

        public string ShipToName { get; set; }

        public string TerritoryEpicorId { get; set; }

        public Customer Customer { get; set; }

        public List<ShipTo> ShipTos { get; set; }

    }
}