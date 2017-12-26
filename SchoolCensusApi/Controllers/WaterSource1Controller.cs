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
    public class WaterSource1Controller : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/WaterSource1
        public IQueryable<WaterSource1> GetWaterSources1()
        {
            return db.WaterSources1;
        }

        // GET: api/WaterSource1/5
        [ResponseType(typeof(WaterSource1))]
        public IHttpActionResult GetWaterSource1(int id)
        {
            WaterSource1 waterSource1 = db.WaterSources1.Find(id);
            if (waterSource1 == null)
            {
                return NotFound();
            }

            return Ok(waterSource1);
        }

        // PUT: api/WaterSource1/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutWaterSource1(int id, WaterSource1 waterSource1)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != waterSource1.WaterSources_id)
            {
                return BadRequest();
            }

            db.Entry(waterSource1).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WaterSource1Exists(id))
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

        // POST: api/WaterSource1
        [ResponseType(typeof(WaterSource1))]
        public IHttpActionResult PostWaterSource1(WaterSource1 waterSource1)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.WaterSources1.Add(waterSource1);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = waterSource1.WaterSources_id }, waterSource1);
        }

        // DELETE: api/WaterSource1/5
        [ResponseType(typeof(WaterSource1))]
        public IHttpActionResult DeleteWaterSource1(int id)
        {
            WaterSource1 waterSource1 = db.WaterSources1.Find(id);
            if (waterSource1 == null)
            {
                return NotFound();
            }

            db.WaterSources1.Remove(waterSource1);
            db.SaveChanges();

            return Ok(waterSource1);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WaterSource1Exists(int id)
        {
            return db.WaterSources1.Count(e => e.WaterSources_id == id) > 0;
        }
    }
}