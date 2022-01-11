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
    public class ContentBannersController : ApiController
    {
        private HazrefSampleEntities db = new HazrefSampleEntities();

        // GET: api/ContentBanners
        public IQueryable<ContentBanner> GetContentBanner()
        {
            return db.ContentBanner;
        }

        // GET: api/ContentBanners/5
        [ResponseType(typeof(ContentBanner))]
        public async Task<IHttpActionResult> GetContentBanner(int id)
        {
            ContentBanner contentBanner = await db.ContentBanner.FindAsync(id);
            if (contentBanner == null)
            {
                return NotFound();
            }

            return Ok(contentBanner);
        }

        // PUT: api/ContentBanners/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutContentBanner(int id, ContentBanner contentBanner)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contentBanner.Id)
            {
                return BadRequest();
            }

            db.Entry(contentBanner).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContentBannerExists(id))
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

        // POST: api/ContentBanners
        [ResponseType(typeof(ContentBanner))]
        public async Task<IHttpActionResult> PostContentBanner(ContentBanner contentBanner)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ContentBanner.Add(contentBanner);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = contentBanner.Id }, contentBanner);
        }

        // DELETE: api/ContentBanners/5
        [ResponseType(typeof(ContentBanner))]
        public async Task<IHttpActionResult> DeleteContentBanner(int id)
        {
            ContentBanner contentBanner = await db.ContentBanner.FindAsync(id);
            if (contentBanner == null)
            {
                return NotFound();
            }

            db.ContentBanner.Remove(contentBanner);
            await db.SaveChangesAsync();

            return Ok(contentBanner);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ContentBannerExists(int id)
        {
            return db.ContentBanner.Count(e => e.Id == id) > 0;
        }
    }
}