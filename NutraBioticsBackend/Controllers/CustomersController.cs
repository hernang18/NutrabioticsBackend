using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NutraBioticsBackend.Models;

namespace NutraBioticsBackend.Controllers
{
    public class CustomersController : Controller
    {
        public DataContext db = new DataContext();

        // GET: Customers
        public ActionResult Index()
        {
            var customers = db.Customers.Where(c => c.VendorId == 74);
            return View(customers.ToList());
        }

        //// GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var customer = db.Customers.Find(id);

            if (customer == null)
            {
                return HttpNotFound();
            }

            var customerview = new CustomerView
            {
                CustomerId = customer.CustomerId,
                Terms = customer.Terms,
                CustId = customer.CustId,
                Company = customer.Company,
                Address = customer.Address,
                Country = customer.Country,
                State = customer.State,
                CreditHold = customer.CreditHold,
                TerritoryId = customer.TerritoryId,
                City = customer.City,
                Names = customer.Names,
                PhoneNum = customer.PhoneNum,
                ResaleId = customer.ResaleId,
                CustNum = customer.CustNum,
                TerritoryEpicorId = customer.TerritoryId,
                ShipTos = customer.ShipTos.ToList(),
            };

            return View(customerview);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            ViewBag.VendorId = new SelectList(db.Vendors, "VendorId", "VendorEpicorId");
            return View();
        }

        // POST: Customers/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerId,CustId,CustNum,Company,ResaleId,TerritoryId,ShipViaCode,Country,State,City,Address,PhoneNum,Names,LastNames,CreditHold,TermsCode,Terms,VendorId")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.VendorId = new SelectList(db.Vendors, "VendorId", "VendorEpicorId", customer.VendorId);
            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.VendorId = new SelectList(db.Vendors, "VendorId", "VendorEpicorId", customer.VendorId);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerId,CustId,CustNum,Company,ResaleId,TerritoryId,ShipViaCode,Country,State,City,Address,PhoneNum,Names,LastNames,CreditHold,TermsCode,Terms,VendorId")] CustomerView customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.VendorId = new SelectList(db.Vendors, "VendorId", "VendorEpicorId", customer.VendorId);
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        public JsonResult GetCustomerList(int CustomerId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var customers = db.Customers.Where(c => c.CustomerId == CustomerId);
            return Json(customers);
        }


        public JsonResult GetShipToList(int ShipToId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var shiptoes = db.ShipToes.Where(c => c.ShipToId == ShipToId);
            return Json(shiptoes);
        }

        #region Sucursales




        // GET: Customers/CreateShipTo

        public ActionResult CreateShipTo(int id)
        {
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "CustId", id);
            return View();
        }




        // POST: ShipToes/CreateShipTo
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateShipTo([Bind(Include = "ShipToId,CustomerId,ShipToNum,CustNum,Company,ShipToName,TerritoryEpicorId,Country,State,City,Address,PhoneNum,Email,VendorId,SincronizadoEpicor")] ShipTo shipTo)
        {
            if (ModelState.IsValid)
            {
                db.ShipToes.Add(shipTo);
                db.SaveChanges();
                return RedirectToAction("Details" + "/" + shipTo.CustomerId);
            }

            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "CustId", shipTo.CustomerId);
            return View(shipTo);
        }


        public JsonResult Get(int CustomerId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var shiptoes = db.ShipToes.Where(s => s.CustomerId == CustomerId);
            return Json(shiptoes);
        }


        public ActionResult EditShipTo(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ShipTo shipTo = db.ShipToes.Find(id);
            if (shipTo == null)
            {
                return HttpNotFound();
            }

            var shiptoview = new ShipToView
            {
                ShipToId = shipTo.ShipToId,
                ShipToNum = shipTo.ShipToNum,
                ShipToName = shipTo.ShipToName,
                Company = shipTo.Company,
                Address = shipTo.Address,
                Country = shipTo.Country,
                State = shipTo.State,
                City = shipTo.City,
                PhoneNum = shipTo.PhoneNum,
                CustNum = shipTo.CustNum,
                TerritoryEpicorId = shipTo.TerritoryEpicorId,
                Contacts = shipTo.Contacts.ToList(),
            };

            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "CustId", shipTo.CustomerId);
            return View(shiptoview);

        }



        //// GET: ShipToes/EditShipTo/5
        //public ActionResult EditShipTo(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    ShipTo shipTo = db.ShipToes.Find(id);
        //    if (shipTo == null)
        //    {
        //        return HttpNotFound();
        //    }

        //ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "CustId", shipTo.CustomerId);
        //return View(shipTo);
        //}

        // POST: ShipToes/EditShipTo/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult EditShipTo([Bind(Include = "ShipToId,CustomerId,ShipToNum,CustNum,Company,ShipToName,TerritoryEpicorId,Country,State,City,Address,PhoneNum,Email,VendorId,SincronizadoEpicor")] ShipTo shipTo)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(shipTo).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Details" + "/" + shipTo.CustomerId);
        //    }
        //    ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "CustId", shipTo.CustomerId);
        //    return View(shipTo);
        //}

        // GET: ShipToes/DeleteShipTo/5
        public ActionResult DeleteShipTo(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShipTo shipTo = db.ShipToes.Find(id);
            if (shipTo == null)
            {
                return HttpNotFound();
            }
            return View(shipTo);
        }

        // POST: ShipToes/DeleteShipTo/5
        [HttpPost, ActionName("DeleteShipTo")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteSHConfirmed(int id)
        {
            ShipTo shipTo = db.ShipToes.Find(id);
            db.ShipToes.Remove(shipTo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        #endregion


        #region Contactos


        // GET: Contacts/Create
        public ActionResult CreateContact()
        {
            ViewBag.ShipToId = new SelectList(db.ShipToes, "ShipToId", "ShipToNum");
            return View();
        }

        // POST: Contacts/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateContact([Bind(Include = "ContactId,ConNum,ShipToId,ShipToNum,CustNum,Company,Name,Country,State,City,Address,PhoneNum,Email,VendorId,SincronizadoEpicor")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                db.Contacts.Add(contact);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ShipToId = new SelectList(db.ShipToes, "ShipToId", "ShipToNum", contact.ShipToId);
            return View(contact);
        }

        // GET: Contacts/Edit/5
        public ActionResult EditContact(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            ViewBag.ShipToId = new SelectList(db.ShipToes, "ShipToId", "ShipToNum", contact.ShipToId);
            return View(contact);
        }

        // POST: Contacts/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditContact([Bind(Include = "ContactId,ConNum,ShipToId,ShipToNum,CustNum,Company,Name,Country,State,City,Address,PhoneNum,Email,VendorId,SincronizadoEpicor")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ShipToId = new SelectList(db.ShipToes, "ShipToId", "ShipToNum", contact.ShipToId);
            return View(contact);
        }

        // GET: Contacts/Delete/5
        public ActionResult DeleteContact(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("DeleteContact")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConConfirmed(int id)
        {
            Contact contact = db.Contacts.Find(id);
            db.Contacts.Remove(contact);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        #endregion


    }
}
