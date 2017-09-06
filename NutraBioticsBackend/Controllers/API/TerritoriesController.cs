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
    public class TerritoriesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Territories
        public IQueryable<Territory> GetTerritories()
        {
            return db.Territories;
        }

        // GET: api/Territories/5
        [ResponseType(typeof(Territory))]
        public async Task<IHttpActionResult> GetTerritory(int id)
        {
            Territory territory = await db.Territories.FindAsync(id);
            if (territory == null)
            {
                return NotFound();
            }

            return Ok(territory);
        }

        // PUT: api/Territories/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTerritory(int id, Territory territory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != territory.TerritoryID)
            {
                return BadRequest();
            }

            db.Entry(territory).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TerritoryExists(id))
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

        // POST: api/Territories
        [ResponseType(typeof(Territory))]
        public async Task<IHttpActionResult> PostTerritory(Territory territory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Territories.Add(territory);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = territory.TerritoryID }, territory);
        }

        // DELETE: api/Territories/5
        [ResponseType(typeof(Territory))]
        public async Task<IHttpActionResult> DeleteTerritory(int id)
        {
            Territory territory = await db.Territories.FindAsync(id);
            if (territory == null)
            {
                return NotFound();
            }

            db.Territories.Remove(territory);
            await db.SaveChangesAsync();

            return Ok(territory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TerritoryExists(int id)
        {
            return db.Territories.Count(e => e.TerritoryID == id) > 0;
        }
    }
}