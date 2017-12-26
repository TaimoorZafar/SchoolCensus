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
    public class SchoolShiftsController : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/SchoolShifts
        public IQueryable<SchoolShift> GetSchoolShifts()
        {
            return db.SchoolShifts;
        }

        // GET: api/SchoolShifts/5
        [ResponseType(typeof(SchoolShift))]
        public IHttpActionResult GetSchoolShift(string id)
        {
            SchoolShift schoolShift = db.SchoolShifts.Find(id);
            if (schoolShift == null)
            {
                return NotFound();
            }

            return Ok(schoolShift);
        }

        // PUT: api/SchoolShifts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSchoolShift(string id, SchoolShift schoolShift)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != schoolShift.SchoolShift1)
            {
                return BadRequest();
            }

            db.Entry(schoolShift).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SchoolShiftExists(id))
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

        // POST: api/SchoolShifts
        [ResponseType(typeof(SchoolShift))]
        public IHttpActionResult PostSchoolShift(SchoolShift schoolShift)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SchoolShifts.Add(schoolShift);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (SchoolShiftExists(schoolShift.SchoolShift1))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = schoolShift.SchoolShift1 }, schoolShift);
        }

        // DELETE: api/SchoolShifts/5
        [ResponseType(typeof(SchoolShift))]
        public IHttpActionResult DeleteSchoolShift(string id)
        {
            SchoolShift schoolShift = db.SchoolShifts.Find(id);
            if (schoolShift == null)
            {
                return NotFound();
            }

            db.SchoolShifts.Remove(schoolShift);
            db.SaveChanges();

            return Ok(schoolShift);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SchoolShiftExists(string id)
        {
            return db.SchoolShifts.Count(e => e.SchoolShift1 == id) > 0;
        }
    }
}