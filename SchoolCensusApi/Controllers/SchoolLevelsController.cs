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
    public class SchoolLevelsController : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/SchoolLevels
        public IQueryable<SchoolLevel> GetSchoolLevels()
        {
            return db.SchoolLevels;
        }

        // GET: api/SchoolLevels/5
        [ResponseType(typeof(SchoolLevel))]
        public IHttpActionResult GetSchoolLevel(string id)
        {
            SchoolLevel schoolLevel = db.SchoolLevels.Find(id);
            if (schoolLevel == null)
            {
                return NotFound();
            }

            return Ok(schoolLevel);
        }

        // PUT: api/SchoolLevels/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSchoolLevel(string id, SchoolLevel schoolLevel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != schoolLevel.LevelOfSchool)
            {
                return BadRequest();
            }

            db.Entry(schoolLevel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SchoolLevelExists(id))
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

        // POST: api/SchoolLevels
        [ResponseType(typeof(SchoolLevel))]
        public IHttpActionResult PostSchoolLevel(SchoolLevel schoolLevel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SchoolLevels.Add(schoolLevel);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (SchoolLevelExists(schoolLevel.LevelOfSchool))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = schoolLevel.LevelOfSchool }, schoolLevel);
        }

        // DELETE: api/SchoolLevels/5
        [ResponseType(typeof(SchoolLevel))]
        public IHttpActionResult DeleteSchoolLevel(string id)
        {
            SchoolLevel schoolLevel = db.SchoolLevels.Find(id);
            if (schoolLevel == null)
            {
                return NotFound();
            }

            db.SchoolLevels.Remove(schoolLevel);
            db.SaveChanges();

            return Ok(schoolLevel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SchoolLevelExists(string id)
        {
            return db.SchoolLevels.Count(e => e.LevelOfSchool == id) > 0;
        }
    }
}