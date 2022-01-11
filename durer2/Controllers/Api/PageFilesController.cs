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
    public class PageFilesController : ApiController
    {
        private HazrefSampleEntities db = new HazrefSampleEntities();

        // GET: api/PageFiles
        public IQueryable<PageFile> GetPageFile()
        {
            return db.PageFile;
        }

        // GET: api/PageFiles/5
        [ResponseType(typeof(PageFile))]
        public async Task<IHttpActionResult> GetPageFile(int id)
        {
            PageFile pageFile = await db.PageFile.FindAsync(id);
            if (pageFile == null)
            {
                return NotFound();
            }

            return Ok(pageFile);
        }

        // PUT: api/PageFiles/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPageFile(int id, PageFile pageFile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pageFile.Id)
            {
                return BadRequest();
            }

            db.Entry(pageFile).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PageFileExists(id))
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

        // POST: api/PageFiles
        [ResponseType(typeof(PageFile))]
        public async Task<IHttpActionResult> PostPageFile(PageFile pageFile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PageFile.Add(pageFile);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = pageFile.Id }, pageFile);
        }

        // DELETE: api/PageFiles/5
        [ResponseType(typeof(PageFile))]
        public async Task<IHttpActionResult> DeletePageFile(int id)
        {
            PageFile pageFile = await db.PageFile.FindAsync(id);
            if (pageFile == null)
            {
                return NotFound();
            }

            db.PageFile.Remove(pageFile);
            await db.SaveChangesAsync();

            return Ok(pageFile);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PageFileExists(int id)
        {
            return db.PageFile.Count(e => e.Id == id) > 0;
        }
    }
}