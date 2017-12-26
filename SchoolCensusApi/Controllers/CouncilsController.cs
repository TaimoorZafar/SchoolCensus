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
    public class CouncilsController : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/Councils
        public IQueryable<Council> GetCouncils()
        {
            return db.Councils;
        }

        // GET: api/Councils/5
        [ResponseType(typeof(Council))]
        public IHttpActionResult GetCouncil(int id)
        {
            Council council = db.Councils.Find(id);
            if (council == null)
            {
                return NotFound();
            }

            return Ok(council);
        }

        // PUT: api/Councils/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCouncil(int id, Council council)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != council.Councils_id)
            {
                return BadRequest();
            }

            db.Entry(council).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CouncilExists(id))
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

        // POST: api/Councils
        [ResponseType(typeof(Council))]
        public IHttpActionResult PostCouncil([FromBody] string council)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ConcileModel sm = JsonConvert.DeserializeObject<ConcileModel>(council);
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

        // DELETE: api/Councils/5
        [ResponseType(typeof(Council))]
        public IHttpActionResult DeleteCouncil(int id)
        {
            Council council = db.Councils.Find(id);
            if (council == null)
            {
                return NotFound();
            }

            db.Councils.Remove(council);
            db.SaveChanges();

            return Ok(council);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CouncilExists(int id)
        {
            return db.Councils.Count(e => e.Councils_id == id) > 0;
        }
    }
}