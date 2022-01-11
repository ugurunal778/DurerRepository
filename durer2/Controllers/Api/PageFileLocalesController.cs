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
    public class PageFileLocalesController : ApiController
    {
        private HazrefSampleEntities db = new HazrefSampleEntities();

        // GET: api/PageFileLocales
        public IQueryable<PageFileLocale> GetPageFileLocale()
        {
            return db.PageFileLocale;
        }

        // GET: api/PageFileLocales/5
        [ResponseType(typeof(PageFileLocale))]
        public async Task<IHttpActionResult> GetPageFileLocale(int id)
        {
            PageFileLocale pageFileLocale = await db.PageFileLocale.FindAsync(id);
            if (pageFileLocale == null)
            {
                return NotFound();
            }

            return Ok(pageFileLocale);
        }

        // PUT: api/PageFileLocales/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPageFileLocale(int id, PageFileLocale pageFileLocale)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pageFileLocale.Id)
            {
                return BadRequest();
            }

            db.Entry(pageFileLocale).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PageFileLocaleExists(id))
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

        // POST: api/PageFileLocales
        [ResponseType(typeof(PageFileLocale))]
        public async Task<IHttpActionResult> PostPageFileLocale(PageFileLocale pageFileLocale)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PageFileLocale.Add(pageFileLocale);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = pageFileLocale.Id }, pageFileLocale);
        }

        // DELETE: api/PageFileLocales/5
        [ResponseType(typeof(PageFileLocale))]
        public async Task<IHttpActionResult> DeletePageFileLocale(int id)
        {
            PageFileLocale pageFileLocale = await db.PageFileLocale.FindAsync(id);
            if (pageFileLocale == null)
            {
                return NotFound();
            }

            db.PageFileLocale.Remove(pageFileLocale);
            await db.SaveChangesAsync();

            return Ok(pageFileLocale);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PageFileLocaleExists(int id)
        {
            return db.PageFileLocale.Count(e => e.Id == id) > 0;
        }
    }
}