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
    public class ClassMediumsController : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/ClassMediums
        public IQueryable<ClassMedium> GetClassMediums()
        {
            return db.ClassMediums;
        }

        // GET: api/ClassMediums/5
        [ResponseType(typeof(ClassMedium))]
        public IHttpActionResult GetClassMedium(string id)
        {
            ClassMedium classMedium = db.ClassMediums.Find(id);
            if (classMedium == null)
            {
                return NotFound();
            }

            return Ok(classMedium);
        }

        // PUT: api/ClassMediums/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutClassMedium(string id, ClassMedium classMedium)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != classMedium.ClassMedium1)
            {
                return BadRequest();
            }

            db.Entry(classMedium).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassMediumExists(id))
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

        // POST: api/ClassMediums
        [ResponseType(typeof(ClassMedium))]
        public IHttpActionResult PostClassMedium(ClassMedium classMedium)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ClassMediums.Add(classMedium);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ClassMediumExists(classMedium.ClassMedium1))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = classMedium.ClassMedium1 }, classMedium);
        }

        // DELETE: api/ClassMediums/5
        [ResponseType(typeof(ClassMedium))]
        public IHttpActionResult DeleteClassMedium(string id)
        {
            ClassMedium classMedium = db.ClassMediums.Find(id);
            if (classMedium == null)
            {
                return NotFound();
            }

            db.ClassMediums.Remove(classMedium);
            db.SaveChanges();

            return Ok(classMedium);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClassMediumExists(string id)
        {
            return db.ClassMediums.Count(e => e.ClassMedium1 == id) > 0;
        }
    }
}