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
    public class KeyPerson1Controller : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/KeyPerson1
        public IQueryable<KeyPerson1> GetKeyPersons1()
        {
            return db.KeyPersons1;
        }

        // GET: api/KeyPerson1/5
        [ResponseType(typeof(KeyPerson1))]
        public IHttpActionResult GetKeyPerson1(int id)
        {
            KeyPerson1 keyPerson1 = db.KeyPersons1.Find(id);
            if (keyPerson1 == null)
            {
                return NotFound();
            }

            return Ok(keyPerson1);
        }

        // PUT: api/KeyPerson1/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutKeyPerson1(int id, KeyPerson1 keyPerson1)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != keyPerson1.KeyPersons_id)
            {
                return BadRequest();
            }

            db.Entry(keyPerson1).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KeyPerson1Exists(id))
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

        // POST: api/KeyPerson1
        [ResponseType(typeof(KeyPerson1))]
        public IHttpActionResult PostKeyPerson1(KeyPerson1 keyPerson1)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.KeyPersons1.Add(keyPerson1);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = keyPerson1.KeyPersons_id }, keyPerson1);
        }

        // DELETE: api/KeyPerson1/5
        [ResponseType(typeof(KeyPerson1))]
        public IHttpActionResult DeleteKeyPerson1(int id)
        {
            KeyPerson1 keyPerson1 = db.KeyPersons1.Find(id);
            if (keyPerson1 == null)
            {
                return NotFound();
            }

            db.KeyPersons1.Remove(keyPerson1);
            db.SaveChanges();

            return Ok(keyPerson1);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KeyPerson1Exists(int id)
        {
            return db.KeyPersons1.Count(e => e.KeyPersons_id == id) > 0;
        }
    }
}