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
    public class HandoverAfterwardsController : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/HandoverAfterwards
        public IQueryable<HandoverAfterward> GetHandoverAfterwards()
        {
            return db.HandoverAfterwards;
        }

        // GET: api/HandoverAfterwards/5
        [ResponseType(typeof(HandoverAfterward))]
        public IHttpActionResult GetHandoverAfterward(string id)
        {
            HandoverAfterward handoverAfterward = db.HandoverAfterwards.Find(id);
            if (handoverAfterward == null)
            {
                return NotFound();
            }

            return Ok(handoverAfterward);
        }

        // PUT: api/HandoverAfterwards/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHandoverAfterward(string id, HandoverAfterward handoverAfterward)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != handoverAfterward.HandoverOrConstructed)
            {
                return BadRequest();
            }

            db.Entry(handoverAfterward).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HandoverAfterwardExists(id))
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

        // POST: api/HandoverAfterwards
        [ResponseType(typeof(HandoverAfterward))]
        public IHttpActionResult PostHandoverAfterward(HandoverAfterward handoverAfterward)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.HandoverAfterwards.Add(handoverAfterward);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (HandoverAfterwardExists(handoverAfterward.HandoverOrConstructed))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = handoverAfterward.HandoverOrConstructed }, handoverAfterward);
        }

        // DELETE: api/HandoverAfterwards/5
        [ResponseType(typeof(HandoverAfterward))]
        public IHttpActionResult DeleteHandoverAfterward(string id)
        {
            HandoverAfterward handoverAfterward = db.HandoverAfterwards.Find(id);
            if (handoverAfterward == null)
            {
                return NotFound();
            }

            db.HandoverAfterwards.Remove(handoverAfterward);
            db.SaveChanges();

            return Ok(handoverAfterward);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HandoverAfterwardExists(string id)
        {
            return db.HandoverAfterwards.Count(e => e.HandoverOrConstructed == id) > 0;
        }
    }
}