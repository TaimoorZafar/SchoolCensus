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
    public class RoomMappingsController : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/RoomMappings
        public IQueryable<RoomMapping> GetRoomMappings()
        {
            return db.RoomMappings;
        }

        // GET: api/RoomMappings/5
        [ResponseType(typeof(RoomMapping))]
        public IHttpActionResult GetRoomMapping(int id)
        {
            RoomMapping roomMapping = db.RoomMappings.Find(id);
            if (roomMapping == null)
            {
                return NotFound();
            }

            return Ok(roomMapping);
        }

        // PUT: api/RoomMappings/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRoomMapping(int id, RoomMapping roomMapping)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != roomMapping.ID)
            {
                return BadRequest();
            }

            db.Entry(roomMapping).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomMappingExists(id))
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

        // POST: api/RoomMappings
        [ResponseType(typeof(RoomMapping))]
        public IHttpActionResult PostRoomMapping(RoomMapping roomMapping)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RoomMappings.Add(roomMapping);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (RoomMappingExists(roomMapping.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = roomMapping.ID }, roomMapping);
        }

        // DELETE: api/RoomMappings/5
        [ResponseType(typeof(RoomMapping))]
        public IHttpActionResult DeleteRoomMapping(int id)
        {
            RoomMapping roomMapping = db.RoomMappings.Find(id);
            if (roomMapping == null)
            {
                return NotFound();
            }

            db.RoomMappings.Remove(roomMapping);
            db.SaveChanges();

            return Ok(roomMapping);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RoomMappingExists(int id)
        {
            return db.RoomMappings.Count(e => e.ID == id) > 0;
        }
    }
}