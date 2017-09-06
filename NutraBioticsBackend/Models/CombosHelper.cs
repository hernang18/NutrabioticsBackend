using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NutraBioticsBackend.Models;


namespace NutraBioticsBackend.Models
{
    public class CombosHelper
    {        
       
        public static List<Customer> GetCustomer(int VendorId)
        {
            DataContext db = new DataContext();
            var Customer = db.Customers.Where(c => c.VendorId == VendorId).OrderBy(c => c.Names).ToList();
            Customer.Add(new Customer
            {
                CustomerId = 0,
                Names="[Seleccione cliente]"
            });
            return Customer.OrderBy(c => c.Names).ToList();
        }

        public static List<PriceListPart> GetPriceListPart(int PriceListId)
        {
            DataContext db = new DataContext();
            var priceListPart = db.PriceListParts.Where(p => p.PriceListId == PriceListId).OrderBy(c => c.PartDescription).ToList();
            priceListPart.Add(new PriceListPart
            {
                PriceListId = 0,
                BasePrice=0,
                PartDescription= "[Seleccione Producto]",
                ListCode=String.Empty,
                PartId=0,
                PartNum=String.Empty,
                PriceListPartId=0

            });
            return priceListPart.OrderBy(c => c.PartDescription).ToList();
        }
    }
}