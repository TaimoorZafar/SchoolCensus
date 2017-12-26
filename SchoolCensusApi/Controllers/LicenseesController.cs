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
    public class LicenseesController : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/Licensees
        public IQueryable<Licensee> GetLicensees()
        {
            return db.Licensees;
        }

        // GET: api/Licensees/5
        [ResponseType(typeof(Licensee))]
        public IHttpActionResult GetLicensee(int id)
        {
            Licensee licensee = db.Licensees.Find(id);
            if (licensee == null)
            {
                return NotFound();
            }

            return Ok(licensee);
        }

        // PUT: api/Licensees/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLicensee(int id, Licensee licensee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != licensee.Licensees_id)
            {
                return BadRequest();
            }

            db.Entry(licensee).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LicenseeExists(id))
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

        // POST: api/Licensees
        [ResponseType(typeof(Licensee))]
        public IHttpActionResult PostLicensee([FromBody] string licensee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            LicenceeModel sm = JsonConvert.DeserializeObject<LicenceeModel>(licensee);

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

        // DELETE: api/Licensees/5
        [ResponseType(typeof(Licensee))]
        public IHttpActionResult DeleteLicensee(int id)
        {
            Licensee licensee = db.Licensees.Find(id);
            if (licensee == null)
            {
                return NotFound();
            }

            db.Licensees.Remove(licensee);
            db.SaveChanges();

            return Ok(licensee);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LicenseeExists(int id)
        {
            return db.Licensees.Count(e => e.Licensees_id == id) > 0;
        }
    }
}