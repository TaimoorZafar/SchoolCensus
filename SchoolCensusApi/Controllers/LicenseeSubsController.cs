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
    public class LicenseeSubsController : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/LicenseeSubs
        public IQueryable<LicenseeSub> GetLicenseeSubs()
        {
            return db.LicenseeSubs;
        }

        // GET: api/LicenseeSubs/5
        [ResponseType(typeof(LicenseeSub))]
        public IHttpActionResult GetLicenseeSub(string id)
        {
            LicenseeSub licenseeSub = db.LicenseeSubs.Find(id);
            if (licenseeSub == null)
            {
                return NotFound();
            }

            return Ok(licenseeSub);
        }

        // PUT: api/LicenseeSubs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLicenseeSub(string id, LicenseeSub licenseeSub)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != licenseeSub.LicenseeSubType)
            {
                return BadRequest();
            }

            db.Entry(licenseeSub).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LicenseeSubExists(id))
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

        // POST: api/LicenseeSubs
        [ResponseType(typeof(LicenseeSub))]
        public IHttpActionResult PostLicenseeSub(LicenseeSub licenseeSub)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.LicenseeSubs.Add(licenseeSub);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (LicenseeSubExists(licenseeSub.LicenseeSubType))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = licenseeSub.LicenseeSubType }, licenseeSub);
        }

        // DELETE: api/LicenseeSubs/5
        [ResponseType(typeof(LicenseeSub))]
        public IHttpActionResult DeleteLicenseeSub(string id)
        {
            LicenseeSub licenseeSub = db.LicenseeSubs.Find(id);
            if (licenseeSub == null)
            {
                return NotFound();
            }

            db.LicenseeSubs.Remove(licenseeSub);
            db.SaveChanges();

            return Ok(licenseeSub);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LicenseeSubExists(string id)
        {
            return db.LicenseeSubs.Count(e => e.LicenseeSubType == id) > 0;
        }
    }
}