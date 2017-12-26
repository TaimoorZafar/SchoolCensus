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
    public class AreaCoveredsController : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/AreaCovereds
        public IQueryable<AreaCovered> GetAreaCovereds()
        {
            return db.AreaCovereds;
        }

        // GET: api/AreaCovereds/5
        [ResponseType(typeof(AreaCovered))]
        public IHttpActionResult GetAreaCovered(string id)
        {
            AreaCovered areaCovered = db.AreaCovereds.Find(id);
            if (areaCovered == null)
            {
                return NotFound();
            }

            return Ok(areaCovered);
        }

        // PUT: api/AreaCovereds/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAreaCovered(string id, AreaCovered areaCovered)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != areaCovered.CoveredOrUncovered)
            {
                return BadRequest();
            }

            db.Entry(areaCovered).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AreaCoveredExists(id))
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

        // POST: api/AreaCovereds
        [ResponseType(typeof(AreaCovered))]
        public IHttpActionResult PostAreaCovered(AreaCovered areaCovered)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AreaCovereds.Add(areaCovered);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (AreaCoveredExists(areaCovered.CoveredOrUncovered))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = areaCovered.CoveredOrUncovered }, areaCovered);
        }

        // DELETE: api/AreaCovereds/5
        [ResponseType(typeof(AreaCovered))]
        public IHttpActionResult DeleteAreaCovered(string id)
        {
            AreaCovered areaCovered = db.AreaCovereds.Find(id);
            if (areaCovered == null)
            {
                return NotFound();
            }

            db.AreaCovereds.Remove(areaCovered);
            db.SaveChanges();

            return Ok(areaCovered);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AreaCoveredExists(string id)
        {
            return db.AreaCovereds.Count(e => e.CoveredOrUncovered == id) > 0;
        }
    }
}