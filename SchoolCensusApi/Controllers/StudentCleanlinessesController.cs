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
    public class StudentCleanlinessesController : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/StudentCleanlinesses
        public IQueryable<StudentCleanliness> GetStudentCleanlinesses()
        {
            return db.StudentCleanlinesses;
        }

        // GET: api/StudentCleanlinesses/5
        [ResponseType(typeof(StudentCleanliness))]
        public IHttpActionResult GetStudentCleanliness(int id)
        {
            StudentCleanliness studentCleanliness = db.StudentCleanlinesses.Find(id);
            if (studentCleanliness == null)
            {
                return NotFound();
            }

            return Ok(studentCleanliness);
        }

        // PUT: api/StudentCleanlinesses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStudentCleanliness(int id, StudentCleanliness studentCleanliness)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != studentCleanliness.ID)
            {
                return BadRequest();
            }

            db.Entry(studentCleanliness).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentCleanlinessExists(id))
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

        // POST: api/StudentCleanlinesses
        [ResponseType(typeof(StudentCleanliness))]
        public HttpResponseMessage PostStudentCleanliness([FromBody] string studentCleanliness)
        {
            if (!ModelState.IsValid)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);

            }
            StudentCleanlinessModel sm = JsonConvert.DeserializeObject<StudentCleanlinessModel>(studentCleanliness);
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

            StudentCleanliness tc = new StudentCleanliness()
            {
                Entity = sm.Studentcleanliness.Entity,
                SurveyID = sm.Studentcleanliness.SurveyID,
                Section = sm.Studentcleanliness.Section,
                Cleanliness = sm.Studentcleanliness.Cleanliness,
                CreatedOn = DateTime.UtcNow,
                ModifiedOn = DateTime.UtcNow,
            };
            db.StudentCleanlinesses.Add(tc);
            db.SaveChanges();
            return new HttpResponseMessage(HttpStatusCode.OK);

        }

        // DELETE: api/StudentCleanlinesses/5
        [ResponseType(typeof(StudentCleanliness))]
        public IHttpActionResult DeleteStudentCleanliness(int id)
        {
            StudentCleanliness studentCleanliness = db.StudentCleanlinesses.Find(id);
            if (studentCleanliness == null)
            {
                return NotFound();
            }

            db.StudentCleanlinesses.Remove(studentCleanliness);
            db.SaveChanges();

            return Ok(studentCleanliness);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StudentCleanlinessExists(int id)
        {
            return db.StudentCleanlinesses.Count(e => e.ID == id) > 0;
        }
    }
}