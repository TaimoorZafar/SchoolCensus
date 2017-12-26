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
    public class Section1Controller : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/Section1
        public IQueryable<Section1> GetSections1()
        {
            return db.Sections1;
        }

        // GET: api/Section1/5
        [ResponseType(typeof(Section1))]
        public IHttpActionResult GetSection1(int id)
        {
            Section1 section1 = db.Sections1.Find(id);
            if (section1 == null)
            {
                return NotFound();
            }

            return Ok(section1);
        }

        // PUT: api/Section1/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSection1(int id, Section1 section1)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != section1.Sections_id)
            {
                return BadRequest();
            }

            db.Entry(section1).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Section1Exists(id))
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

        // POST: api/Section1
        [ResponseType(typeof(Section1))]
        public IHttpActionResult PostSection1(Section1 section1)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Sections1.Add(section1);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = section1.Sections_id }, section1);
        }

        // DELETE: api/Section1/5
        [ResponseType(typeof(Section1))]
        public IHttpActionResult DeleteSection1(int id)
        {
            Section1 section1 = db.Sections1.Find(id);
            if (section1 == null)
            {
                return NotFound();
            }

            db.Sections1.Remove(section1);
            db.SaveChanges();

            return Ok(section1);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Section1Exists(int id)
        {
            return db.Sections1.Count(e => e.Sections_id == id) > 0;
        }
    }
}