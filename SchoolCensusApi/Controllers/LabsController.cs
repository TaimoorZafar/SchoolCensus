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
    public class LabsController : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/Labs
        public IQueryable<Lab> GetLabs()
        {
            return db.Labs;
        }

        // GET: api/Labs/5
        [ResponseType(typeof(Lab))]
        public IHttpActionResult GetLab(int id)
        {
            Lab lab = db.Labs.Find(id);
            if (lab == null)
            {
                return NotFound();
            }

            return Ok(lab);
        }

        // PUT: api/Labs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLab(int id, Lab lab)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != lab.Labs_id)
            {
                return BadRequest();
            }

            db.Entry(lab).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LabExists(id))
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

        // POST: api/Labs
        [ResponseType(typeof(Lab))]
        public IHttpActionResult PostLab([FromBody] string lab)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            LabsModel sm = JsonConvert.DeserializeObject<LabsModel>(lab);
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

        // DELETE: api/Labs/5
        [ResponseType(typeof(Lab))]
        public IHttpActionResult DeleteLab(int id)
        {
            Lab lab = db.Labs.Find(id);
            if (lab == null)
            {
                return NotFound();
            }

            db.Labs.Remove(lab);
            db.SaveChanges();

            return Ok(lab);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LabExists(int id)
        {
            return db.Labs.Count(e => e.Labs_id == id) > 0;
        }
    }
}