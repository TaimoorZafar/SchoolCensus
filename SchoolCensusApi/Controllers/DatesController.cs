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
    public class DatesController : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/Dates
        public IQueryable<Date> GetDates()
        {
            return db.Dates;
        }

        // GET: api/Dates/5
        [ResponseType(typeof(Date))]
        public IHttpActionResult GetDate(string id)
        {
            Date date = db.Dates.Find(id);
            if (date == null)
            {
                return NotFound();
            }

            return Ok(date);
        }

        // PUT: api/Dates/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDate(string id, Date date)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != date.Date1)
            {
                return BadRequest();
            }

            db.Entry(date).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DateExists(id))
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

        // POST: api/Dates
        [ResponseType(typeof(Date))]
        public IHttpActionResult PostDate(Date date)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Dates.Add(date);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (DateExists(date.Date1))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = date.Date1 }, date);
        }

        // DELETE: api/Dates/5
        [ResponseType(typeof(Date))]
        public IHttpActionResult DeleteDate(string id)
        {
            Date date = db.Dates.Find(id);
            if (date == null)
            {
                return NotFound();
            }

            db.Dates.Remove(date);
            db.SaveChanges();

            return Ok(date);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DateExists(string id)
        {
            return db.Dates.Count(e => e.Date1 == id) > 0;
        }
    }
}