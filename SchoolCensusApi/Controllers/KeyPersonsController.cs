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
    public class KeyPersonsController : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/KeyPersons
        public IQueryable<KeyPerson> GetKeyPersons()
        {
            return db.KeyPersons;
        }

        // GET: api/KeyPersons/5
        [ResponseType(typeof(KeyPerson))]
        public IHttpActionResult GetKeyPerson(string id)
        {
            KeyPerson keyPerson = db.KeyPersons.Find(id);
            if (keyPerson == null)
            {
                return NotFound();
            }

            return Ok(keyPerson);
        }

        // PUT: api/KeyPersons/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutKeyPerson(string id, KeyPerson keyPerson)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != keyPerson.KeyPerson1)
            {
                return BadRequest();
            }

            db.Entry(keyPerson).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KeyPersonExists(id))
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

        // POST: api/KeyPersons
        [ResponseType(typeof(KeyPerson))]
        public IHttpActionResult PostKeyPerson(KeyPerson keyPerson)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.KeyPersons.Add(keyPerson);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (KeyPersonExists(keyPerson.KeyPerson1))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = keyPerson.KeyPerson1 }, keyPerson);
        }

        // DELETE: api/KeyPersons/5
        [ResponseType(typeof(KeyPerson))]
        public IHttpActionResult DeleteKeyPerson(string id)
        {
            KeyPerson keyPerson = db.KeyPersons.Find(id);
            if (keyPerson == null)
            {
                return NotFound();
            }

            db.KeyPersons.Remove(keyPerson);
            db.SaveChanges();

            return Ok(keyPerson);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KeyPersonExists(string id)
        {
            return db.KeyPersons.Count(e => e.KeyPerson1 == id) > 0;
        }
    }
}