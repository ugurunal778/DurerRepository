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
    public class ContentsController : ApiController
    {
        private HazrefSampleEntities db = new HazrefSampleEntities();

        // GET: api/Contents
        public IQueryable<Content> GetContent()
        {
            return db.Content;
        }

        // GET: api/Contents/5
        [ResponseType(typeof(Content))]
        public async Task<IHttpActionResult> GetContent(int id)
        {
            Content content = await db.Content.FindAsync(id);
            if (content == null)
            {
                return NotFound();
            }

            return Ok(content);
        }

        // PUT: api/Contents/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutContent(int id, Content content)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != content.Id)
            {
                return BadRequest();
            }

            db.Entry(content).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContentExists(id))
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

        // POST: api/Contents
        [ResponseType(typeof(Content))]
        public async Task<IHttpActionResult> PostContent(Content content)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Content.Add(content);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = content.Id }, content);
        }

        // DELETE: api/Contents/5
        [ResponseType(typeof(Content))]
        public async Task<IHttpActionResult> DeleteContent(int id)
        {
            Content content = await db.Content.FindAsync(id);
            if (content == null)
            {
                return NotFound();
            }

            db.Content.Remove(content);
            await db.SaveChangesAsync();

            return Ok(content);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ContentExists(int id)
        {
            return db.Content.Count(e => e.Id == id) > 0;
        }
    }
}