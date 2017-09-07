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
    public class ShipToesController : Controller
    {
        private DataContext db = new DataContext();

        // GET: ShipToes
        public ActionResult Index()
        {
            var shipToes = db.ShipToes.Include(s => s.Customer);
            return View(shipToes.ToList());
        }

        // GET: ShipToes/Details/5
        public ActionResult Details(int? id)
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

        // GET: ShipToes/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "CustId");
            return View();
        }

        // POST: ShipToes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ShipToId,CustomerId,ShipToNum,CustNum,Company,ShipToName,TerritoryEpicorId,CountryId,Country,State,City,Address,PhoneNum,Email,VendorId,SincronizadoEpicor")] ShipTo shipTo)
        {
            if (ModelState.IsValid)
            {
                db.ShipToes.Add(shipTo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "CustId", shipTo.CustomerId);
            return View(shipTo);
        }

        // GET: ShipToes/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "CustId", shipTo.CustomerId);
            return View(shipTo);
        }

        // POST: ShipToes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ShipToId,CustomerId,ShipToNum,CustNum,Company,ShipToName,TerritoryEpicorId,CountryId,Country,State,City,Address,PhoneNum,Email,VendorId,SincronizadoEpicor")] ShipTo shipTo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shipTo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "CustId", shipTo.CustomerId);
            return View(shipTo);
        }

        // GET: ShipToes/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: ShipToes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ShipTo shipTo = db.ShipToes.Find(id);
            db.ShipToes.Remove(shipTo);
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
    }
}
