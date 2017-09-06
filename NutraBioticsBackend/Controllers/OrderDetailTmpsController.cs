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
    public class OrderDetailTmpsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: OrderDetailTmps
        public ActionResult Index()
        {
            return View(db.OrderDetailTmp.ToList());
        }

        // GET: OrderDetailTmps/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetailTmp orderDetailTmp = db.OrderDetailTmp.Find(id);
            if (orderDetailTmp == null)
            {
                return HttpNotFound();
            }
            return View(orderDetailTmp);
        }

        // GET: OrderDetailTmps/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderDetailTmps/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderDetailTmpId,PriceListPartId,PartId,Reference,PartNum,OrderQty,UnitPrice,TaxAmt,Total")] OrderDetailTmp orderDetailTmp)
        {
            if (ModelState.IsValid)
            {
                db.OrderDetailTmp.Add(orderDetailTmp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(orderDetailTmp);
        }

        // GET: OrderDetailTmps/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetailTmp orderDetailTmp = db.OrderDetailTmp.Find(id);
            if (orderDetailTmp == null)
            {
                return HttpNotFound();
            }
            return View(orderDetailTmp);
        }

        // POST: OrderDetailTmps/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderDetailTmpId,PriceListPartId,PartId,Reference,PartNum,OrderQty,UnitPrice,TaxAmt,Total")] OrderDetailTmp orderDetailTmp)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderDetailTmp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(orderDetailTmp);
        }

        // GET: OrderDetailTmps/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetailTmp orderDetailTmp = db.OrderDetailTmp.Find(id);
            if (orderDetailTmp == null)
            {
                return HttpNotFound();
            }
            return View(orderDetailTmp);
        }

        // POST: OrderDetailTmps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderDetailTmp orderDetailTmp = db.OrderDetailTmp.Find(id);
            db.OrderDetailTmp.Remove(orderDetailTmp);
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
