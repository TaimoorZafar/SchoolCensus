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
    public class UomAreasController : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/UomAreas
        public IQueryable<UomArea> GetUomAreas()
        {
            return db.UomAreas;
        }

        // GET: api/UomAreas/5
        [ResponseType(typeof(UomArea))]
        public IHttpActionResult GetUomArea(string id)
        {
            UomArea uomArea = db.UomAreas.Find(id);
            if (uomArea == null)
            {
                return NotFound();
            }

            return Ok(uomArea);
        }

        // PUT: api/UomAreas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUomArea(string id, UomArea uomArea)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != uomArea.Uom)
            {
                return BadRequest();
            }

            db.Entry(uomArea).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UomAreaExists(id))
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

        // POST: api/UomAreas
        [ResponseType(typeof(UomArea))]
        public IHttpActionResult PostUomArea(UomArea uomArea)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UomAreas.Add(uomArea);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (UomAreaExists(uomArea.Uom))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = uomArea.Uom }, uomArea);
        }

        // DELETE: api/UomAreas/5
        [ResponseType(typeof(UomArea))]
        public IHttpActionResult DeleteUomArea(string id)
        {
            UomArea uomArea = db.UomAreas.Find(id);
            if (uomArea == null)
            {
                return NotFound();
            }

            db.UomAreas.Remove(uomArea);
            db.SaveChanges();

            return Ok(uomArea);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UomAreaExists(string id)
        {
            return db.UomAreas.Count(e => e.Uom == id) > 0;
        }
    }
}