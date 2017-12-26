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
    public class WaterSourcesController : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/WaterSources
        public IQueryable<WaterSource> GetWaterSources()
        {
            return db.WaterSources;
        }

        // GET: api/WaterSources/5
        [ResponseType(typeof(WaterSource))]
        public IHttpActionResult GetWaterSource(string id)
        {
            WaterSource waterSource = db.WaterSources.Find(id);
            if (waterSource == null)
            {
                return NotFound();
            }

            return Ok(waterSource);
        }

        // PUT: api/WaterSources/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutWaterSource(string id, WaterSource waterSource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != waterSource.FunctionalWaterSource)
            {
                return BadRequest();
            }

            db.Entry(waterSource).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WaterSourceExists(id))
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

        // POST: api/WaterSources
        [ResponseType(typeof(WaterSource))]
        public IHttpActionResult PostWaterSource(WaterSource waterSource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.WaterSources.Add(waterSource);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (WaterSourceExists(waterSource.FunctionalWaterSource))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = waterSource.FunctionalWaterSource }, waterSource);
        }

        // DELETE: api/WaterSources/5
        [ResponseType(typeof(WaterSource))]
        public IHttpActionResult DeleteWaterSource(string id)
        {
            WaterSource waterSource = db.WaterSources.Find(id);
            if (waterSource == null)
            {
                return NotFound();
            }

            db.WaterSources.Remove(waterSource);
            db.SaveChanges();

            return Ok(waterSource);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WaterSourceExists(string id)
        {
            return db.WaterSources.Count(e => e.FunctionalWaterSource == id) > 0;
        }
    }
}