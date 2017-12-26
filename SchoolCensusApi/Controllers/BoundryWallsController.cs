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
    public class BoundryWallsController : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/BoundryWalls
        public IQueryable<BoundryWall> GetBoundryWalls()
        {
            return db.BoundryWalls;
        }

        // GET: api/BoundryWalls/5
        [ResponseType(typeof(BoundryWall))]
        public IHttpActionResult GetBoundryWall(string id)
        {
            BoundryWall boundryWall = db.BoundryWalls.Find(id);
            if (boundryWall == null)
            {
                return NotFound();
            }

            return Ok(boundryWall);
        }

        // PUT: api/BoundryWalls/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBoundryWall(string id, BoundryWall boundryWall)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != boundryWall.BoundryWallAvailable)
            {
                return BadRequest();
            }

            db.Entry(boundryWall).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BoundryWallExists(id))
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

        // POST: api/BoundryWalls
        [ResponseType(typeof(BoundryWall))]
        public IHttpActionResult PostBoundryWall(BoundryWall boundryWall)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BoundryWalls.Add(boundryWall);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (BoundryWallExists(boundryWall.BoundryWallAvailable))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = boundryWall.BoundryWallAvailable }, boundryWall);
        }

        // DELETE: api/BoundryWalls/5
        [ResponseType(typeof(BoundryWall))]
        public IHttpActionResult DeleteBoundryWall(string id)
        {
            BoundryWall boundryWall = db.BoundryWalls.Find(id);
            if (boundryWall == null)
            {
                return NotFound();
            }

            db.BoundryWalls.Remove(boundryWall);
            db.SaveChanges();

            return Ok(boundryWall);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BoundryWallExists(string id)
        {
            return db.BoundryWalls.Count(e => e.BoundryWallAvailable == id) > 0;
        }
    }
}