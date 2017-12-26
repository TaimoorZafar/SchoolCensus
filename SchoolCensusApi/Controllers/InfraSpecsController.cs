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
    public class InfraSpecsController : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/InfraSpecs
        public IQueryable<InfraSpec> GetInfraSpecs()
        {
            return db.InfraSpecs;
        }

        // GET: api/InfraSpecs/5
        [ResponseType(typeof(InfraSpec))]
        public IHttpActionResult GetInfraSpec(int id)
        {
            InfraSpec infraSpec = db.InfraSpecs.Find(id);
            if (infraSpec == null)
            {
                return NotFound();
            }

            return Ok(infraSpec);
        }

        // PUT: api/InfraSpecs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutInfraSpec(int id, InfraSpec infraSpec)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != infraSpec.InfraSpecs_id)
            {
                return BadRequest();
            }

            db.Entry(infraSpec).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InfraSpecExists(id))
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

        // POST: api/InfraSpecs
        [ResponseType(typeof(InfraSpec))]
        public IHttpActionResult PostInfraSpec([FromBody] string infraSpec)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            InfraSpecsModel sm = JsonConvert.DeserializeObject<InfraSpecsModel>(infraSpec);
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

        // DELETE: api/InfraSpecs/5
        [ResponseType(typeof(InfraSpec))]
        public IHttpActionResult DeleteInfraSpec(int id)
        {
            InfraSpec infraSpec = db.InfraSpecs.Find(id);
            if (infraSpec == null)
            {
                return NotFound();
            }

            db.InfraSpecs.Remove(infraSpec);
            db.SaveChanges();

            return Ok(infraSpec);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InfraSpecExists(int id)
        {
            return db.InfraSpecs.Count(e => e.InfraSpecs_id == id) > 0;
        }
    }
}