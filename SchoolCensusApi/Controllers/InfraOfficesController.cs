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
    public class InfraOfficesController : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/InfraOffices
        public IQueryable<InfraOffice> GetInfraOffices()
        {
            return db.InfraOffices;
        }

        // GET: api/InfraOffices/5
        [ResponseType(typeof(InfraOffice))]
        public IHttpActionResult GetInfraOffice(int id)
        {
            InfraOffice infraOffice = db.InfraOffices.Find(id);
            if (infraOffice == null)
            {
                return NotFound();
            }

            return Ok(infraOffice);
        }

        // PUT: api/InfraOffices/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutInfraOffice(int id, InfraOffice infraOffice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != infraOffice.InfraOffices_id)
            {
                return BadRequest();
            }

            db.Entry(infraOffice).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InfraOfficeExists(id))
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

        // POST: api/InfraOffices
        [ResponseType(typeof(InfraOffice))]
        public IHttpActionResult PostInfraOffice(InfraOffice infraOffice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.InfraOffices.Add(infraOffice);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = infraOffice.InfraOffices_id }, infraOffice);
        }

        // DELETE: api/InfraOffices/5
        [ResponseType(typeof(InfraOffice))]
        public IHttpActionResult DeleteInfraOffice(int id)
        {
            InfraOffice infraOffice = db.InfraOffices.Find(id);
            if (infraOffice == null)
            {
                return NotFound();
            }

            db.InfraOffices.Remove(infraOffice);
            db.SaveChanges();

            return Ok(infraOffice);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InfraOfficeExists(int id)
        {
            return db.InfraOffices.Count(e => e.InfraOffices_id == id) > 0;
        }
    }
}