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
    public class PageLocalesController : ApiController
    {
        private HazrefSampleEntities db = new HazrefSampleEntities();

        // GET: api/PageLocales
        public IQueryable<PageLocale> GetPageLocale()
        {
            return db.PageLocale;
        }

        // GET: api/PageLocales/5
        [ResponseType(typeof(PageLocale))]
        public async Task<IHttpActionResult> GetPageLocale(int id)
        {
            PageLocale pageLocale = await db.PageLocale.FindAsync(id);
            if (pageLocale == null)
            {
                return NotFound();
            }

            return Ok(pageLocale);
        }

        // PUT: api/PageLocales/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPageLocale(int id, PageLocale pageLocale)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pageLocale.Id)
            {
                return BadRequest();
            }

            db.Entry(pageLocale).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PageLocaleExists(id))
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

        // POST: api/PageLocales
        [ResponseType(typeof(PageLocale))]
        public async Task<IHttpActionResult> PostPageLocale(PageLocale pageLocale)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PageLocale.Add(pageLocale);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = pageLocale.Id }, pageLocale);
        }

        // DELETE: api/PageLocales/5
        [ResponseType(typeof(PageLocale))]
        public async Task<IHttpActionResult> DeletePageLocale(int id)
        {
            PageLocale pageLocale = await db.PageLocale.FindAsync(id);
            if (pageLocale == null)
            {
                return NotFound();
            }

            db.PageLocale.Remove(pageLocale);
            await db.SaveChangesAsync();

            return Ok(pageLocale);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PageLocaleExists(int id)
        {
            return db.PageLocale.Count(e => e.Id == id) > 0;
        }
    }
}