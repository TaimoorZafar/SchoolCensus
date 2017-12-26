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
    public class GroundSurroundingsController : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/GroundSurroundings
        public IQueryable<GroundSurrounding> GetGroundSurroundings()
        {
            return db.GroundSurroundings;
        }

        // GET: api/GroundSurroundings/5
        [ResponseType(typeof(GroundSurrounding))]
        public IHttpActionResult GetGroundSurrounding(int id)
        {
            GroundSurrounding groundSurrounding = db.GroundSurroundings.Find(id);
            if (groundSurrounding == null)
            {
                return NotFound();
            }

            return Ok(groundSurrounding);
        }

        // PUT: api/GroundSurroundings/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGroundSurrounding(int id, GroundSurrounding groundSurrounding)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != groundSurrounding.ID)
            {
                return BadRequest();
            }

            db.Entry(groundSurrounding).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroundSurroundingExists(id))
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

        // POST: api/GroundSurroundings
        [ResponseType(typeof(GroundSurrounding))]
        public HttpResponseMessage PostGroundSurrounding([FromBody] string groundSurrounding)
        {
            if (!ModelState.IsValid)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            GroundSurroundingModel sm = JsonConvert.DeserializeObject<GroundSurroundingModel>(groundSurrounding);
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
            GroundSurrounding gs = new GroundSurrounding()
            {
                Entity = sm.Groundsurrounding.Entity,
                Survey = sm.Groundsurrounding.Survey,
                Section = sm.Groundsurrounding.Section,
                CreatedOn = DateTime.UtcNow,
                ModifiedOn = DateTime.UtcNow,
            };


            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        // DELETE: api/GroundSurroundings/5
        [ResponseType(typeof(GroundSurrounding))]
        public IHttpActionResult DeleteGroundSurrounding(int id)
        {
            GroundSurrounding groundSurrounding = db.GroundSurroundings.Find(id);
            if (groundSurrounding == null)
            {
                return NotFound();
            }

            db.GroundSurroundings.Remove(groundSurrounding);
            db.SaveChanges();

            return Ok(groundSurrounding);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GroundSurroundingExists(int id)
        {
            return db.GroundSurroundings.Count(e => e.ID == id) > 0;
        }
    }
}