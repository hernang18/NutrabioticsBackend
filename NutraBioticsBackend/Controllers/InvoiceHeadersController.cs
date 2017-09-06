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
    public class InvoiceHeadersController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/InvoiceHeaders
        public IQueryable<InvoiceHeader> GetInvoiceHeaders()
        {
            return db.InvoiceHeaders;
        }

        // GET: api/InvoiceHeaders/5
        [ResponseType(typeof(InvoiceHeader))]
        public async Task<IHttpActionResult> GetInvoiceHeader(int id)
        {
            //InvoiceHeader invoiceHeader = await db.InvoiceHeaders.FindAsync(id);
            //if (invoiceHeader == null)
            //{
            //    return NotFound();
            //}
            var invoiceHeader = await db.InvoiceHeaders
           .Where(c => c.VendorId == id)
           .ToListAsync();
            //.FirstOrDefaultAsync();                      
            if (invoiceHeader == null)
            {
                return BadRequest("El vendedor no tiene clientes con facturas.");
            }

            return Ok(invoiceHeader);
        }

        // PUT: api/InvoiceHeaders/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutInvoiceHeader(int id, InvoiceHeader invoiceHeader)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != invoiceHeader.InvoiceHeaderId)
            {
                return BadRequest();
            }

            db.Entry(invoiceHeader).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceHeaderExists(id))
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

        // POST: api/InvoiceHeaders
        [ResponseType(typeof(InvoiceHeader))]
        public async Task<IHttpActionResult> PostInvoiceHeader(InvoiceHeader invoiceHeader)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.InvoiceHeaders.Add(invoiceHeader);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = invoiceHeader.InvoiceHeaderId }, invoiceHeader);
        }

        // DELETE: api/InvoiceHeaders/5
        [ResponseType(typeof(InvoiceHeader))]
        public async Task<IHttpActionResult> DeleteInvoiceHeader(int id)
        {
            InvoiceHeader invoiceHeader = await db.InvoiceHeaders.FindAsync(id);
            if (invoiceHeader == null)
            {
                return NotFound();
            }

            db.InvoiceHeaders.Remove(invoiceHeader);
            await db.SaveChangesAsync();

            return Ok(invoiceHeader);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InvoiceHeaderExists(int id)
        {
            return db.InvoiceHeaders.Count(e => e.InvoiceHeaderId == id) > 0;
        }
    }
}