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
    public class ReasonClosedsController : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/ReasonCloseds
        public IQueryable<ReasonClosed> GetReasonCloseds()
        {
            return db.ReasonCloseds;
        }

        // GET: api/ReasonCloseds/5
        [ResponseType(typeof(ReasonClosed))]
        public IHttpActionResult GetReasonClosed(string id)
        {
            ReasonClosed reasonClosed = db.ReasonCloseds.Find(id);
            if (reasonClosed == null)
            {
                return NotFound();
            }

            return Ok(reasonClosed);
        }

        // PUT: api/ReasonCloseds/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutReasonClosed(string id, ReasonClosed reasonClosed)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reasonClosed.ReasonIfClosed)
            {
                return BadRequest();
            }

            db.Entry(reasonClosed).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReasonClosedExists(id))
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

        // POST: api/ReasonCloseds
        [ResponseType(typeof(ReasonClosed))]
        public IHttpActionResult PostReasonClosed(ReasonClosed reasonClosed)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ReasonCloseds.Add(reasonClosed);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ReasonClosedExists(reasonClosed.ReasonIfClosed))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = reasonClosed.ReasonIfClosed }, reasonClosed);
        }

        // DELETE: api/ReasonCloseds/5
        [ResponseType(typeof(ReasonClosed))]
        public IHttpActionResult DeleteReasonClosed(string id)
        {
            ReasonClosed reasonClosed = db.ReasonCloseds.Find(id);
            if (reasonClosed == null)
            {
                return NotFound();
            }

            db.ReasonCloseds.Remove(reasonClosed);
            db.SaveChanges();

            return Ok(reasonClosed);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReasonClosedExists(string id)
        {
            return db.ReasonCloseds.Count(e => e.ReasonIfClosed == id) > 0;
        }
    }
}