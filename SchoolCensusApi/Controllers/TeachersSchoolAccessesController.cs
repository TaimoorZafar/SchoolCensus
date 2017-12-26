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
    public class TeachersSchoolAccessesController : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/TeachersSchoolAccesses
        public IQueryable<TeachersSchoolAccess> GetTeachersSchoolAccesses()
        {
            return db.TeachersSchoolAccesses;
        }

        // GET: api/TeachersSchoolAccesses/5
        [ResponseType(typeof(TeachersSchoolAccess))]
        public IHttpActionResult GetTeachersSchoolAccess(int id)
        {
            TeachersSchoolAccess teachersSchoolAccess = db.TeachersSchoolAccesses.Find(id);
            if (teachersSchoolAccess == null)
            {
                return NotFound();
            }

            return Ok(teachersSchoolAccess);
        }

        // PUT: api/TeachersSchoolAccesses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTeachersSchoolAccess(int id, TeachersSchoolAccess teachersSchoolAccess)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != teachersSchoolAccess.TeacherSchoolAccesses_id)
            {
                return BadRequest();
            }

            db.Entry(teachersSchoolAccess).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeachersSchoolAccessExists(id))
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

        // POST: api/TeachersSchoolAccesses
        [ResponseType(typeof(TeachersSchoolAccess))]
        public IHttpActionResult PostTeachersSchoolAccess(TeachersSchoolAccess teachersSchoolAccess)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TeachersSchoolAccesses.Add(teachersSchoolAccess);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = teachersSchoolAccess.TeacherSchoolAccesses_id }, teachersSchoolAccess);
        }

        // DELETE: api/TeachersSchoolAccesses/5
        [ResponseType(typeof(TeachersSchoolAccess))]
        public IHttpActionResult DeleteTeachersSchoolAccess(int id)
        {
            TeachersSchoolAccess teachersSchoolAccess = db.TeachersSchoolAccesses.Find(id);
            if (teachersSchoolAccess == null)
            {
                return NotFound();
            }

            db.TeachersSchoolAccesses.Remove(teachersSchoolAccess);
            db.SaveChanges();

            return Ok(teachersSchoolAccess);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TeachersSchoolAccessExists(int id)
        {
            return db.TeachersSchoolAccesses.Count(e => e.TeacherSchoolAccesses_id == id) > 0;
        }
    }
}