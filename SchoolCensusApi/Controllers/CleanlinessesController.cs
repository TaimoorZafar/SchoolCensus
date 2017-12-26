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
    public class CleanlinessesController : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/Cleanlinesses
        public IQueryable<Cleanliness> GetCleanlinesses()
        {
            return db.Cleanlinesses;
        }

        // GET: api/Cleanlinesses/5
        [ResponseType(typeof(Cleanliness))]
        public IHttpActionResult GetCleanliness(int id)
        {
            Cleanliness cleanliness = db.Cleanlinesses.Find(id);
            if (cleanliness == null)
            {
                return NotFound();
            }

            return Ok(cleanliness);
        }

        // PUT: api/Cleanlinesses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCleanliness(int id, Cleanliness cleanliness)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cleanliness.Cleanliness_id)
            {
                return BadRequest();
            }

            db.Entry(cleanliness).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CleanlinessExists(id))
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

        // POST: api/Cleanlinesses
        [ResponseType(typeof(Cleanliness))]
        public IHttpActionResult PostCleanliness(Cleanliness cleanliness)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cleanlinesses.Add(cleanliness);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cleanliness.Cleanliness_id }, cleanliness);
        }

        // DELETE: api/Cleanlinesses/5
        [ResponseType(typeof(Cleanliness))]
        public IHttpActionResult DeleteCleanliness(int id)
        {
            Cleanliness cleanliness = db.Cleanlinesses.Find(id);
            if (cleanliness == null)
            {
                return NotFound();
            }

            db.Cleanlinesses.Remove(cleanliness);
            db.SaveChanges();

            return Ok(cleanliness);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CleanlinessExists(int id)
        {
            return db.Cleanlinesses.Count(e => e.Cleanliness_id == id) > 0;
        }
    }
}