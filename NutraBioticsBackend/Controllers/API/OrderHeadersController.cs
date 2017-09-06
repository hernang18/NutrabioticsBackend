using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using NutraBioticsBackend.Models;
using System.Data.Entity.Validation;
using System.Web.Mvc;

namespace NutraBioticsBackend.Controllers.API
{
    public class OrderHeadersController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/OrderHeaders
        public IQueryable<OrderHeader> GetOrderHeaders()
        {
            return db.OrderHeaders;
        }

        // GET: api/OrderHeaders/5
        [ResponseType(typeof(OrderHeader))]
        public async Task<IHttpActionResult> GetOrderHeader(int id)
        {
            //OrderHeader orderHeader = await db.OrderHeaders.FindAsync(id);
            //if (orderHeader == null)
            //{
            //    return NotFound();
            //}
            var orderHeader = await db.OrderHeaders
           .Where(c => c.SalesOrderHeaderId == id)
           .ToListAsync();
            //.FirstOrDefaultAsync();                      
            if (orderHeader == null)
            {
                return NotFound();
            }


            int InvoiceNum = 0;
            bool Facturado = false;
            List<OrderSyncPhone> orderSyncPhonelist = new List<OrderSyncPhone>();
            foreach (var item in orderHeader)
            {
                if (item.OrderNum != 0)
                {
                    clsConexion objCon = new clsConexion();
                    DataTable dtInvcOrder = new DataTable();
                    //Obtenemos las ordenes sin enviar a Epicor
                    objCon.Connection = NutraBioticsBackend.Properties.Resources.ConexionEpicor;
                    string SQL = " Select OrderNum,InvoiceNum " +
                                " from Erp.InvcHead" +
                                " where OrderNum =" + item.OrderNum;
                    objCon.SQL = SQL;
                    dtInvcOrder = objCon.ConsultarDT();
                    if (dtInvcOrder.Rows.Count > 0)
                    {
                        Facturado = true;
                        InvoiceNum = Convert.ToInt32(dtInvcOrder.Rows[0]["InvoiceNum"]);
                    }
                    else
                    {
                        Facturado = false;
                        InvoiceNum = 0;
                    }

                }
                else
                {
                    Facturado = false;
                    InvoiceNum = 0;
                }

                OrderSyncPhone orderSyncPhone = new OrderSyncPhone();
                orderSyncPhone.SalesOrderHeaderInterId = item.SalesOrderHeaderId;
                orderSyncPhone.SalesOrderHeaderPhoneId = 0;//SalesOrderHeaderPhoneId;
                orderSyncPhone.OrderNum = item.OrderNum;
                orderSyncPhone.TaxAmt = item.TaxAmt;
                orderSyncPhone.Total = item.Total;
                orderSyncPhone.Facturado = Facturado;
                orderSyncPhone.InvoiceNum = InvoiceNum;
                orderSyncPhonelist.Add(orderSyncPhone);
            }

            return Ok(orderSyncPhonelist);
        }

        // PUT: api/OrderHeaders/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutOrderHeader(int id, OrderHeader orderHeader)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != orderHeader.SalesOrderHeaderId)
            {
                return BadRequest();
            }

