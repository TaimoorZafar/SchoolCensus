﻿using System;
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
    public class GendersController : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/Genders
        public IQueryable<Gender> GetGenders()
        {
            return db.Genders;
        }

        // GET: api/Genders/5
        [ResponseType(typeof(Gender))]
        public IHttpActionResult GetGender(string id)
        {
            Gender gender = db.Genders.Find(id);
            if (gender == null)
            {
                return NotFound();
            }

            return Ok(gender);
        }

        // PUT: api/Genders/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGender(string id, Gender gender)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != gender.Gender1)
            {
                return BadRequest();
            }

            db.Entry(gender).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenderExists(id))
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

        // POST: api/Genders
        [ResponseType(typeof(Gender))]
        public IHttpActionResult PostGender(Gender gender)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Genders.Add(gender);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (GenderExists(gender.Gender1))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = gender.Gender1 }, gender);
        }

        // DELETE: api/Genders/5
        [ResponseType(typeof(Gender))]
        public IHttpActionResult DeleteGender(string id)
        {
            Gender gender = db.Genders.Find(id);
            if (gender == null)
            {
                return NotFound();
            }

            db.Genders.Remove(gender);
            db.SaveChanges();

            return Ok(gender);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GenderExists(string id)
        {
            return db.Genders.Count(e => e.Gender1 == id) > 0;
        }
    }
}