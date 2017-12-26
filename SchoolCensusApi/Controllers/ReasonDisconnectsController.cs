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
    public class ReasonDisconnectsController : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/ReasonDisconnects
        public IQueryable<ReasonDisconnect> GetReasonDisconnects()
        {
            return db.ReasonDisconnects;
        }

        // GET: api/ReasonDisconnects/5
        [ResponseType(typeof(ReasonDisconnect))]
        public IHttpActionResult GetReasonDisconnect(string id)
        {
            ReasonDisconnect reasonDisconnect = db.ReasonDisconnects.Find(id);
            if (reasonDisconnect == null)
            {
                return NotFound();
            }

            return Ok(reasonDisconnect);
        }

        // PUT: api/ReasonDisconnects/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutReasonDisconnect(string id, ReasonDisconnect reasonDisconnect)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reasonDisconnect.ReasonForDisconnection)
            {
                return BadRequest();
            }

            db.Entry(reasonDisconnect).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReasonDisconnectExists(id))
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

        // POST: api/ReasonDisconnects
        [ResponseType(typeof(ReasonDisconnect))]
        public IHttpActionResult PostReasonDisconnect(ReasonDisconnect reasonDisconnect)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ReasonDisconnects.Add(reasonDisconnect);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ReasonDisconnectExists(reasonDisconnect.ReasonForDisconnection))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = reasonDisconnect.ReasonForDisconnection }, reasonDisconnect);
        }

        // DELETE: api/ReasonDisconnects/5
        [ResponseType(typeof(ReasonDisconnect))]
        public IHttpActionResult DeleteReasonDisconnect(string id)
        {
            ReasonDisconnect reasonDisconnect = db.ReasonDisconnects.Find(id);
            if (reasonDisconnect == null)
            {
                return NotFound();
            }

            db.ReasonDisconnects.Remove(reasonDisconnect);
            db.SaveChanges();

            return Ok(reasonDisconnect);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReasonDisconnectExists(string id)
        {
            return db.ReasonDisconnects.Count(e => e.ReasonForDisconnection == id) > 0;
        }
    }
}