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
    public class PersonContactsController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/PersonContacts
        public IQueryable<PersonContact> GetPersonContacts()
        {
            return db.PersonContacts;
        }

        // GET: api/PersonContacts/5
        [ResponseType(typeof(PersonContact))]
        public async Task<IHttpActionResult> GetPersonContact(int id)
        {
            PersonContact personContact = await db.PersonContacts.FindAsync(id);
            if (personContact == null)
            {
                return NotFound();
            }

            return Ok(personContact);
        }

        // PUT: api/PersonContacts/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPersonContact(int id, PersonContact personContact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != personContact.PersonContactId)
            {
                return BadRequest();
            }

            db.Entry(personContact).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonContactExists(id))
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

        // POST: api/PersonContacts
        [ResponseType(typeof(PersonContact))]
        public async Task<IHttpActionResult> PostPersonContact(PersonContact personContact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PersonContacts.Add(personContact);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = personContact.PersonContactId }, personContact);
        }

        // DELETE: api/PersonContacts/5
        [ResponseType(typeof(PersonContact))]
        public async Task<IHttpActionResult> DeletePersonContact(int id)
        {
            PersonContact personContact = await db.PersonContacts.FindAsync(id);
            if (personContact == null)
            {
                return NotFound();
            }

            db.PersonContacts.Remove(personContact);
            await db.SaveChangesAsync();

            return Ok(personContact);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PersonContactExists(int id)
        {
            return db.PersonContacts.Count(e => e.PersonContactId == id) > 0;
        }
    }
}