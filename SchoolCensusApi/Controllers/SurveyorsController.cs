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
using Newtonsoft.Json;

namespace SchoolCensusApi.Controllers
{
    public class SurveyorsController : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/Surveyors
        public IQueryable<Surveyor> GetSurveyors()
        {
            return db.Surveyors;
        }

        // GET: api/Surveyors/5
        [ResponseType(typeof(Surveyor))]
        public IHttpActionResult GetSurveyor(int id)
        {
            Surveyor surveyor = db.Surveyors.Find(id);
            if (surveyor == null)
            {
                return NotFound();
            }

            return Ok(surveyor);
        }

        // PUT: api/Surveyors/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSurveyor(int id, Surveyor surveyor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != surveyor.fSurveyor_id)
            {
                return BadRequest();
            }

            db.Entry(surveyor).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SurveyorExists(id))
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

        // POST: api/Surveyors
        public HttpResponseMessage PostSurveyor( [FromBody] string surveyor)
        {
            if (!ModelState.IsValid)
            {
                 return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            SurveyorsModel sm = JsonConvert.DeserializeObject<SurveyorsModel>(surveyor);

            EMI emi = new EMI() {
                EmisCode = sm.Generalmodel.Emi.EmisCode,
                NameOfSchool = sm.Generalmodel.Emi.NameOfSchool,
                CreatedOn = DateTime.UtcNow,
                ModifiedOn = DateTime.UtcNow
            };
            db.EMIS.Add(emi);
            db.SaveChanges();
            Comment c = new Comment() {
                Entity = sm.Generalmodel.Comment.Entity,
                SurveyID = sm.Generalmodel.Comment.SurveyID,
                Section = sm.Generalmodel.Comment.Section,
                CreatedOn = DateTime.UtcNow,
                ModifiedOn = DateTime.UtcNow,
                Comments = sm.Generalmodel.Comment.Comments
            };
            db.Comments.Add(c);
            db.SaveChanges();
            SpecialFacility sf = new SpecialFacility()
            {
                Entity = sm.Generalmodel.Specialfacility.Entity,
                SurveyID = sm.Generalmodel.Specialfacility.SurveyID,
                Section = sm.Generalmodel.Specialfacility.Section,
                CreatedOn = DateTime.UtcNow,
                ModifiedOn = DateTime.UtcNow,
                AnyFacilitiesForDisableStudents = sm.Generalmodel.Specialfacility.AnyFacilitiesForDisableStudents,
                Description = sm.Generalmodel.Specialfacility.Description
            };
            db.SpecialFacilities.Add(sf);
            db.SaveChanges();
            List<Picture> res = sm.Generalmodel.Img.Select(x => new Picture {
                Entity = x.Entity,
                SurveyID = x.SurveyID,
                Section = x.Section,
                CreatedOn = DateTime.UtcNow,
                ModifiedOn = DateTime.UtcNow,
                Picture1 = x.Picture1
            }).ToList();
            db.Pictures.AddRange(res);
            db.SaveChanges();
            
            Surveyor s = new Surveyor()
            {
                Entity = sm.Surveyor.Entity,
                SurveyID = sm.Surveyor.SurveyID,
                Section = sm.Surveyor.Section,
                CreatedOn = DateTime.UtcNow,
                ModifiedOn = DateTime.UtcNow,
                NameOfMonitor = sm.Surveyor.NameOfMonitor,
                Department = sm.Surveyor.Department
            };
            db.Surveyors.Add(s);
            db.SaveChanges();

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        // DELETE: api/Surveyors/5
        [ResponseType(typeof(Surveyor))]
        public IHttpActionResult DeleteSurveyor(int id)
        {
            Surveyor surveyor = db.Surveyors.Find(id);
            if (surveyor == null)
            {
                return NotFound();
            }

            db.Surveyors.Remove(surveyor);
            db.SaveChanges();

            return Ok(surveyor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SurveyorExists(int id)
        {
            return db.Surveyors.Count(e => e.fSurveyor_id == id) > 0;
        }
    }
}