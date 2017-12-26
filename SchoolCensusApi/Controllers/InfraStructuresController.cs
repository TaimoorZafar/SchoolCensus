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
    public class InfraStructuresController : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/InfraStructures
        public IQueryable<InfraStructure> GetInfraStructures()
        {
            return db.InfraStructures;
        }

        // GET: api/InfraStructures/5
        [ResponseType(typeof(InfraStructure))]
        public IHttpActionResult GetInfraStructure(int id)
        {
            InfraStructure infraStructure = db.InfraStructures.Find(id);
            if (infraStructure == null)
            {
                return NotFound();
            }

            return Ok(infraStructure);
        }

        // PUT: api/InfraStructures/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutInfraStructure(int id, InfraStructure infraStructure)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != infraStructure.InfraStructures_id)
            {
                return BadRequest();
            }

            db.Entry(infraStructure).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InfraStructureExists(id))
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

        // POST: api/InfraStructures
        [ResponseType(typeof(InfraStructure))]
        public IHttpActionResult PostInfraStructure([FromBody] string infraStructure)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            InfraStructureModel sm = JsonConvert.DeserializeObject<InfraStructureModel>(infraStructure);
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

        // DELETE: api/InfraStructures/5
        [ResponseType(typeof(InfraStructure))]
        public IHttpActionResult DeleteInfraStructure(int id)
        {
            InfraStructure infraStructure = db.InfraStructures.Find(id);
            if (infraStructure == null)
            {
                return NotFound();
            }

            db.InfraStructures.Remove(infraStructure);
            db.SaveChanges();

            return Ok(infraStructure);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InfraStructureExists(int id)
        {
            return db.InfraStructures.Count(e => e.InfraStructures_id == id) > 0;
        }
    }
}