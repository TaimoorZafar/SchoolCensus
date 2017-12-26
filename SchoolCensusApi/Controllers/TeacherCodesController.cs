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
    public class TeacherCodesController : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/TeacherCodes
        public IQueryable<TeacherCode> GetTeacherCodes()
        {
            return db.TeacherCodes;
        }

        // GET: api/TeacherCodes/5
        [ResponseType(typeof(TeacherCode))]
        public IHttpActionResult GetTeacherCode(string id)
        {
            TeacherCode teacherCode = db.TeacherCodes.Find(id);
            if (teacherCode == null)
            {
                return NotFound();
            }

            return Ok(teacherCode);
        }

        // PUT: api/TeacherCodes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTeacherCode(string id, TeacherCode teacherCode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != teacherCode.TeacherCode1)
            {
                return BadRequest();
            }

            db.Entry(teacherCode).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherCodeExists(id))
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

        // POST: api/TeacherCodes
        [ResponseType(typeof(TeacherCode))]
        public IHttpActionResult PostTeacherCode(TeacherCode teacherCode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TeacherCodes.Add(teacherCode);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (TeacherCodeExists(teacherCode.TeacherCode1))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = teacherCode.TeacherCode1 }, teacherCode);
        }

        // DELETE: api/TeacherCodes/5
        [ResponseType(typeof(TeacherCode))]
        public IHttpActionResult DeleteTeacherCode(string id)
        {
            TeacherCode teacherCode = db.TeacherCodes.Find(id);
            if (teacherCode == null)
            {
                return NotFound();
            }

            db.TeacherCodes.Remove(teacherCode);
            db.SaveChanges();

            return Ok(teacherCode);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TeacherCodeExists(string id)
        {
            return db.TeacherCodes.Count(e => e.TeacherCode1 == id) > 0;
        }
    }
}