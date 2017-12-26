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
    public class ToiletFuncsController : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/ToiletFuncs
        public IQueryable<ToiletFunc> GetToiletFuncs()
        {
            return db.ToiletFuncs;
        }

        // GET: api/ToiletFuncs/5
        [ResponseType(typeof(ToiletFunc))]
        public IHttpActionResult GetToiletFunc(int id)
        {
            ToiletFunc toiletFunc = db.ToiletFuncs.Find(id);
            if (toiletFunc == null)
            {
                return NotFound();
            }

            return Ok(toiletFunc);
        }

        // PUT: api/ToiletFuncs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutToiletFunc(int id, ToiletFunc toiletFunc)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != toiletFunc.ToiletFuncs_id)
            {
                return BadRequest();
            }

            db.Entry(toiletFunc).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToiletFuncExists(id))
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

        // POST: api/ToiletFuncs
        [ResponseType(typeof(ToiletFunc))]
        public IHttpActionResult PostToiletFunc(ToiletFunc toiletFunc)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ToiletFuncs.Add(toiletFunc);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = toiletFunc.ToiletFuncs_id }, toiletFunc);
        }

        // DELETE: api/ToiletFuncs/5
        [ResponseType(typeof(ToiletFunc))]
        public IHttpActionResult DeleteToiletFunc(int id)
        {
            ToiletFunc toiletFunc = db.ToiletFuncs.Find(id);
            if (toiletFunc == null)
            {
                return NotFound();
            }

            db.ToiletFuncs.Remove(toiletFunc);
            db.SaveChanges();

            return Ok(toiletFunc);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ToiletFuncExists(int id)
        {
            return db.ToiletFuncs.Count(e => e.ToiletFuncs_id == id) > 0;
        }
    }
}