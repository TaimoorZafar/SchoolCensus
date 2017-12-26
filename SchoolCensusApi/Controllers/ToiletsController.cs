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
    public class ToiletsController : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/Toilets
        public IQueryable<Toilet> GetToilets()
        {
            return db.Toilets;
        }

        // GET: api/Toilets/5
        [ResponseType(typeof(Toilet))]
        public IHttpActionResult GetToilet(int id)
        {
            Toilet toilet = db.Toilets.Find(id);
            if (toilet == null)
            {
                return NotFound();
            }

            return Ok(toilet);
        }

        // PUT: api/Toilets/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutToilet(int id, Toilet toilet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != toilet.Toilets_id)
            {
                return BadRequest();
            }

            db.Entry(toilet).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToiletExists(id))
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

        // POST: api/Toilets
        [ResponseType(typeof(Toilet))]
        public IHttpActionResult PostToilet(Toilet toilet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Toilets.Add(toilet);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = toilet.Toilets_id }, toilet);
        }

        // DELETE: api/Toilets/5
        [ResponseType(typeof(Toilet))]
        public IHttpActionResult DeleteToilet(int id)
        {
            Toilet toilet = db.Toilets.Find(id);
            if (toilet == null)
            {
                return NotFound();
            }

            db.Toilets.Remove(toilet);
            db.SaveChanges();

            return Ok(toilet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ToiletExists(int id)
        {
            return db.Toilets.Count(e => e.Toilets_id == id) > 0;
        }
    }
}