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
    public class TeacherClassesController : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/TeacherClasses
        public IQueryable<TeacherClass> GetTeacherClasses()
        {
            return db.TeacherClasses;
        }

        // GET: api/TeacherClasses/5
        [ResponseType(typeof(TeacherClass))]
        public IHttpActionResult GetTeacherClass(int id)
        {
            TeacherClass teacherClass = db.TeacherClasses.Find(id);
            if (teacherClass == null)
            {
                return NotFound();
            }

            return Ok(teacherClass);
        }

        // PUT: api/TeacherClasses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTeacherClass(int id, TeacherClass teacherClass)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != teacherClass.ID)
            {
                return BadRequest();
            }

            db.Entry(teacherClass).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherClassExists(id))
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

        // POST: api/TeacherClasses
        [ResponseType(typeof(TeacherClass))]
        public IHttpActionResult PostTeacherClass(TeacherClass teacherClass)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TeacherClasses.Add(teacherClass);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = teacherClass.ID }, teacherClass);
        }

        // DELETE: api/TeacherClasses/5
        [ResponseType(typeof(TeacherClass))]
        public IHttpActionResult DeleteTeacherClass(int id)
        {
            TeacherClass teacherClass = db.TeacherClasses.Find(id);
            if (teacherClass == null)
            {
                return NotFound();
            }

            db.TeacherClasses.Remove(teacherClass);
            db.SaveChanges();

            return Ok(teacherClass);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TeacherClassExists(int id)
        {
            return db.TeacherClasses.Count(e => e.ID == id) > 0;
        }
    }
}