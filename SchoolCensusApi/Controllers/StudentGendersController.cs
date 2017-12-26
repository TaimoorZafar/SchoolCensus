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
    public class StudentGendersController : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/StudentGenders
        public IQueryable<StudentGender> GetStudentGenders()
        {
            return db.StudentGenders;
        }

        // GET: api/StudentGenders/5
        [ResponseType(typeof(StudentGender))]
        public IHttpActionResult GetStudentGender(string id)
        {
            StudentGender studentGender = db.StudentGenders.Find(id);
            if (studentGender == null)
            {
                return NotFound();
            }

            return Ok(studentGender);
        }

        // PUT: api/StudentGenders/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStudentGender(string id, StudentGender studentGender)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != studentGender.StudentGender1)
            {
                return BadRequest();
            }

            db.Entry(studentGender).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentGenderExists(id))
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

        // POST: api/StudentGenders
        [ResponseType(typeof(StudentGender))]
        public IHttpActionResult PostStudentGender(StudentGender studentGender)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.StudentGenders.Add(studentGender);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (StudentGenderExists(studentGender.StudentGender1))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = studentGender.StudentGender1 }, studentGender);
        }

        // DELETE: api/StudentGenders/5
        [ResponseType(typeof(StudentGender))]
        public IHttpActionResult DeleteStudentGender(string id)
        {
            StudentGender studentGender = db.StudentGenders.Find(id);
            if (studentGender == null)
            {
                return NotFound();
            }

            db.StudentGenders.Remove(studentGender);
            db.SaveChanges();

            return Ok(studentGender);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StudentGenderExists(string id)
        {
            return db.StudentGenders.Count(e => e.StudentGender1 == id) > 0;
        }
    }
}