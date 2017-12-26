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
    public class TeachersComingsController : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/TeachersComings
        public IQueryable<TeachersComing> GetTeachersComings()
        {
            return db.TeachersComings;
        }

        // GET: api/TeachersComings/5
        [ResponseType(typeof(TeachersComing))]
        public IHttpActionResult GetTeachersComing(string id)
        {
            TeachersComing teachersComing = db.TeachersComings.Find(id);
            if (teachersComing == null)
            {
                return NotFound();
            }

            return Ok(teachersComing);
        }

        // PUT: api/TeachersComings/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTeachersComing(string id, TeachersComing teachersComing)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != teachersComing.TeachersAreComingFrom)
            {
                return BadRequest();
            }

            db.Entry(teachersComing).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeachersComingExists(id))
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

        // POST: api/TeachersComings
        [ResponseType(typeof(TeachersComing))]
        public IHttpActionResult PostTeachersComing(TeachersComing teachersComing)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TeachersComings.Add(teachersComing);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (TeachersComingExists(teachersComing.TeachersAreComingFrom))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = teachersComing.TeachersAreComingFrom }, teachersComing);
        }

        // DELETE: api/TeachersComings/5
        [ResponseType(typeof(TeachersComing))]
        public IHttpActionResult DeleteTeachersComing(string id)
        {
            TeachersComing teachersComing = db.TeachersComings.Find(id);
            if (teachersComing == null)
            {
                return NotFound();
            }

            db.TeachersComings.Remove(teachersComing);
            db.SaveChanges();

            return Ok(teachersComing);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TeachersComingExists(string id)
        {
            return db.TeachersComings.Count(e => e.TeachersAreComingFrom == id) > 0;
        }
    }
}