            db.Entry(orderHeader).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderHeaderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/OrderHeaders
        [ResponseType(typeof(OrderHeader))]
        public async Task<IHttpActionResult> PostOrderHeader(List<OrderHeaderSync> list)
        {
            using (var transaccion = db.Database.BeginTransaction())
            {
                try
                {
                    int contactid = 0, connum = 0;

                    List<OrderSyncPhone> orderSyncPhonelist = new List<OrderSyncPhone>();
                    foreach (var order in list)
                    {
                        if (order.Platform == "Movil")
                        {
                            var contactlist = await db.Contacts
                             .Where(c => c.ShipToId == order.ShipToId)
                            .FirstOrDefaultAsync();

                            if (contactlist == null)
                            {
                                var shipto = await db.ShipToes
                                .Where(s => s.ShipToId == order.ShipToId)
                                .FirstOrDefaultAsync();

                                var contact = new Contact
                                {
                                    Address = shipto.Address,
                                    City = shipto.City,
                                    Company = shipto.Company,
                                    State = shipto.State,
                                    Country = shipto.Country,
                                    Name = shipto.ShipToName,
                                    PhoneNum = shipto.PhoneNum,
                                    ConNum = 1,
                                    ShipToNum = shipto.ShipToNum,
                                    ShipToId = shipto.ShipToId,
                                };

                                db.Contacts.Add(contact);
                                await db.SaveChangesAsync();

                                var contactlist2 = await db.Contacts
                                .Where(c => c.ShipToId == order.ShipToId)
                                .FirstOrDefaultAsync();

                                contactid = contactlist2.ContactId;
                                connum = contactlist2.ConNum;
                            }
                            else
                            {
                                contactid = contactlist.ContactId;
                                connum = contactlist.ConNum;
                            }
                        }
                        else
                        {
                            contactid = order.ContactId;
                            connum = order.ConNum;
                        }


                        var orderHeader = new OrderHeader
                        {
                            CustId = order.CustId,
                            ContactId = contactid,
                            OrderNum = order.OrderNum,
                            ConNum = connum,
                            CreditHold = order.CreditHold,
                            CustomerId = order.CustomerId,
                            Date = order.Date,
                            SalesCategory = order.SalesCategory,
                            Observations = order.Observations,
                            ShipToId = order.ShipToId,
                            ShipToNum = order.ShipToNum,
                            TermsCode = order.TermsCode,
                            UserId = order.UserId,
                            NeedByDate = order.NeedByDate,
                            RowMod = order.RowMod,
                            Total = order.Total,
                            VendorId = order.VendorId,
                            Platform=order.Platform,
                            SalesOrderHeaderInterId = order.SalesOrderHeaderInterId
                        };

                        if (orderHeader.SalesOrderHeaderInterId != 0)
                        {
                            await DeleteOrderDetail(orderHeader.SalesOrderHeaderInterId);
                            await DeleteOrderHeader(orderHeader.SalesOrderHeaderInterId);
                        }

                        db.OrderHeaders.Add(orderHeader);
                        await db.SaveChangesAsync();
                        OrderSyncPhone orderSyncPhone = new OrderSyncPhone();
                        orderSyncPhone.SalesOrderHeaderInterId = orderHeader.SalesOrderHeaderId;
                        orderSyncPhone.SalesOrderHeaderPhoneId = order.SalesOrderHeaderPhoneId;
                        orderSyncPhone.TaxAmt = orderHeader.TaxAmt;
                        orderSyncPhone.Total = orderHeader.Total;
                        orderSyncPhone.OrderNum = order.OrderNum;

                        orderSyncPhonelist.Add(orderSyncPhone);


                        foreach (var detail in order.OrderDetails)
                        {
                            var orderDetail = new OrderDetail
                            {
                                OrderLine = detail.OrderLine,
                                OrderQty = detail.OrderQty,
                                OrderNum = detail.OrderNum,
                                PartId = detail.PartId,
                                PartNum = detail.PartNum,
                                PriceListPartId = detail.PriceListPartId,
                                SalesOrderHeaderId = orderHeader.SalesOrderHeaderId,
                                TaxAmt = detail.TaxAmt,
                                UnitPrice = detail.UnitPrice,
                                Reference = detail.Reference,
                                Total = detail.Total
                            };

                            db.OrderDetails.Add(orderDetail);
                        }
                        //orderHeaderList.Add(orderHeader);
                    }

                    await db.SaveChangesAsync();
                    transaccion.Commit();
                    return Ok(orderSyncPhonelist);
                }
                catch (DbEntityValidationException e)
                {
                    var message = string.Empty;
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        message += string.Format("Entity of type \"{0}\" in state \"{1}\" has the following " +
                            "validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            message += string.Format("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    transaccion.Rollback();
                    return BadRequest(message);
                }
                catch (Exception ex)
                {
                    transaccion.Rollback();
                    return BadRequest(ex.Message);
                }
            }
        }

        // DELETE: api/OrderHeaders/5
        [ResponseType(typeof(OrderHeader))]
        public async Task<IHttpActionResult> DeleteOrderHeader(int id)
        {
            OrderHeader orderHeader = await db.OrderHeaders.FindAsync(id);
            if (orderHeader == null)
            {
                return NotFound();
            }

            db.OrderHeaders.Remove(orderHeader);
            await db.SaveChangesAsync();

            return Ok(orderHeader);
        }

        // DELETE: api/OrderDetails/5
        [ResponseType(typeof(OrderDetail))]
        public async Task<IHttpActionResult> DeleteOrderDetail(int SalesOrderHeaderId)
        {

            //if (orderDetail == null)
            //{
            //    return NotFound();
            //}
            var orderDetail = await db.OrderDetails
            .Where(o => o.SalesOrderHeaderId == SalesOrderHeaderId)
            .ToListAsync();
            //.FirstOrDefaultAsync();                      
            if (orderDetail == null)
            {
                return NotFound();
            }
            foreach (var item in orderDetail)
            {
                OrderDetail orderDetail2 = new OrderDetail();
                //orderDetail2.OrderHeader = item.OrderHeader;
                //orderDetail2.OrderLine = item.OrderLine;
                //orderDetail2.OrderNum = item.OrderNum;
                //orderDetail2.OrderQty = item.OrderQty;
                //orderDetail2.OrderHeader = item.OrderHeader;
                //orderDetail2.OrderHeader = item.OrderHeader;
                //orderDetail2.Part = item.Part;
                //orderDetail2.PriceListPartId = item.PriceListPartId;
                //orderDetail2.PriceListPart = item.PriceListPart;
                //orderDetail2.PartNum = item.PartNum;

                orderDetail2 = item;
                db.OrderDetails.Remove(orderDetail2);
                await db.SaveChangesAsync();
            }
            return Ok(orderDetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderHeaderExists(int id)
        {
            return db.OrderHeaders.Count(e => e.SalesOrderHeaderId == id) > 0;
        }



    }
}