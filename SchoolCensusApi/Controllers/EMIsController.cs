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
    public class EMIsController : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/EMIs
        public IQueryable<EMI> GetEMIS()
        {
            return db.EMIS;
        }

        // GET: api/EMIs/5
        [ResponseType(typeof(EMI))]
        public IHttpActionResult GetEMI(string id)
        {
            EMI eMI = db.EMIS.Find(id);
            if (eMI == null)
            {
                return NotFound();
            }

            return Ok(eMI);
        }

        // PUT: api/EMIs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEMI(string id, EMI eMI)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != eMI.EmisCode)
            {
                return BadRequest();
            }

            db.Entry(eMI).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EMIExists(id))
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

        // POST: api/EMIs
        [ResponseType(typeof(EMI))]
        public IHttpActionResult PostEMI(EMI eMI)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EMIS.Add(eMI);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (EMIExists(eMI.EmisCode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = eMI.EmisCode }, eMI);
        }

        // DELETE: api/EMIs/5
        [ResponseType(typeof(EMI))]
        public IHttpActionResult DeleteEMI(string id)
        {
            EMI eMI = db.EMIS.Find(id);
            if (eMI == null)
            {
                return NotFound();
            }

            db.EMIS.Remove(eMI);
            db.SaveChanges();

            return Ok(eMI);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EMIExists(string id)
        {
            return db.EMIS.Count(e => e.EmisCode == id) > 0;
        }
    }
}