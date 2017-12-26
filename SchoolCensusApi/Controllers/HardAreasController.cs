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
    public class HardAreasController : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/HardAreas
        public IQueryable<HardArea> GetHardAreas()
        {
            return db.HardAreas;
        }

        // GET: api/HardAreas/5
        [ResponseType(typeof(HardArea))]
        public IHttpActionResult GetHardArea(string id)
        {
            HardArea hardArea = db.HardAreas.Find(id);
            if (hardArea == null)
            {
                return NotFound();
            }

            return Ok(hardArea);
        }

        // PUT: api/HardAreas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHardArea(string id, HardArea hardArea)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hardArea.IsSchoolLocatedInHardAreas)
            {
                return BadRequest();
            }

            db.Entry(hardArea).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HardAreaExists(id))
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

        // POST: api/HardAreas
        [ResponseType(typeof(HardArea))]
        public IHttpActionResult PostHardArea(HardArea hardArea)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.HardAreas.Add(hardArea);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (HardAreaExists(hardArea.IsSchoolLocatedInHardAreas))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = hardArea.IsSchoolLocatedInHardAreas }, hardArea);
        }

        // DELETE: api/HardAreas/5
        [ResponseType(typeof(HardArea))]
        public IHttpActionResult DeleteHardArea(string id)
        {
            HardArea hardArea = db.HardAreas.Find(id);
            if (hardArea == null)
            {
                return NotFound();
            }

            db.HardAreas.Remove(hardArea);
            db.SaveChanges();

            return Ok(hardArea);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HardAreaExists(string id)
        {
            return db.HardAreas.Count(e => e.IsSchoolLocatedInHardAreas == id) > 0;
        }
    }
}