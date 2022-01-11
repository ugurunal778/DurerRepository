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
    public class Pages1Controller : ApiController
    {
        private HazrefSampleEntities db = new HazrefSampleEntities();

        // GET: api/Pages1
        public IQueryable<Pages> GetPages()     
        {
            return db.Pages;
        }

        // GET: api/Pages1/5
        [ResponseType(typeof(Pages))]
        public async Task<IHttpActionResult> GetPages(int id)
        {
            Pages pages = await db.Pages.FindAsync(id);
            if (pages == null)
            {
                return NotFound();
            }

            return Ok(pages);
        }

        // PUT: api/Pages1/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPages(int id, Pages pages)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pages.Id)
            {
                return BadRequest();
            }

            db.Entry(pages).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PagesExists(id))
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

        // POST: api/Pages1
        [ResponseType(typeof(Pages))]
        public async Task<IHttpActionResult> PostPages(Pages pages)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Pages.Add(pages);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = pages.Id }, pages);
        }

        // DELETE: api/Pages1/5
        [ResponseType(typeof(Pages))]
        public async Task<IHttpActionResult> DeletePages(int id)
        {
            Pages pages = await db.Pages.FindAsync(id);
            if (pages == null)
            {
                return NotFound();
            }

            db.Pages.Remove(pages);
            await db.SaveChangesAsync();

            return Ok(pages);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PagesExists(int id)
        {
            return db.Pages.Count(e => e.Id == id) > 0;
        }
    }
}