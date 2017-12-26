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
    public class NearBiesController : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/NearBies
        public IQueryable<NearBy> GetNearBys()
        {
            return db.NearBys;
        }

        // GET: api/NearBies/5
        [ResponseType(typeof(NearBy))]
        public IHttpActionResult GetNearBy(int id)
        {
            NearBy nearBy = db.NearBys.Find(id);
            if (nearBy == null)
            {
                return NotFound();
            }

            return Ok(nearBy);
        }

        // PUT: api/NearBies/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNearBy(int id, NearBy nearBy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != nearBy.NearBys_id)
            {
                return BadRequest();
            }

            db.Entry(nearBy).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NearByExists(id))
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

        // POST: api/NearBies
        [ResponseType(typeof(NearBy))]
        public IHttpActionResult PostNearBy(NearBy nearBy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.NearBys.Add(nearBy);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = nearBy.NearBys_id }, nearBy);
        }

        // DELETE: api/NearBies/5
        [ResponseType(typeof(NearBy))]
        public IHttpActionResult DeleteNearBy(int id)
        {
            NearBy nearBy = db.NearBys.Find(id);
            if (nearBy == null)
            {
                return NotFound();
            }

            db.NearBys.Remove(nearBy);
            db.SaveChanges();

            return Ok(nearBy);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NearByExists(int id)
        {
            return db.NearBys.Count(e => e.NearBys_id == id) > 0;
        }
    }
}