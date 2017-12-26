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
    public class ElectricitiesController : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/Electricities
        public IQueryable<Electricity> GetElectricities()
        {
            return db.Electricities;
        }

        // GET: api/Electricities/5
        [ResponseType(typeof(Electricity))]
        public IHttpActionResult GetElectricity(int id)
        {
            Electricity electricity = db.Electricities.Find(id);
            if (electricity == null)
            {
                return NotFound();
            }

            return Ok(electricity);
        }

        // PUT: api/Electricities/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutElectricity(int id, Electricity electricity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != electricity.Electricities_id)
            {
                return BadRequest();
            }

            db.Entry(electricity).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ElectricityExists(id))
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

        // POST: api/Electricities
        [ResponseType(typeof(Electricity))]
        public IHttpActionResult PostElectricity([FromBody] string electricity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ElectricityModel sm = JsonConvert.DeserializeObject<ElectricityModel>(electricity);
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

        // DELETE: api/Electricities/5
        [ResponseType(typeof(Electricity))]
        public IHttpActionResult DeleteElectricity(int id)
        {
            Electricity electricity = db.Electricities.Find(id);
            if (electricity == null)
            {
                return NotFound();
            }

            db.Electricities.Remove(electricity);
            db.SaveChanges();

            return Ok(electricity);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ElectricityExists(int id)
        {
            return db.Electricities.Count(e => e.Electricities_id == id) > 0;
        }
    }
}