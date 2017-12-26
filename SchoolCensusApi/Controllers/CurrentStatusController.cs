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
    public class CurrentStatusController : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/CurrentStatus
        public IQueryable<CurrentStatu> GetCurrentStatus()
        {
            return db.CurrentStatus;
        }

        // GET: api/CurrentStatus/5
        [ResponseType(typeof(CurrentStatu))]
        public IHttpActionResult GetCurrentStatu(string id)
        {
            CurrentStatu currentStatu = db.CurrentStatus.Find(id);
            if (currentStatu == null)
            {
                return NotFound();
            }

            return Ok(currentStatu);
        }

        // PUT: api/CurrentStatus/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCurrentStatu(string id, CurrentStatu currentStatu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != currentStatu.CurrentStatus)
            {
                return BadRequest();
            }

            db.Entry(currentStatu).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CurrentStatuExists(id))
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

        // POST: api/CurrentStatus
        [ResponseType(typeof(CurrentStatu))]
        public IHttpActionResult PostCurrentStatu(CurrentStatu currentStatu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CurrentStatus.Add(currentStatu);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CurrentStatuExists(currentStatu.CurrentStatus))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = currentStatu.CurrentStatus }, currentStatu);
        }

        // DELETE: api/CurrentStatus/5
        [ResponseType(typeof(CurrentStatu))]
        public IHttpActionResult DeleteCurrentStatu(string id)
        {
            CurrentStatu currentStatu = db.CurrentStatus.Find(id);
            if (currentStatu == null)
            {
                return NotFound();
            }

            db.CurrentStatus.Remove(currentStatu);
            db.SaveChanges();

            return Ok(currentStatu);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CurrentStatuExists(string id)
        {
            return db.CurrentStatus.Count(e => e.CurrentStatus == id) > 0;
        }
    }
}