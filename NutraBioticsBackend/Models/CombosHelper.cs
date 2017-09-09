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

        public static List<ShipTo> GetShipto(int VendorId)
        {
            DataContext db = new DataContext();
            var shipto = db.ShipToes.Where(c => c.VendorId == VendorId).OrderBy(c => c.ShipToName).ToList();
            shipto.Add(new ShipTo
            {
                ShipToId = 0,
                ShipToName = "[Seleccione Shipto]"
            });
            return shipto.OrderBy(c => c.ShipToName).ToList();
        }

        public static List<PriceList> GetPriceList(int customerId)
        {
            DataContext db = new DataContext();
            var customerpricelist = db.CustomerPriceLists.Where(c => c.CustomerId == customerId);
            var pricelist = from item in db.PriceLists
                            join item2 in customerpricelist on item.PriceListId equals item2.PriceListId
                            select item;

            return pricelist.OrderBy(p=>p.ListDescription).ToList();
        }
        public static List<Contact> GetContact(int VendorID)
        {
            DataContext db = new DataContext();
            var contact = db.Contacts.Where(c=>c.VendorId==VendorID).OrderBy(c => c.Name).ToList();
            contact.Add(new Contact
            {
                ContactId = 0,
                Name = "[Seleccione Contacto]"
            });
            return contact.OrderBy(c => c.Name).ToList();
        }

        public static List<Part> GetOnePartNull()
        {

            List<Part> part = new List<Part>();
            part.Add(new Part
            {
                 PartId= 0,
                 PartDescription = "[Seleccione cliente]"
            });
            return part;
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

        public static List<Country> GetCountry()
        {
            DataContext db = new DataContext();
            var countries = db.Countries.ToList();
            countries.Add(new Country
            {
                CountryId = 0,
                Description = "[Seleccione pais...]"
            });
            return countries.OrderBy(c => c.Description).ToList();
        }

        public static List<Territory> GetTerritory()
        {
            DataContext db = new DataContext();
            var territories = db.Territories.ToList();
            territories.Add(new Territory
            {
                TerritoryID = 0,
                TerritoryDesc = "[Seleccione Territorio...]"
            });
            return territories.OrderBy(c => c.TerritoryDesc).ToList();
        }



    }
}