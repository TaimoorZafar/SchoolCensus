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
    public class SchoolStatus1Controller : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/SchoolStatus1
        public IQueryable<SchoolStatus> GetSchoolStatuses()
        {
            return db.SchoolStatuses;
        }

        // GET: api/SchoolStatus1/5
        [ResponseType(typeof(SchoolStatus))]
        public IHttpActionResult GetSchoolStatus(int id)
        {
            SchoolStatus schoolStatus = db.SchoolStatuses.Find(id);
            if (schoolStatus == null)
            {
                return NotFound();
            }

            return Ok(schoolStatus);
        }

        // PUT: api/SchoolStatus1/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSchoolStatus(int id, SchoolStatus schoolStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != schoolStatus.SchoolStatuses_id)
            {
                return BadRequest();
            }

            db.Entry(schoolStatus).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SchoolStatusExists(id))
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

        // POST: api/SchoolStatus1
        [ResponseType(typeof(SchoolStatus))]
        public IHttpActionResult PostSchoolStatus(SchoolStatus schoolStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SchoolStatuses.Add(schoolStatus);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = schoolStatus.SchoolStatuses_id }, schoolStatus);
        }

        // DELETE: api/SchoolStatus1/5
        [ResponseType(typeof(SchoolStatus))]
        public IHttpActionResult DeleteSchoolStatus(int id)
        {
            SchoolStatus schoolStatus = db.SchoolStatuses.Find(id);
            if (schoolStatus == null)
            {
                return NotFound();
            }

            db.SchoolStatuses.Remove(schoolStatus);
            db.SaveChanges();

            return Ok(schoolStatus);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SchoolStatusExists(int id)
        {
            return db.SchoolStatuses.Count(e => e.SchoolStatuses_id == id) > 0;
        }
    }
}