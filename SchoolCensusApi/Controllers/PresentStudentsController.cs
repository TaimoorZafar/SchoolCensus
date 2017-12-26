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
    public class PresentStudentsController : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/PresentStudents
        public IQueryable<PresentStudent> GetPresentStudents()
        {
            return db.PresentStudents;
        }

        // GET: api/PresentStudents/5
        [ResponseType(typeof(PresentStudent))]
        public IHttpActionResult GetPresentStudent(int id)
        {
            PresentStudent presentStudent = db.PresentStudents.Find(id);
            if (presentStudent == null)
            {
                return NotFound();
            }

            return Ok(presentStudent);
        }

        // PUT: api/PresentStudents/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPresentStudent(int id, PresentStudent presentStudent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != presentStudent.PresentStudents_id)
            {
                return BadRequest();
            }

            db.Entry(presentStudent).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PresentStudentExists(id))
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

        // POST: api/PresentStudents
        [ResponseType(typeof(PresentStudent))]
        public IHttpActionResult PostPresentStudent(PresentStudent presentStudent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PresentStudents.Add(presentStudent);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = presentStudent.PresentStudents_id }, presentStudent);
        }

        // DELETE: api/PresentStudents/5
        [ResponseType(typeof(PresentStudent))]
        public IHttpActionResult DeletePresentStudent(int id)
        {
            PresentStudent presentStudent = db.PresentStudents.Find(id);
            if (presentStudent == null)
            {
                return NotFound();
            }

            db.PresentStudents.Remove(presentStudent);
            db.SaveChanges();

            return Ok(presentStudent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PresentStudentExists(int id)
        {
            return db.PresentStudents.Count(e => e.PresentStudents_id == id) > 0;
        }
    }
}