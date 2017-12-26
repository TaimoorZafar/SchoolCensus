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
    public class InfraClassesController : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/InfraClasses
        public IQueryable<InfraClass> GetInfraClasses()
        {
            return db.InfraClasses;
        }

        // GET: api/InfraClasses/5
        [ResponseType(typeof(InfraClass))]
        public IHttpActionResult GetInfraClass(int id)
        {
            InfraClass infraClass = db.InfraClasses.Find(id);
            if (infraClass == null)
            {
                return NotFound();
            }

            return Ok(infraClass);
        }

        // PUT: api/InfraClasses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutInfraClass(int id, InfraClass infraClass)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != infraClass.InfraClasses_id)
            {
                return BadRequest();
            }

            db.Entry(infraClass).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InfraClassExists(id))
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

        // POST: api/InfraClasses
        [ResponseType(typeof(InfraClass))]
        public IHttpActionResult PostInfraClass(InfraClass infraClass)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.InfraClasses.Add(infraClass);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = infraClass.InfraClasses_id }, infraClass);
        }

        // DELETE: api/InfraClasses/5
        [ResponseType(typeof(InfraClass))]
        public IHttpActionResult DeleteInfraClass(int id)
        {
            InfraClass infraClass = db.InfraClasses.Find(id);
            if (infraClass == null)
            {
                return NotFound();
            }

            db.InfraClasses.Remove(infraClass);
            db.SaveChanges();

            return Ok(infraClass);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InfraClassExists(int id)
        {
            return db.InfraClasses.Count(e => e.InfraClasses_id == id) > 0;
        }
    }
}