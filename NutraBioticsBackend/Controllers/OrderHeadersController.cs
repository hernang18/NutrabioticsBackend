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

        [HttpPost]
        public ActionResult AddProduct(AddproductView view)
        {
            if(ModelState.IsValid)
            {
                var priceListParts = db.PriceListParts.Where(p => p.PriceListId == view.PriceListId && p.PartId == view.PartId).FirstOrDefault();
                var part = db.Parts.Find(view.PartId);
                var orderDetailTmp = new OrderDetailTmp
                {

                    OrderQty = view.OrderQty,       
                    PartId = view.PartId,
                    PartNum = part.PartNum,
                    PriceListPartId = priceListParts.PriceListPartId,
                    Reference = view.Reference,
                    TaxAmt = 0,
                    Total = view.OrderQty * view.UnitPrice,
                    UnitPrice=view.UnitPrice
                };
                db.OrderDetailTmp.Add(orderDetailTmp);
                db.SaveChanges();
                return RedirectToAction("Create");

            }
            ViewBag.PriceListId = new SelectList(db.PriceLists.Where(p => p.PriceListId == view.PriceListId), "PriceListId", "ListDescription");
            ViewBag.PartId = new SelectList(CombosHelper.GetPriceListPart(view.PriceListId), "PartId", "PartDescription");
            return View(view);
        }

            public ActionResult AddProduct(int PriceListId)
        {

            //var CustomerPriceList = db.CustomerPriceLists.Where(c => c.CustomerId == CustomerID).OrderBy(c => c.CustomerId).ToList();
            ViewBag.PriceListId = new SelectList(db.PriceLists.Where(p=>p.PriceListId== PriceListId), "PriceListId", "ListDescription");
            ViewBag.PartId = new SelectList(CombosHelper.GetPriceListPart(PriceListId), "PartId", "PartDescription");
            //ViewBag.PriceListPartId=
            return View();
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
            ViewBag.CustomerId = new SelectList(CombosHelper.GetCustomer(74), "CustomerId", "Names");
            ViewBag.ShipToId = new SelectList(db.ShipToes.Where(c => c.VendorId == 74 && c.CustomerId==db.Customers.FirstOrDefault().CustomerId).OrderBy(c => c.ShipToName), "ShipToId", "ShipToName");
            ViewBag.ContactId = new SelectList(db.Contacts.Where(c => c.VendorId == 74 && c.ShipToId==db.ShipToes.FirstOrDefault().ShipToId).OrderBy(c=>c.Name), "ContactId", "Name");
            ViewBag.PriceListId = new SelectList(db.PriceLists.OrderBy(P => P.PriceListId), "PriceListId", "ListDescription");            
            var view = new NewOrderView
            {
                Date = DateTime.Now,
                NeedByDate = DateTime.Now
            };
            
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName");
            view.OrderDetails = db.OrderDetailTmp.ToList();
            return View(view);  
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
        public JsonResult GetPriceList(int customerId)
        {
            var customerpricelist = db.CustomerPriceLists.Where(c => c.CustomerId == customerId);
            var pricelist = from item in db.PriceLists
                            join item2 in customerpricelist on item.PriceListId equals item2.PriceListId
                            select item;
                 
            db.Configuration.ProxyCreationEnabled = false;
            return Json(pricelist);
        }
        public JsonResult GetProductPrice(int PriceListId,int PartId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var pricelist = db.PriceListParts.Where(p => p.PriceListId == PriceListId && p.PartId==PartId);
            return Json(pricelist);
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
