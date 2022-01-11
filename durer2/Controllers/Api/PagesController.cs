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
    public class PagesController : ApiController
    {
        private HazrefSampleEntities db = new HazrefSampleEntities();

        // GET: api/Pages
        public IQueryable<Page> GetPage()
        {
            return db.Page;
        }

        // GET: api/Pages/5
        [ResponseType(typeof(Page))]
        public async Task<IHttpActionResult> GetPage(int id)
        {
            Page page = await db.Page.FindAsync(id);
            if (page == null)
            {
                return NotFound();
            }

            return Ok(page);
        }

        // PUT: api/Pages/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPage(int id, Page page)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != page.Id)
            {
                return BadRequest();
            }

            db.Entry(page).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PageExists(id))
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

        // POST: api/Pages
        [ResponseType(typeof(Page))]
        public async Task<IHttpActionResult> PostPage(Page page)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Page.Add(page);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = page.Id }, page);
        }

        // DELETE: api/Pages/5
        [ResponseType(typeof(Page))]
        public async Task<IHttpActionResult> DeletePage(int id)
        {
            Page page = await db.Page.FindAsync(id);
            if (page == null)
            {
                return NotFound();
            }

            db.Page.Remove(page);
            await db.SaveChangesAsync();

            return Ok(page);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PageExists(int id)
        {
            return db.Page.Count(e => e.Id == id) > 0;
        }
    }
}