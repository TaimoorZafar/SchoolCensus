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
    public class AccessToSchoolsController : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/AccessToSchools
        public IQueryable<AccessToSchool> GetAccessToSchools()
        {
            return db.AccessToSchools;
        }

        // GET: api/AccessToSchools/5
        [ResponseType(typeof(AccessToSchool))]
        public IHttpActionResult GetAccessToSchool(string id)
        {
            AccessToSchool accessToSchool = db.AccessToSchools.Find(id);
            if (accessToSchool == null)
            {
                return NotFound();
            }

            return Ok(accessToSchool);
        }

        // PUT: api/AccessToSchools/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAccessToSchool(string id, AccessToSchool accessToSchool)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != accessToSchool.AccessToSchool1)
            {
                return BadRequest();
            }

            db.Entry(accessToSchool).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccessToSchoolExists(id))
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

        // POST: api/AccessToSchools
        [ResponseType(typeof(AccessToSchool))]
        public IHttpActionResult PostAccessToSchool(AccessToSchool accessToSchool)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AccessToSchools.Add(accessToSchool);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (AccessToSchoolExists(accessToSchool.AccessToSchool1))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = accessToSchool.AccessToSchool1 }, accessToSchool);
        }

        // DELETE: api/AccessToSchools/5
        [ResponseType(typeof(AccessToSchool))]
        public IHttpActionResult DeleteAccessToSchool(string id)
        {
            AccessToSchool accessToSchool = db.AccessToSchools.Find(id);
            if (accessToSchool == null)
            {
                return NotFound();
            }

            db.AccessToSchools.Remove(accessToSchool);
            db.SaveChanges();

            return Ok(accessToSchool);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AccessToSchoolExists(string id)
        {
            return db.AccessToSchools.Count(e => e.AccessToSchool1 == id) > 0;
        }
    }
}