using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using NutraBioticsBackend.Models;
using System.Threading.Tasks;

namespace NutraBioticsBackend.Controllers.API
{
    public class SalesRepsController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/SalesReps
        public IQueryable<SalesRep> GetSalesReps()
        {
            return db.SalesReps;
        }

        // GET: api/SalesReps/5
        [ResponseType(typeof(SalesRep))]
        public async Task <IHttpActionResult> GetSalesRep(int id)
        {
            //SalesRep salesRep = db.SalesReps.Find(id);
            //if (salesRep == null)
            //{
            //    return NotFound();
            //}
            var salesReps = await db.SalesReps
           .Where(s => s.VendorId == id)
           .ToListAsync();
            //.FirstOrDefaultAsync();                      
            if (salesReps == null)
            {
                return BadRequest("El Cliente no tiene sucursales asociadas.");
            }

            return Ok(salesReps);
        }

        // PUT: api/SalesReps/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSalesRep(int id, SalesRep salesRep)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != salesRep.SalesRepId)
            {
                return BadRequest();
            }

            db.Entry(salesRep).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalesRepExists(id))
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

        // POST: api/SalesReps
        [ResponseType(typeof(SalesRep))]
        public IHttpActionResult PostSalesRep(SalesRep salesRep)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SalesReps.Add(salesRep);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = salesRep.SalesRepId }, salesRep);
        }

        // DELETE: api/SalesReps/5
        [ResponseType(typeof(SalesRep))]
        public IHttpActionResult DeleteSalesRep(int id)
        {
            SalesRep salesRep = db.SalesReps.Find(id);
            if (salesRep == null)
            {
                return NotFound();
            }

            db.SalesReps.Remove(salesRep);
            db.SaveChanges();

            return Ok(salesRep);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SalesRepExists(int id)
        {
            return db.SalesReps.Count(e => e.SalesRepId == id) > 0;
        }
    }
}