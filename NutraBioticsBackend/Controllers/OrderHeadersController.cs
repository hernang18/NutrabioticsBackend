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

        public ActionResult DeleteProduct(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var orderDetailTmpConsulta = db.OrderDetailTmp.Where(o => o.UserId == 1 && o.OrderDetailTmpId == id).FirstOrDefault();
            if (orderDetailTmpConsulta == null)
            {
                return HttpNotFound();
            }
            db.OrderDetailTmp.Remove(orderDetailTmpConsulta);
            db.SaveChanges();
            return  RedirectToAction("Create");
            
        }

        [HttpPost]
        public ActionResult AddProduct(AddproductView view)
        {
            if(ModelState.IsValid)
            {
                var orderDetailTmpConsulta = db.OrderDetailTmp.Where(o => o.UserId == 1 && o.PartId==view.PartId).FirstOrDefault();

                if (orderDetailTmpConsulta == null)
                {
                    var priceListParts = db.PriceListParts.Where(p => p.PriceListId == view.PriceListId && p.PartId == view.PartId).FirstOrDefault();
                    var part = db.Parts.Find(view.PartId);
                    var orderDetailTmp = new OrderDetailTmp
                    {
                        OrderQty = view.OrderQty,
                        PartId = view.PartId,
                        PartNum = part.PartNum,
                        PartDescription = part.PartDescription,
                        PriceListPartId = priceListParts.PriceListPartId,
                        Reference = view.Reference,
                        TaxAmt = 0,
                        Total = view.OrderQty * view.UnitPrice,
                        UnitPrice = view.UnitPrice,
                        UserId = 1
                    };
                    db.OrderDetailTmp.Add(orderDetailTmp);
                }
                else
                {
                    orderDetailTmpConsulta.OrderQty += view.OrderQty;
                    db.Entry(orderDetailTmpConsulta).State = EntityState.Modified;
                }
        
                db.SaveChanges();
                //view.OrderQty = 0;
                //view.UnitPrice = 0;
                //view.Reference = 0;

               // return View(view);
                return RedirectToAction("Create");
            }

            ViewBag.PriceListId = new SelectList(db.PriceLists.Where(p => p.PriceListId == view.PriceListId), "PriceListId", "ListDescription");
            ViewBag.PartId = new SelectList(CombosHelper.GetPriceListPart(view.PriceListId), "PartId", "PartDescription");
            return View(view);
        }

        public ActionResult AddProduct()
        {
            //var CustomerPriceList = db.CustomerPriceLists.Where(c => c.CustomerId == CustomerID).OrderBy(c => c.CustomerId).ToList();
            ViewBag.PriceListId = new SelectList(db.PriceLists.Where(p=>p.PriceListId== 1), "PriceListId", "ListDescription");
            ViewBag.PartId = new SelectList(CombosHelper.GetPriceListPart(1), "PartId", "PartDescription");
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
            ViewBag.PriceListId = new SelectList(db.PriceLists.Where(p=>p.PriceListId==0).OrderBy(P => P.PriceListId), "PriceListId", "ListDescription");          
            //ViewBag.PartId= new SelectList(CombosHelper.GetOnePartNull(), "PartId", "PartDescription");
            ViewBag.PartId= new SelectList(db.Parts.OrderBy(p=>p.PartDescription), "PartId", "PartDescription");
            var view = new NewOrderView
            {
                Date = DateTime.Now,
                NeedByDate = DateTime.Now
            };
            
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName");
            view.OrderDetails = db.OrderDetailTmp.Where(o=>o.UserId==1).ToList();
            return View(view);  
        }

        // POST: OrderHeaders/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
   

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NewOrderView view)
        {     
                view.RowMod = "C";
                view.Platform = "WEB";
                view.SalesCategory = "Bogota";
                if (view.CustId == null && view.CustomerId != 0)
                {
                    var customer = db.Customers.Where(c => c.CustomerId == view.CustomerId).FirstOrDefault();
                    view.CustId = customer.CustId;
                }
                else
                {
                    ModelState.AddModelError(String.Empty, "Seleccione un cliente");
                }
                if (view.ShipToNum == null && view.ShipToId != 0)
                {
                    var shipto = db.ShipToes.Where(s => s.ShipToId == view.ShipToId).FirstOrDefault();
                    view.ShipToNum = shipto.ShipToNum;
                }
                if (view.ConNum == 0 && view.ContactId != 0)
                {
                    var contact = db.Contacts.Where(c => c.ContactId == view.ContactId).FirstOrDefault();
                    view.ConNum = contact.ConNum;
                }
                view.OrderDetails = db.OrderDetailTmp.Where(o => o.UserId == 1).ToList();

                var response = MovementsHelper.NewOrder(view, 1);
                if (response.Succes)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(String.Empty, response.Message);
                //db.OrderHeaders.Add(orderHeader);
                //db.SaveChanges();             

                view.OrderDetails = db.OrderDetailTmp.Where(o => o.UserId == 1).ToList();
                ViewBag.ContactId = new SelectList(db.Contacts, "ContactId", "Name", view.ContactId);
                ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Names", view.CustomerId);
                ViewBag.ShipToId = new SelectList(db.ShipToes, "ShipToId", "ShipToName", view.ShipToId);
                ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName", view.UserId);
                ViewBag.PriceListId = new SelectList(db.PriceLists.OrderBy(P => P.PriceListId), "PriceListId", "ListDescription");
                //return RedirectToAction("Create", view);         
            return View(view);
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
        public JsonResult GetPartlist(int PriceListId)
        {
            var part = db.PriceListParts.Where(p => p.PriceListId == PriceListId);    
            db.Configuration.ProxyCreationEnabled = false;    
            return Json(part);                     
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
