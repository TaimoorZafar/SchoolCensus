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
    public class HygienesController : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/Hygienes
        public IQueryable<Hygiene> GetHygienes()
        {
            return db.Hygienes;
        }

        // GET: api/Hygienes/5
        [ResponseType(typeof(Hygiene))]
        public IHttpActionResult GetHygiene(string id)
        {
            Hygiene hygiene = db.Hygienes.Find(id);
            if (hygiene == null)
            {
                return NotFound();
            }

            return Ok(hygiene);
        }

        // PUT: api/Hygienes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHygiene(string id, Hygiene hygiene)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hygiene.WaterPointsHygiene)
            {
                return BadRequest();
            }

            db.Entry(hygiene).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HygieneExists(id))
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

        // POST: api/Hygienes
        [ResponseType(typeof(Hygiene))]
        public IHttpActionResult PostHygiene(Hygiene hygiene)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Hygienes.Add(hygiene);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (HygieneExists(hygiene.WaterPointsHygiene))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = hygiene.WaterPointsHygiene }, hygiene);
        }

        // DELETE: api/Hygienes/5
        [ResponseType(typeof(Hygiene))]
        public IHttpActionResult DeleteHygiene(string id)
        {
            Hygiene hygiene = db.Hygienes.Find(id);
            if (hygiene == null)
            {
                return NotFound();
            }

            db.Hygienes.Remove(hygiene);
            db.SaveChanges();

            return Ok(hygiene);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HygieneExists(string id)
        {
            return db.Hygienes.Count(e => e.WaterPointsHygiene == id) > 0;
        }
    }
}