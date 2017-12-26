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
    public class RegisteredStudentsController : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/RegisteredStudents
        public IQueryable<RegisteredStudent> GetRegisteredStudents()
        {
            return db.RegisteredStudents;
        }

        // GET: api/RegisteredStudents/5
        [ResponseType(typeof(RegisteredStudent))]
        public IHttpActionResult GetRegisteredStudent(int id)
        {
            RegisteredStudent registeredStudent = db.RegisteredStudents.Find(id);
            if (registeredStudent == null)
            {
                return NotFound();
            }

            return Ok(registeredStudent);
        }

        // PUT: api/RegisteredStudents/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRegisteredStudent(int id, RegisteredStudent registeredStudent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != registeredStudent.RegisteredStudents_id)
            {
                return BadRequest();
            }

            db.Entry(registeredStudent).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegisteredStudentExists(id))
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

        // POST: api/RegisteredStudents
        [ResponseType(typeof(RegisteredStudent))]
        public IHttpActionResult PostRegisteredStudent(RegisteredStudent registeredStudent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RegisteredStudents.Add(registeredStudent);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = registeredStudent.RegisteredStudents_id }, registeredStudent);
        }

        // DELETE: api/RegisteredStudents/5
        [ResponseType(typeof(RegisteredStudent))]
        public IHttpActionResult DeleteRegisteredStudent(int id)
        {
            RegisteredStudent registeredStudent = db.RegisteredStudents.Find(id);
            if (registeredStudent == null)
            {
                return NotFound();
            }

            db.RegisteredStudents.Remove(registeredStudent);
            db.SaveChanges();

            return Ok(registeredStudent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RegisteredStudentExists(int id)
        {
            return db.RegisteredStudents.Count(e => e.RegisteredStudents_id == id) > 0;
        }
    }
}