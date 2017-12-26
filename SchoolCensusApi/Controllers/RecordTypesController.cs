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
    public class RecordTypesController : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/RecordTypes
        public IQueryable<RecordType> GetRecordTypes()
        {
            return db.RecordTypes;
        }

        // GET: api/RecordTypes/5
        [ResponseType(typeof(RecordType))]
        public IHttpActionResult GetRecordType(string id)
        {
            RecordType recordType = db.RecordTypes.Find(id);
            if (recordType == null)
            {
                return NotFound();
            }

            return Ok(recordType);
        }

        // PUT: api/RecordTypes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRecordType(string id, RecordType recordType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != recordType.RecordType1)
            {
                return BadRequest();
            }

            db.Entry(recordType).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecordTypeExists(id))
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

        // POST: api/RecordTypes
        [ResponseType(typeof(RecordType))]
        public IHttpActionResult PostRecordType(RecordType recordType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RecordTypes.Add(recordType);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (RecordTypeExists(recordType.RecordType1))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = recordType.RecordType1 }, recordType);
        }

        // DELETE: api/RecordTypes/5
        [ResponseType(typeof(RecordType))]
        public IHttpActionResult DeleteRecordType(string id)
        {
            RecordType recordType = db.RecordTypes.Find(id);
            if (recordType == null)
            {
                return NotFound();
            }

            db.RecordTypes.Remove(recordType);
            db.SaveChanges();

            return Ok(recordType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RecordTypeExists(string id)
        {
            return db.RecordTypes.Count(e => e.RecordType1 == id) > 0;
        }
    }
}