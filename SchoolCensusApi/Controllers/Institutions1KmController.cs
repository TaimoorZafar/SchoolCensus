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
    public class Institutions1KmController : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/Institutions1Km
        public IQueryable<Institutions1Km> GetInstitutions1Km()
        {
            return db.Institutions1Km;
        }

        // GET: api/Institutions1Km/5
        [ResponseType(typeof(Institutions1Km))]
        public IHttpActionResult GetInstitutions1Km(string id)
        {
            Institutions1Km institutions1Km = db.Institutions1Km.Find(id);
            if (institutions1Km == null)
            {
                return NotFound();
            }

            return Ok(institutions1Km);
        }

        // PUT: api/Institutions1Km/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutInstitutions1Km(string id, Institutions1Km institutions1Km)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != institutions1Km.InstitutionTypeWithin1KmRadius)
            {
                return BadRequest();
            }

            db.Entry(institutions1Km).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Institutions1KmExists(id))
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

        // POST: api/Institutions1Km
        [ResponseType(typeof(Institutions1Km))]
        public IHttpActionResult PostInstitutions1Km(Institutions1Km institutions1Km)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Institutions1Km.Add(institutions1Km);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (Institutions1KmExists(institutions1Km.InstitutionTypeWithin1KmRadius))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = institutions1Km.InstitutionTypeWithin1KmRadius }, institutions1Km);
        }

        // DELETE: api/Institutions1Km/5
        [ResponseType(typeof(Institutions1Km))]
        public IHttpActionResult DeleteInstitutions1Km(string id)
        {
            Institutions1Km institutions1Km = db.Institutions1Km.Find(id);
            if (institutions1Km == null)
            {
                return NotFound();
            }

            db.Institutions1Km.Remove(institutions1Km);
            db.SaveChanges();

            return Ok(institutions1Km);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Institutions1KmExists(string id)
        {
            return db.Institutions1Km.Count(e => e.InstitutionTypeWithin1KmRadius == id) > 0;
        }
    }
}