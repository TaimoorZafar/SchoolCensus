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
    public class SpecialFacilitiesController : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/SpecialFacilities
        public IQueryable<SpecialFacility> GetSpecialFacilities()
        {
            return db.SpecialFacilities;
        }

        // GET: api/SpecialFacilities/5
        [ResponseType(typeof(SpecialFacility))]
        public IHttpActionResult GetSpecialFacility(int id)
        {
            SpecialFacility specialFacility = db.SpecialFacilities.Find(id);
            if (specialFacility == null)
            {
                return NotFound();
            }

            return Ok(specialFacility);
        }

        // PUT: api/SpecialFacilities/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSpecialFacility(int id, SpecialFacility specialFacility)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != specialFacility.SpecialFacilities_id)
            {
                return BadRequest();
            }

            db.Entry(specialFacility).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpecialFacilityExists(id))
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

        // POST: api/SpecialFacilities
        [ResponseType(typeof(SpecialFacility))]
        public IHttpActionResult PostSpecialFacility(SpecialFacility specialFacility)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SpecialFacilities.Add(specialFacility);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = specialFacility.SpecialFacilities_id }, specialFacility);
        }

        // DELETE: api/SpecialFacilities/5
        [ResponseType(typeof(SpecialFacility))]
        public IHttpActionResult DeleteSpecialFacility(int id)
        {
            SpecialFacility specialFacility = db.SpecialFacilities.Find(id);
            if (specialFacility == null)
            {
                return NotFound();
            }

            db.SpecialFacilities.Remove(specialFacility);
            db.SaveChanges();

            return Ok(specialFacility);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SpecialFacilityExists(int id)
        {
            return db.SpecialFacilities.Count(e => e.SpecialFacilities_id == id) > 0;
        }
    }
}