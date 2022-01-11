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
    public class PageLocaleForIntegrationsController : ApiController
    {
        private HazrefSampleEntities db = new HazrefSampleEntities();

        // GET: api/PageLocaleForIntegrations
        public IQueryable<PageLocaleForIntegration> GetPageLocaleForIntegration()
        {
            return db.PageLocaleForIntegration;
        }

        // GET: api/PageLocaleForIntegrations/5
        [ResponseType(typeof(PageLocaleForIntegration))]
        public async Task<IHttpActionResult> GetPageLocaleForIntegration(int id)
        {
            PageLocaleForIntegration pageLocaleForIntegration = await db.PageLocaleForIntegration.FindAsync(id);
            if (pageLocaleForIntegration == null)
            {
                return NotFound();
            }

            return Ok(pageLocaleForIntegration);
        }

        // PUT: api/PageLocaleForIntegrations/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPageLocaleForIntegration(int id, PageLocaleForIntegration pageLocaleForIntegration)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pageLocaleForIntegration.PageId)
            {
                return BadRequest();
            }

            db.Entry(pageLocaleForIntegration).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PageLocaleForIntegrationExists(id))
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

        // POST: api/PageLocaleForIntegrations
        [ResponseType(typeof(PageLocaleForIntegration))]
        public async Task<IHttpActionResult> PostPageLocaleForIntegration(PageLocaleForIntegration pageLocaleForIntegration)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PageLocaleForIntegration.Add(pageLocaleForIntegration);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PageLocaleForIntegrationExists(pageLocaleForIntegration.PageId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = pageLocaleForIntegration.PageId }, pageLocaleForIntegration);
        }

        // DELETE: api/PageLocaleForIntegrations/5
        [ResponseType(typeof(PageLocaleForIntegration))]
        public async Task<IHttpActionResult> DeletePageLocaleForIntegration(int id)
        {
            PageLocaleForIntegration pageLocaleForIntegration = await db.PageLocaleForIntegration.FindAsync(id);
            if (pageLocaleForIntegration == null)
            {
                return NotFound();
            }

            db.PageLocaleForIntegration.Remove(pageLocaleForIntegration);
            await db.SaveChangesAsync();

            return Ok(pageLocaleForIntegration);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PageLocaleForIntegrationExists(int id)
        {
            return db.PageLocaleForIntegration.Count(e => e.PageId == id) > 0;
        }
    }
}