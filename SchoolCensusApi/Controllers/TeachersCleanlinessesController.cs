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
    public class TeachersCleanlinessesController : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/TeachersCleanlinesses
        public IQueryable<TeachersCleanliness> GetTeachersCleanlinesses()
        {
            return db.TeachersCleanlinesses;
        }

        // GET: api/TeachersCleanlinesses/5
        [ResponseType(typeof(TeachersCleanliness))]
        public IHttpActionResult GetTeachersCleanliness(int id)
        {
            TeachersCleanliness teachersCleanliness = db.TeachersCleanlinesses.Find(id);
            if (teachersCleanliness == null)
            {
                return NotFound();
            }

            return Ok(teachersCleanliness);
        }

        // PUT: api/TeachersCleanlinesses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTeachersCleanliness(int id, TeachersCleanliness teachersCleanliness)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != teachersCleanliness.ID)
            {
                return BadRequest();
            }

            db.Entry(teachersCleanliness).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeachersCleanlinessExists(id))
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

        // POST: api/TeachersCleanlinesses
        public HttpResponseMessage PostTeachersCleanliness([FromBody] string teachersCleanliness)
        {
            if (!ModelState.IsValid)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }

            TeacherCleanlinessModel sm = JsonConvert.DeserializeObject<TeacherCleanlinessModel>(teachersCleanliness);
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

            TeachersCleanliness tc = new TeachersCleanliness()
            {
                Entity = sm.Teacherscleanliness.Entity,
                SurveyID = sm.Teacherscleanliness.SurveyID,
                Section = sm.Teacherscleanliness.Section,
                Cleanliness = sm.Teacherscleanliness.Cleanliness,
                CreatedOn = DateTime.UtcNow,
                ModifiedOn = DateTime.UtcNow,
            };
            db.TeachersCleanlinesses.Add(tc);
            db.SaveChanges();

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        // DELETE: api/TeachersCleanlinesses/5
        [ResponseType(typeof(TeachersCleanliness))]
        public IHttpActionResult DeleteTeachersCleanliness(int id)
        {
            TeachersCleanliness teachersCleanliness = db.TeachersCleanlinesses.Find(id);
            if (teachersCleanliness == null)
            {
                return NotFound();
            }

            db.TeachersCleanlinesses.Remove(teachersCleanliness);
            db.SaveChanges();

            return Ok(teachersCleanliness);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TeachersCleanlinessExists(int id)
        {
            return db.TeachersCleanlinesses.Count(e => e.ID == id) > 0;
        }
    }
}