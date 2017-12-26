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
    public class EmergencyContactsController : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/EmergencyContacts
        public IQueryable<EmergencyContact> GetEmergencyContacts()
        {
            return db.EmergencyContacts;
        }

        // GET: api/EmergencyContacts/5
        [ResponseType(typeof(EmergencyContact))]
        public IHttpActionResult GetEmergencyContact(int id)
        {
            EmergencyContact emergencyContact = db.EmergencyContacts.Find(id);
            if (emergencyContact == null)
            {
                return NotFound();
            }

            return Ok(emergencyContact);
        }

        // PUT: api/EmergencyContacts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmergencyContact(int id, EmergencyContact emergencyContact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != emergencyContact.EmergencyContacts_id)
            {
                return BadRequest();
            }

            db.Entry(emergencyContact).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmergencyContactExists(id))
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

        // POST: api/EmergencyContacts
        [ResponseType(typeof(EmergencyContact))]
        public IHttpActionResult PostEmergencyContact(EmergencyContact emergencyContact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EmergencyContacts.Add(emergencyContact);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = emergencyContact.EmergencyContacts_id }, emergencyContact);
        }

        // DELETE: api/EmergencyContacts/5
        [ResponseType(typeof(EmergencyContact))]
        public IHttpActionResult DeleteEmergencyContact(int id)
        {
            EmergencyContact emergencyContact = db.EmergencyContacts.Find(id);
            if (emergencyContact == null)
            {
                return NotFound();
            }

            db.EmergencyContacts.Remove(emergencyContact);
            db.SaveChanges();

            return Ok(emergencyContact);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmergencyContactExists(int id)
        {
            return db.EmergencyContacts.Count(e => e.EmergencyContacts_id == id) > 0;
        }
    }
}