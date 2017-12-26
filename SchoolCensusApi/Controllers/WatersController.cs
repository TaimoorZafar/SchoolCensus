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
    public class WatersController : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/Waters
        public IQueryable<Water> GetWaters()
        {
            return db.Waters;
        }

        // GET: api/Waters/5
        [ResponseType(typeof(Water))]
        public IHttpActionResult GetWater(int id)
        {
            Water water = db.Waters.Find(id);
            if (water == null)
            {
                return NotFound();
            }

            return Ok(water);
        }

        // PUT: api/Waters/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutWater(int id, Water water)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != water.Waters_id)
            {
                return BadRequest();
            }

            db.Entry(water).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WaterExists(id))
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

        // POST: api/Waters
        [ResponseType(typeof(Water))]
        public IHttpActionResult PostWater([FromBody] string water)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            WaterModel sm = JsonConvert.DeserializeObject<WaterModel>(water);
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

        // DELETE: api/Waters/5
        [ResponseType(typeof(Water))]
        public IHttpActionResult DeleteWater(int id)
        {
            Water water = db.Waters.Find(id);
            if (water == null)
            {
                return NotFound();
            }

            db.Waters.Remove(water);
            db.SaveChanges();

            return Ok(water);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WaterExists(int id)
        {
            return db.Waters.Count(e => e.Waters_id == id) > 0;
        }
    }
}