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
    public class LicenseeTypesController : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/LicenseeTypes
        public IQueryable<LicenseeType> GetLicenseeTypes()
        {
            return db.LicenseeTypes;
        }

        // GET: api/LicenseeTypes/5
        [ResponseType(typeof(LicenseeType))]
        public IHttpActionResult GetLicenseeType(string id)
        {
            LicenseeType licenseeType = db.LicenseeTypes.Find(id);
            if (licenseeType == null)
            {
                return NotFound();
            }

            return Ok(licenseeType);
        }

        // PUT: api/LicenseeTypes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLicenseeType(string id, LicenseeType licenseeType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != licenseeType.LicenseeType1)
            {
                return BadRequest();
            }

            db.Entry(licenseeType).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LicenseeTypeExists(id))
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

        // POST: api/LicenseeTypes
        [ResponseType(typeof(LicenseeType))]
        public IHttpActionResult PostLicenseeType(LicenseeType licenseeType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.LicenseeTypes.Add(licenseeType);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (LicenseeTypeExists(licenseeType.LicenseeType1))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = licenseeType.LicenseeType1 }, licenseeType);
        }

        // DELETE: api/LicenseeTypes/5
        [ResponseType(typeof(LicenseeType))]
        public IHttpActionResult DeleteLicenseeType(string id)
        {
            LicenseeType licenseeType = db.LicenseeTypes.Find(id);
            if (licenseeType == null)
            {
                return NotFound();
            }

            db.LicenseeTypes.Remove(licenseeType);
            db.SaveChanges();

            return Ok(licenseeType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LicenseeTypeExists(string id)
        {
            return db.LicenseeTypes.Count(e => e.LicenseeType1 == id) > 0;
        }
    }
}