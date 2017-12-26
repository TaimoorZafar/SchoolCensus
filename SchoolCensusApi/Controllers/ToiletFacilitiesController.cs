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
    public class ToiletFacilitiesController : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/ToiletFacilities
        public IQueryable<ToiletFacility> GetToiletFacilities()
        {
            return db.ToiletFacilities;
        }

        // GET: api/ToiletFacilities/5
        [ResponseType(typeof(ToiletFacility))]
        public IHttpActionResult GetToiletFacility(int id)
        {
            ToiletFacility toiletFacility = db.ToiletFacilities.Find(id);
            if (toiletFacility == null)
            {
                return NotFound();
            }

            return Ok(toiletFacility);
        }

        // PUT: api/ToiletFacilities/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutToiletFacility(int id, ToiletFacility toiletFacility)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != toiletFacility.ToiletFacilities_id)
            {
                return BadRequest();
            }

            db.Entry(toiletFacility).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToiletFacilityExists(id))
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

        // POST: api/ToiletFacilities
        [ResponseType(typeof(ToiletFacility))]
        public IHttpActionResult PostToiletFacility(ToiletFacility toiletFacility)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ToiletFacilities.Add(toiletFacility);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = toiletFacility.ToiletFacilities_id }, toiletFacility);
        }

        // DELETE: api/ToiletFacilities/5
        [ResponseType(typeof(ToiletFacility))]
        public IHttpActionResult DeleteToiletFacility(int id)
        {
            ToiletFacility toiletFacility = db.ToiletFacilities.Find(id);
            if (toiletFacility == null)
            {
                return NotFound();
            }

            db.ToiletFacilities.Remove(toiletFacility);
            db.SaveChanges();

            return Ok(toiletFacility);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ToiletFacilityExists(int id)
        {
            return db.ToiletFacilities.Count(e => e.ToiletFacilities_id == id) > 0;
        }
    }
}