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
    public class StudentAgesController : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/StudentAges
        public IQueryable<StudentAge> GetStudentAges()
        {
            return db.StudentAges;
        }

        // GET: api/StudentAges/5
        [ResponseType(typeof(StudentAge))]
        public IHttpActionResult GetStudentAge(int id)
        {
            StudentAge studentAge = db.StudentAges.Find(id);
            if (studentAge == null)
            {
                return NotFound();
            }

            return Ok(studentAge);
        }

        // PUT: api/StudentAges/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStudentAge(int id, StudentAge studentAge)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != studentAge.StudentAge1)
            {
                return BadRequest();
            }

            db.Entry(studentAge).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentAgeExists(id))
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

        // POST: api/StudentAges
        [ResponseType(typeof(StudentAge))]
        public IHttpActionResult PostStudentAge(StudentAge studentAge)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.StudentAges.Add(studentAge);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (StudentAgeExists(studentAge.StudentAge1))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = studentAge.StudentAge1 }, studentAge);
        }

        // DELETE: api/StudentAges/5
        [ResponseType(typeof(StudentAge))]
        public IHttpActionResult DeleteStudentAge(int id)
        {
            StudentAge studentAge = db.StudentAges.Find(id);
            if (studentAge == null)
            {
                return NotFound();
            }

            db.StudentAges.Remove(studentAge);
            db.SaveChanges();

            return Ok(studentAge);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StudentAgeExists(int id)
        {
            return db.StudentAges.Count(e => e.StudentAge1 == id) > 0;
        }
    }
}