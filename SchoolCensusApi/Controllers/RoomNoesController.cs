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
    public class RoomNoesController : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/RoomNoes
        public IQueryable<RoomNo> GetRoomNoes()
        {
            return db.RoomNoes;
        }

        // GET: api/RoomNoes/5
        [ResponseType(typeof(RoomNo))]
        public IHttpActionResult GetRoomNo(int id)
        {
            RoomNo roomNo = db.RoomNoes.Find(id);
            if (roomNo == null)
            {
                return NotFound();
            }

            return Ok(roomNo);
        }

        // PUT: api/RoomNoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRoomNo(int id, RoomNo roomNo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != roomNo.RoomID)
            {
                return BadRequest();
            }

            db.Entry(roomNo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomNoExists(id))
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

        // POST: api/RoomNoes
        [ResponseType(typeof(RoomNo))]
        public IHttpActionResult PostRoomNo(RoomNo roomNo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RoomNoes.Add(roomNo);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = roomNo.RoomID }, roomNo);
        }

        // DELETE: api/RoomNoes/5
        [ResponseType(typeof(RoomNo))]
        public IHttpActionResult DeleteRoomNo(int id)
        {
            RoomNo roomNo = db.RoomNoes.Find(id);
            if (roomNo == null)
            {
                return NotFound();
            }

            db.RoomNoes.Remove(roomNo);
            db.SaveChanges();

            return Ok(roomNo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RoomNoExists(int id)
        {
            return db.RoomNoes.Count(e => e.RoomID == id) > 0;
        }
    }
}