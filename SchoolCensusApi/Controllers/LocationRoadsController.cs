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
    public class LocationRoadsController : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/LocationRoads
        public IQueryable<LocationRoad> GetLocationRoads()
        {
            return db.LocationRoads;
        }

        // GET: api/LocationRoads/5
        [ResponseType(typeof(LocationRoad))]
        public IHttpActionResult GetLocationRoad(string id)
        {
            LocationRoad locationRoad = db.LocationRoads.Find(id);
            if (locationRoad == null)
            {
                return NotFound();
            }

            return Ok(locationRoad);
        }

        // PUT: api/LocationRoads/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLocationRoad(string id, LocationRoad locationRoad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != locationRoad.LocationRoad1)
            {
                return BadRequest();
            }

            db.Entry(locationRoad).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationRoadExists(id))
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

        // POST: api/LocationRoads
        [ResponseType(typeof(LocationRoad))]
        public IHttpActionResult PostLocationRoad(LocationRoad locationRoad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.LocationRoads.Add(locationRoad);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (LocationRoadExists(locationRoad.LocationRoad1))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = locationRoad.LocationRoad1 }, locationRoad);
        }

        // DELETE: api/LocationRoads/5
        [ResponseType(typeof(LocationRoad))]
        public IHttpActionResult DeleteLocationRoad(string id)
        {
            LocationRoad locationRoad = db.LocationRoads.Find(id);
            if (locationRoad == null)
            {
                return NotFound();
            }

            db.LocationRoads.Remove(locationRoad);
            db.SaveChanges();

            return Ok(locationRoad);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LocationRoadExists(string id)
        {
            return db.LocationRoads.Count(e => e.LocationRoad1 == id) > 0;
        }
    }
}