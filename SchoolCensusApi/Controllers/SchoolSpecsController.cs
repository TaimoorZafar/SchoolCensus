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
    public class SchoolSpecsController : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/SchoolSpecs
        public IQueryable<SchoolSpec> GetSchoolSpecs()
        {
            return db.SchoolSpecs;
        }

        // GET: api/SchoolSpecs/5
        [ResponseType(typeof(SchoolSpec))]
        public IHttpActionResult GetSchoolSpec(int id)
        {
            SchoolSpec schoolSpec = db.SchoolSpecs.Find(id);
            if (schoolSpec == null)
            {
                return NotFound();
            }

            return Ok(schoolSpec);
        }

        // PUT: api/SchoolSpecs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSchoolSpec(int id, SchoolSpec schoolSpec)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != schoolSpec.SchoolSpecs_id)
            {
                return BadRequest();
            }

            db.Entry(schoolSpec).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SchoolSpecExists(id))
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

        // POST: api/SchoolSpecs
        [ResponseType(typeof(SchoolSpec))]
        public IHttpActionResult PostSchoolSpec([FromBody] string schoolSpec)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SchoolSpecsModel sm = JsonConvert.DeserializeObject<SchoolSpecsModel>(schoolSpec);
            EMI emi = new EMI()
            {
                EmisCode = sm.Generalmodel.Emi.EmisCode,
                NameOfSchool = sm.Generalmodel.Emi.NameOfSchool,
                CreatedOn = DateTime.UtcNow,
                ModifiedOn = DateTime.UtcNow
            };
            db.EMIS.Add(emi);
            db.SaveChanges();
            Comment c = new Comment()
            {
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
            List<Picture> res = sm.Generalmodel.Img.Select(x => new Picture
            {
                Entity = x.Entity,
                SurveyID = x.SurveyID,
                Section = x.Section,
                CreatedOn = DateTime.UtcNow,
                ModifiedOn = DateTime.UtcNow,
                Picture1 = x.Picture1
            }).ToList();
            db.Pictures.AddRange(res);
            db.SaveChanges();



            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        // DELETE: api/SchoolSpecs/5
        [ResponseType(typeof(SchoolSpec))]
        public IHttpActionResult DeleteSchoolSpec(int id)
        {
            SchoolSpec schoolSpec = db.SchoolSpecs.Find(id);
            if (schoolSpec == null)
            {
                return NotFound();
            }

            db.SchoolSpecs.Remove(schoolSpec);
            db.SaveChanges();

            return Ok(schoolSpec);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SchoolSpecExists(int id)
        {
            return db.SchoolSpecs.Count(e => e.SchoolSpecs_id == id) > 0;
        }
    }
}