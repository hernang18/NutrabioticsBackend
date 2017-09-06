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

namespace NutraBioticsBackend.Controllers
{
    public class InvoiceDetailsController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/InvoiceDetails
        public IQueryable<InvoiceDetail> GetInvoiceDetails()
        {
            return db.InvoiceDetails;
        }

        // GET: api/InvoiceDetails/5
        [ResponseType(typeof(InvoiceDetail))]
        public async Task<IHttpActionResult> GetInvoiceDetail(int id)
        {
            //InvoiceDetail invoiceDetail = await db.InvoiceDetails.FindAsync(id);
            //if (invoiceDetail == null)
            //{
            //    return NotFound();
            //}
            var invoiceDetail = await db.InvoiceDetails
            .Where(c => c.VendorId == id)
            .ToListAsync();
            //.FirstOrDefaultAsync();                      
            if (invoiceDetail == null)
            {
                return BadRequest("El vendedor no tiene clientes con facturas.");
            }

            return Ok(invoiceDetail);
        }

        // PUT: api/InvoiceDetails/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutInvoiceDetail(int id, InvoiceDetail invoiceDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != invoiceDetail.InvoiceDetailId)
            {
                return BadRequest();
            }

            db.Entry(invoiceDetail).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceDetailExists(id))
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

        // POST: api/InvoiceDetails
        [ResponseType(typeof(InvoiceDetail))]
        public async Task<IHttpActionResult> PostInvoiceDetail(InvoiceDetail invoiceDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.InvoiceDetails.Add(invoiceDetail);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = invoiceDetail.InvoiceDetailId }, invoiceDetail);
        }

        // DELETE: api/InvoiceDetails/5
        [ResponseType(typeof(InvoiceDetail))]
        public async Task<IHttpActionResult> DeleteInvoiceDetail(int id)
        {
            InvoiceDetail invoiceDetail = await db.InvoiceDetails.FindAsync(id);
            if (invoiceDetail == null)
            {
                return NotFound();
            }

            db.InvoiceDetails.Remove(invoiceDetail);
            await db.SaveChangesAsync();

            return Ok(invoiceDetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InvoiceDetailExists(int id)
        {
            return db.InvoiceDetails.Count(e => e.InvoiceDetailId == id) > 0;
        }
    }
}