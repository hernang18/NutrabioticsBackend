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

namespace NutraBioticsBackend.Controllers.API
{
    public class CashHeadersController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/CashHeaders
        public IQueryable<CashHeader> GetCashHeaders()
        {
            return db.CashHeaders;
        }

        // GET: api/CashHeaders/5
        [ResponseType(typeof(CashHeader))]
        public async Task<IHttpActionResult> GetCashHeader(int id)
        {
            //CashHeader cashHeader = await db.CashHeaders.FindAsync(id);
            //if (cashHeader == null)
            //{
            //    return NotFound();
            //}
            var cashHeader = await db.CashHeaders
            .Where(c => c.VendorId == id)
            .ToListAsync();
            //.FirstOrDefaultAsync();                      
            if (cashHeader == null)
            {
                return BadRequest("El vendedor no tiene clientes con pagos.");
            }


            return Ok(cashHeader);
        }

        // PUT: api/CashHeaders/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCashHeader(int id, CashHeader cashHeader)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cashHeader.CashHeaderId)
            {
                return BadRequest();
            }

            db.Entry(cashHeader).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CashHeaderExists(id))
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

        // POST: api/CashHeaders
        [ResponseType(typeof(CashHeader))]
        public async Task<IHttpActionResult> PostCashHeader(CashHeader cashHeader)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CashHeaders.Add(cashHeader);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = cashHeader.CashHeaderId }, cashHeader);
        }

        // DELETE: api/CashHeaders/5
        [ResponseType(typeof(CashHeader))]
        public async Task<IHttpActionResult> DeleteCashHeader(int id)
        {
            CashHeader cashHeader = await db.CashHeaders.FindAsync(id);
            if (cashHeader == null)
            {
                return NotFound();
            }

            db.CashHeaders.Remove(cashHeader);
            await db.SaveChangesAsync();

            return Ok(cashHeader);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CashHeaderExists(int id)
        {
            return db.CashHeaders.Count(e => e.CashHeaderId == id) > 0;
        }
    }
}