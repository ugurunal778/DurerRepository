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
    public class LanguagesController : ApiController
    {
        private HazrefSampleEntities db = new HazrefSampleEntities();

        // GET: api/Languages
        public IQueryable<Languages> GetLanguages()
        {
            return db.Languages;
        }

        // GET: api/Languages/5
        [ResponseType(typeof(Languages))]
        public async Task<IHttpActionResult> GetLanguages(int id)
        {
            Languages languages = await db.Languages.FindAsync(id);
            if (languages == null)
            {
                return NotFound();
            }

            return Ok(languages);
        }

        // PUT: api/Languages/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutLanguages(int id, Languages languages)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != languages.Id)
            {
                return BadRequest();
            }

            db.Entry(languages).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LanguagesExists(id))
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

        // POST: api/Languages
        [ResponseType(typeof(Languages))]
        public async Task<IHttpActionResult> PostLanguages(Languages languages)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Languages.Add(languages);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LanguagesExists(languages.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = languages.Id }, languages);
        }

        // DELETE: api/Languages/5
        [ResponseType(typeof(Languages))]
        public async Task<IHttpActionResult> DeleteLanguages(int id)
        {
            Languages languages = await db.Languages.FindAsync(id);
            if (languages == null)
            {
                return NotFound();
            }

            db.Languages.Remove(languages);
            await db.SaveChangesAsync();

            return Ok(languages);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LanguagesExists(int id)
        {
            return db.Languages.Count(e => e.Id == id) > 0;
        }
    }
}