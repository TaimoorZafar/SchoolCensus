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
    public class OwnedBiesController : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/OwnedBies
        public IQueryable<OwnedBy> GetOwnedBies()
        {
            return db.OwnedBies;
        }

        // GET: api/OwnedBies/5
        [ResponseType(typeof(OwnedBy))]
        public IHttpActionResult GetOwnedBy(string id)
        {
            OwnedBy ownedBy = db.OwnedBies.Find(id);
            if (ownedBy == null)
            {
                return NotFound();
            }

            return Ok(ownedBy);
        }

        // PUT: api/OwnedBies/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOwnedBy(string id, OwnedBy ownedBy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ownedBy.OwnedBy1)
            {
                return BadRequest();
            }

            db.Entry(ownedBy).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OwnedByExists(id))
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

        // POST: api/OwnedBies
        [ResponseType(typeof(OwnedBy))]
        public IHttpActionResult PostOwnedBy(OwnedBy ownedBy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.OwnedBies.Add(ownedBy);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (OwnedByExists(ownedBy.OwnedBy1))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = ownedBy.OwnedBy1 }, ownedBy);
        }

        // DELETE: api/OwnedBies/5
        [ResponseType(typeof(OwnedBy))]
        public IHttpActionResult DeleteOwnedBy(string id)
        {
            OwnedBy ownedBy = db.OwnedBies.Find(id);
            if (ownedBy == null)
            {
                return NotFound();
            }

            db.OwnedBies.Remove(ownedBy);
            db.SaveChanges();

            return Ok(ownedBy);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OwnedByExists(string id)
        {
            return db.OwnedBies.Count(e => e.OwnedBy1 == id) > 0;
        }
    }
}