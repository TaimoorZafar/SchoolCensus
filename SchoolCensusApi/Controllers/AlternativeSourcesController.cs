using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using SchoolCensusApi.Models;

namespace SchoolCensusApi.Controllers
{
    public class AlternativeSourcesController : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/AlternativeSources
        public IQueryable<AlternativeSource> GetAlternativeSources()
        {
            return db.AlternativeSources;
        }

        // GET: api/AlternativeSources/5
        [ResponseType(typeof(AlternativeSource))]
        public IHttpActionResult GetAlternativeSource(string id)
        {
            AlternativeSource alternativeSource = db.AlternativeSources.Find(id);
            if (alternativeSource == null)
            {
                return NotFound();
            }

            return Ok(alternativeSource);
        }

        // PUT: api/AlternativeSources/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAlternativeSource(string id, AlternativeSource alternativeSource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != alternativeSource.AlternativeSourceAvailableIfWapdaNotAvailable)
            {
                return BadRequest();
            }

            db.Entry(alternativeSource).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlternativeSourceExists(id))
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

        // POST: api/AlternativeSources
        [ResponseType(typeof(AlternativeSource))]
        public IHttpActionResult PostAlternativeSource(AlternativeSource alternativeSource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AlternativeSources.Add(alternativeSource);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (AlternativeSourceExists(alternativeSource.AlternativeSourceAvailableIfWapdaNotAvailable))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = alternativeSource.AlternativeSourceAvailableIfWapdaNotAvailable }, alternativeSource);
        }

        // DELETE: api/AlternativeSources/5
        [ResponseType(typeof(AlternativeSource))]
        public IHttpActionResult DeleteAlternativeSource(string id)
        {
            AlternativeSource alternativeSource = db.AlternativeSources.Find(id);
            if (alternativeSource == null)
            {
                return NotFound();
            }

            db.AlternativeSources.Remove(alternativeSource);
            db.SaveChanges();

            return Ok(alternativeSource);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AlternativeSourceExists(string id)
        {
            return db.AlternativeSources.Count(e => e.AlternativeSourceAvailableIfWapdaNotAvailable == id) > 0;
        }
    }
}