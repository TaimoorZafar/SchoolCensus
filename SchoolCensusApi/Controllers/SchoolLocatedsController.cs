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
    public class SchoolLocatedsController : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/SchoolLocateds
        public IQueryable<SchoolLocated> GetSchoolLocateds()
        {
            return db.SchoolLocateds;
        }

        // GET: api/SchoolLocateds/5
        [ResponseType(typeof(SchoolLocated))]
        public IHttpActionResult GetSchoolLocated(string id)
        {
            SchoolLocated schoolLocated = db.SchoolLocateds.Find(id);
            if (schoolLocated == null)
            {
                return NotFound();
            }

            return Ok(schoolLocated);
        }

        // PUT: api/SchoolLocateds/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSchoolLocated(string id, SchoolLocated schoolLocated)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != schoolLocated.SchoolLocated1)
            {
                return BadRequest();
            }

            db.Entry(schoolLocated).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SchoolLocatedExists(id))
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

        // POST: api/SchoolLocateds
        [ResponseType(typeof(SchoolLocated))]
        public IHttpActionResult PostSchoolLocated(SchoolLocated schoolLocated)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SchoolLocateds.Add(schoolLocated);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (SchoolLocatedExists(schoolLocated.SchoolLocated1))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = schoolLocated.SchoolLocated1 }, schoolLocated);
        }

        // DELETE: api/SchoolLocateds/5
        [ResponseType(typeof(SchoolLocated))]
        public IHttpActionResult DeleteSchoolLocated(string id)
        {
            SchoolLocated schoolLocated = db.SchoolLocateds.Find(id);
            if (schoolLocated == null)
            {
                return NotFound();
            }

            db.SchoolLocateds.Remove(schoolLocated);
            db.SaveChanges();

            return Ok(schoolLocated);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SchoolLocatedExists(string id)
        {
            return db.SchoolLocateds.Count(e => e.SchoolLocated1 == id) > 0;
        }
    }
}