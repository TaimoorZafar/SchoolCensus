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
    public class PopulationTypesController : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/PopulationTypes
        public IQueryable<PopulationType> GetPopulationTypes()
        {
            return db.PopulationTypes;
        }

        // GET: api/PopulationTypes/5
        [ResponseType(typeof(PopulationType))]
        public IHttpActionResult GetPopulationType(string id)
        {
            PopulationType populationType = db.PopulationTypes.Find(id);
            if (populationType == null)
            {
                return NotFound();
            }

            return Ok(populationType);
        }

        // PUT: api/PopulationTypes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPopulationType(string id, PopulationType populationType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != populationType.PopulationType1)
            {
                return BadRequest();
            }

            db.Entry(populationType).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PopulationTypeExists(id))
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

        // POST: api/PopulationTypes
        [ResponseType(typeof(PopulationType))]
        public IHttpActionResult PostPopulationType(PopulationType populationType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PopulationTypes.Add(populationType);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PopulationTypeExists(populationType.PopulationType1))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = populationType.PopulationType1 }, populationType);
        }

        // DELETE: api/PopulationTypes/5
        [ResponseType(typeof(PopulationType))]
        public IHttpActionResult DeletePopulationType(string id)
        {
            PopulationType populationType = db.PopulationTypes.Find(id);
            if (populationType == null)
            {
                return NotFound();
            }

            db.PopulationTypes.Remove(populationType);
            db.SaveChanges();

            return Ok(populationType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PopulationTypeExists(string id)
        {
            return db.PopulationTypes.Count(e => e.PopulationType1 == id) > 0;
        }
    }
}