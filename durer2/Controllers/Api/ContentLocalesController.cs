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
using Facade;

namespace durer2.Controllers.Api
{
    public class ContentLocalesController : ApiController
    {
        private HazrefSampleEntities db = new HazrefSampleEntities();

        // GET: api/ContentLocales
        public IQueryable<ContentLocale> GetContentLocale()
        {
            return db.ContentLocale;
        }

        // GET: api/ContentLocales/5
        [ResponseType(typeof(ContentLocale))]
        public async Task<IHttpActionResult> GetContentLocale(int id)
        {
            ContentLocale contentLocale = await db.ContentLocale.FindAsync(id);
            if (contentLocale == null)
            {
                return NotFound();
            }

            return Ok(contentLocale);
        }

        // PUT: api/ContentLocales/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutContentLocale(int id, ContentLocale contentLocale)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contentLocale.Id)
            {
                return BadRequest();
            }

            db.Entry(contentLocale).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContentLocaleExists(id))
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

        // POST: api/ContentLocales
        [ResponseType(typeof(ContentLocale))]
        public async Task<IHttpActionResult> PostContentLocale(ContentLocale contentLocale)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ContentLocale.Add(contentLocale);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = contentLocale.Id }, contentLocale);
        }

        // DELETE: api/ContentLocales/5
        [ResponseType(typeof(ContentLocale))]
        public async Task<IHttpActionResult> DeleteContentLocale(int id)
        {
            ContentLocale contentLocale = await db.ContentLocale.FindAsync(id);
            if (contentLocale == null)
            {
                return NotFound();
            }

            db.ContentLocale.Remove(contentLocale);
            await db.SaveChangesAsync();

            return Ok(contentLocale);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ContentLocaleExists(int id)
        {
            return db.ContentLocale.Count(e => e.Id == id) > 0;
        }
    }
}