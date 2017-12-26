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
    public class SchoolStatusController : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/SchoolStatus
        public IQueryable<SchoolStatu> GetSchoolStatus()
        {
            return db.SchoolStatus;
        }

        // GET: api/SchoolStatus/5
        [ResponseType(typeof(SchoolStatu))]
        public IHttpActionResult GetSchoolStatu(string id)
        {
            SchoolStatu schoolStatu = db.SchoolStatus.Find(id);
            if (schoolStatu == null)
            {
                return NotFound();
            }

            return Ok(schoolStatu);
        }

        // PUT: api/SchoolStatus/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSchoolStatu(string id, SchoolStatu schoolStatu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != schoolStatu.SchoolStatus)
            {
                return BadRequest();
            }

            db.Entry(schoolStatu).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SchoolStatuExists(id))
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

        // POST: api/SchoolStatus
        [ResponseType(typeof(SchoolStatu))]
        public IHttpActionResult PostSchoolStatu([FromBody] string schoolStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            SchoolStatusModel sm = JsonConvert.DeserializeObject<SchoolStatusModel>(schoolStatus);
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

        // DELETE: api/SchoolStatus/5
        [ResponseType(typeof(SchoolStatu))]
        public IHttpActionResult DeleteSchoolStatu(string id)
        {
            SchoolStatu schoolStatu = db.SchoolStatus.Find(id);
            if (schoolStatu == null)
            {
                return NotFound();
            }

            db.SchoolStatus.Remove(schoolStatu);
            db.SaveChanges();

            return Ok(schoolStatu);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SchoolStatuExists(string id)
        {
            return db.SchoolStatus.Count(e => e.SchoolStatus == id) > 0;
        }
    }
}