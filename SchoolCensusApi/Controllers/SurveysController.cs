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
    public class SurveysController : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/Surveys
        public IQueryable<Survey> GetSurveys()
        {
            return db.Surveys;
        }

        // GET: api/Surveys/5
        [ResponseType(typeof(Survey))]
        public IHttpActionResult GetSurvey(string id)
        {
            Survey survey = db.Surveys.Find(id);
            if (survey == null)
            {
                return NotFound();
            }

            return Ok(survey);
        }

        // PUT: api/Surveys/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSurvey(string id, Survey survey)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != survey.SurveyID)
            {
                return BadRequest();
            }

            db.Entry(survey).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SurveyExists(id))
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

        // POST: api/Surveys
        [ResponseType(typeof(Survey))]
        public IHttpActionResult PostSurvey(Survey survey)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Surveys.Add(survey);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (SurveyExists(survey.SurveyID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = survey.SurveyID }, survey);
        }

        // DELETE: api/Surveys/5
        [ResponseType(typeof(Survey))]
        public IHttpActionResult DeleteSurvey(string id)
        {
            Survey survey = db.Surveys.Find(id);
            if (survey == null)
            {
                return NotFound();
            }

            db.Surveys.Remove(survey);
            db.SaveChanges();

            return Ok(survey);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SurveyExists(string id)
        {
            return db.Surveys.Count(e => e.SurveyID == id) > 0;
        }
    }
}