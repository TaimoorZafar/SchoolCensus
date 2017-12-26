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
    public class ConstructionTypesController : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/ConstructionTypes
        public IQueryable<ConstructionType> GetConstructionTypes()
        {
            return db.ConstructionTypes;
        }

        // GET: api/ConstructionTypes/5
        [ResponseType(typeof(ConstructionType))]
        public IHttpActionResult GetConstructionType(string id)
        {
            ConstructionType constructionType = db.ConstructionTypes.Find(id);
            if (constructionType == null)
            {
                return NotFound();
            }

            return Ok(constructionType);
        }

        // PUT: api/ConstructionTypes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutConstructionType(string id, ConstructionType constructionType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != constructionType.ConstructionType1)
            {
                return BadRequest();
            }

            db.Entry(constructionType).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConstructionTypeExists(id))
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

        // POST: api/ConstructionTypes
        [ResponseType(typeof(ConstructionType))]
        public IHttpActionResult PostConstructionType(ConstructionType constructionType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ConstructionTypes.Add(constructionType);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ConstructionTypeExists(constructionType.ConstructionType1))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = constructionType.ConstructionType1 }, constructionType);
        }

        // DELETE: api/ConstructionTypes/5
        [ResponseType(typeof(ConstructionType))]
        public IHttpActionResult DeleteConstructionType(string id)
        {
            ConstructionType constructionType = db.ConstructionTypes.Find(id);
            if (constructionType == null)
            {
                return NotFound();
            }

            db.ConstructionTypes.Remove(constructionType);
            db.SaveChanges();

            return Ok(constructionType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ConstructionTypeExists(string id)
        {
            return db.ConstructionTypes.Count(e => e.ConstructionType1 == id) > 0;
        }
    }
}