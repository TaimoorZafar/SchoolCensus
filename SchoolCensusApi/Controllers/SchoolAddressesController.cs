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
    public class SchoolAddressesController : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/SchoolAddresses
        public IQueryable<SchoolAddress> GetSchoolAddresses()
        {
            return db.SchoolAddresses;
        }

        // GET: api/SchoolAddresses/5
        [ResponseType(typeof(SchoolAddress))]
        public IHttpActionResult GetSchoolAddress(int id)
        {
            SchoolAddress schoolAddress = db.SchoolAddresses.Find(id);
            if (schoolAddress == null)
            {
                return NotFound();
            }

            return Ok(schoolAddress);
        }

        // PUT: api/SchoolAddresses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSchoolAddress(int id, SchoolAddress schoolAddress)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != schoolAddress.SchoolAddress_id)
            {
                return BadRequest();
            }

            db.Entry(schoolAddress).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SchoolAddressExists(id))
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

        // POST: api/SchoolAddresses
        [ResponseType(typeof(SchoolAddress))]
        public HttpResponseMessage PostSchoolAddress([FromBody] string schoolAddress)
        {
            if (!ModelState.IsValid)
            {
                 return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            SchoolAddressesModel sm = JsonConvert.DeserializeObject<SchoolAddressesModel>(schoolAddress);

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

        // DELETE: api/SchoolAddresses/5
        [ResponseType(typeof(SchoolAddress))]
        public IHttpActionResult DeleteSchoolAddress(int id)
        {
            SchoolAddress schoolAddress = db.SchoolAddresses.Find(id);
            if (schoolAddress == null)
            {
                return NotFound();
            }

            db.SchoolAddresses.Remove(schoolAddress);
            db.SaveChanges();

            return Ok(schoolAddress);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SchoolAddressExists(int id)
        {
            return db.SchoolAddresses.Count(e => e.SchoolAddress_id == id) > 0;
        }
    }
}