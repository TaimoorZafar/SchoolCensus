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
    public class SchoolAccessesController : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/SchoolAccesses
        public IQueryable<SchoolAccess> GetSchoolAccesses()
        {
            return db.SchoolAccesses;
        }

        // GET: api/SchoolAccesses/5
        [ResponseType(typeof(SchoolAccess))]
        public IHttpActionResult GetSchoolAccess(int id)
        {
            SchoolAccess schoolAccess = db.SchoolAccesses.Find(id);
            if (schoolAccess == null)
            {
                return NotFound();
            }

            return Ok(schoolAccess);
        }

        // PUT: api/SchoolAccesses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSchoolAccess(int id, SchoolAccess schoolAccess)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != schoolAccess.SchoolAccesses_id)
            {
                return BadRequest();
            }

            db.Entry(schoolAccess).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SchoolAccessExists(id))
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

        // POST: api/SchoolAccesses
        [ResponseType(typeof(SchoolAccess))]
        public IHttpActionResult PostSchoolAccess([FromBody] string schoolAccess)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SchoolAccessModel sm = JsonConvert.DeserializeObject<SchoolAccessModel>(schoolAccess);
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

        // DELETE: api/SchoolAccesses/5
        [ResponseType(typeof(SchoolAccess))]
        public IHttpActionResult DeleteSchoolAccess(int id)
        {
            SchoolAccess schoolAccess = db.SchoolAccesses.Find(id);
            if (schoolAccess == null)
            {
                return NotFound();
            }

            db.SchoolAccesses.Remove(schoolAccess);
            db.SaveChanges();

            return Ok(schoolAccess);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SchoolAccessExists(int id)
        {
            return db.SchoolAccesses.Count(e => e.SchoolAccesses_id == id) > 0;
        }
    }
}