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
    public class NewsLocalesController : ApiController
    {
        private HazrefSampleEntities db = new HazrefSampleEntities();

        // GET: api/NewsLocales
        public IQueryable<NewsLocale> GetNewsLocale()
        {
            return db.NewsLocale;
        }

        // GET: api/NewsLocales/5
        [ResponseType(typeof(NewsLocale))]
        public async Task<IHttpActionResult> GetNewsLocale(int id)
        {
            NewsLocale newsLocale = await db.NewsLocale.FindAsync(id);
            if (newsLocale == null)
            {
                return NotFound();
            }

            return Ok(newsLocale);
        }

        // PUT: api/NewsLocales/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutNewsLocale(int id, NewsLocale newsLocale)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != newsLocale.Id)
            {
                return BadRequest();
            }

            db.Entry(newsLocale).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NewsLocaleExists(id))
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

        // POST: api/NewsLocales
        [ResponseType(typeof(NewsLocale))]
        public async Task<IHttpActionResult> PostNewsLocale(NewsLocale newsLocale)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.NewsLocale.Add(newsLocale);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = newsLocale.Id }, newsLocale);
        }

        // DELETE: api/NewsLocales/5
        [ResponseType(typeof(NewsLocale))]
        public async Task<IHttpActionResult> DeleteNewsLocale(int id)
        {
            NewsLocale newsLocale = await db.NewsLocale.FindAsync(id);
            if (newsLocale == null)
            {
                return NotFound();
            }

            db.NewsLocale.Remove(newsLocale);
            await db.SaveChangesAsync();

            return Ok(newsLocale);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NewsLocaleExists(int id)
        {
            return db.NewsLocale.Count(e => e.Id == id) > 0;
        }
    }
}