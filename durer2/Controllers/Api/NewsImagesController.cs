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
    public class NewsImagesController : ApiController
    {
        private HazrefSampleEntities db = new HazrefSampleEntities();

        // GET: api/NewsImages
        public IQueryable<NewsImage> GetNewsImage()
        {
            return db.NewsImage;
        }

        // GET: api/NewsImages/5
        [ResponseType(typeof(NewsImage))]
        public async Task<IHttpActionResult> GetNewsImage(int id)
        {
            NewsImage newsImage = await db.NewsImage.FindAsync(id);
            if (newsImage == null)
            {
                return NotFound();
            }

            return Ok(newsImage);
        }

        // PUT: api/NewsImages/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutNewsImage(int id, NewsImage newsImage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != newsImage.Id)
            {
                return BadRequest();
            }

            db.Entry(newsImage).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NewsImageExists(id))
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

        // POST: api/NewsImages
        [ResponseType(typeof(NewsImage))]
        public async Task<IHttpActionResult> PostNewsImage(NewsImage newsImage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.NewsImage.Add(newsImage);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = newsImage.Id }, newsImage);
        }

        // DELETE: api/NewsImages/5
        [ResponseType(typeof(NewsImage))]
        public async Task<IHttpActionResult> DeleteNewsImage(int id)
        {
            NewsImage newsImage = await db.NewsImage.FindAsync(id);
            if (newsImage == null)
            {
                return NotFound();
            }

            db.NewsImage.Remove(newsImage);
            await db.SaveChangesAsync();

            return Ok(newsImage);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NewsImageExists(int id)
        {
            return db.NewsImage.Count(e => e.Id == id) > 0;
        }
    }
}