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
    public class OrderHeadersController : Controller
    {
        private DataContext db = new DataContext();

        // GET: OrderHeaders
        public ActionResult Index()
        {
            var orderHeaders = db.OrderHeaders.Include(o => o.Contact).Include(o => o.Customer).Include(o => o.ShipTo).Include(o => o.User);
            return View(orderHeaders.ToList());
        }

        // GET: OrderHeaders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderHeader orderHeader = db.OrderHeaders.Find(id);
            if (orderHeader == null)
            {
                return HttpNotFound();
            }
            return View(orderHeader);
        }

        // GET: OrderHeaders/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Customers.Where(c => c.VendorId == 74).OrderBy(c => c.Names), "CustomerId", "Names");
            ViewBag.ShipToId = new SelectList(db.ShipToes.Where(c => c.VendorId == 74 && c.CustomerId==db.Customers.FirstOrDefault().CustomerId).OrderBy(c => c.ShipToName), "ShipToId", "ShipToName");
            ViewBag.ContactId = new SelectList(db.Contacts.Where(c => c.VendorId == 74 && c.ShipToId==db.ShipToes.FirstOrDefault().ShipToId).OrderBy(c=>c.Name), "ContactId", "Name");
           
            
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName");
            return View();
                
           
        }

        // POST: OrderHeaders/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SalesOrderHeaderId,SalesOrderHeaderInterId,OrderNum,UserId,VendorId,CustomerId,CustId,CreditHold,Date,NeedByDate,TermsCode,ShipToId,ContactId,ConNum,SalesCategory,Observations,TaxAmt,Total,SincronizadoEpicor,ShipToNum,RowMod")] OrderHeader orderHeader)
        {
            if (ModelState.IsValid)
            {
                db.OrderHeaders.Add(orderHeader);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ContactId = new SelectList(db.Contacts, "ContactId", "ShipToNum", orderHeader.ContactId);
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "CustId", orderHeader.CustomerId);
            ViewBag.ShipToId = new SelectList(db.ShipToes, "ShipToId", "ShipToNum", orderHeader.ShipToId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName", orderHeader.UserId);
            return View(orderHeader);
        }

        // GET: OrderHeaders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderHeader orderHeader = db.OrderHeaders.Find(id);
            if (orderHeader == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContactId = new SelectList(db.Contacts, "ContactId", "ShipToNum", orderHeader.ContactId);
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "CustId", orderHeader.CustomerId);
            ViewBag.ShipToId = new SelectList(db.ShipToes, "ShipToId", "ShipToNum", orderHeader.ShipToId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName", orderHeader.UserId);
            return View(orderHeader);
        }

        // POST: OrderHeaders/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SalesOrderHeaderId,SalesOrderHeaderInterId,OrderNum,UserId,VendorId,CustomerId,CustId,CreditHold,Date,NeedByDate,TermsCode,ShipToId,ContactId,ConNum,SalesCategory,Observations,TaxAmt,Total,SincronizadoEpicor,ShipToNum,RowMod")] OrderHeader orderHeader)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderHeader).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ContactId = new SelectList(db.Contacts, "ContactId", "ShipToNum", orderHeader.ContactId);
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "CustId", orderHeader.CustomerId);
            ViewBag.ShipToId = new SelectList(db.ShipToes, "ShipToId", "ShipToNum", orderHeader.ShipToId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName", orderHeader.UserId);
            return View(orderHeader);
        }

        // GET: OrderHeaders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderHeader orderHeader = db.OrderHeaders.Find(id);
            if (orderHeader == null)
            {
                return HttpNotFound();
            }
            return View(orderHeader);
        }

        // POST: OrderHeaders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderHeader orderHeader = db.OrderHeaders.Find(id);
            db.OrderHeaders.Remove(orderHeader);
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

        #region DrowdownList create an edit
        public JsonResult GetShipToesList(int CustomerId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var shiptoes = db.ShipToes.Where(s => s.CustomerId == CustomerId);
            return Json(shiptoes);
        }
        public JsonResult GetContactList(int shiptoid)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var contacts = db.Contacts.Where(c => c.ShipToId == shiptoid);
            return Json(contacts);
        }
        public JsonResult GetCustomeList(int CustomerId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var customers = db.Customers.Where(c => c.CustomerId == CustomerId);
            return Json(customers);
        }

        #endregion

        #region OrderDetails


        // GET: Customers/CreateShipTo

        public ActionResult CreateOrderDetails(int id)
        {
            ViewBag.SalesOrderHeaderId = new SelectList(db.OrderHeaders, "SalesOrderHeaderId", "SalesOrderHeaderId", id);
            return View();
        }

        // POST: ShipToes/CreateShipTo
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOrderDetails(OrderDetail orderDetails)
        {
            if (ModelState.IsValid)
            {
                db.OrderDetails.Add(orderDetails);
                db.SaveChanges();
                return RedirectToAction("Details" + "/" + orderDetails.SalesOrderHeaderId);
            }

            ViewBag.SalesOrderHeaderId = new SelectList(db.OrderHeaders, "SalesOrderHeaderId", "SalesOrderHeaderId", orderDetails.SalesOrderHeaderId);
            return View(orderDetails);
        }
        #endregion
    }
}
