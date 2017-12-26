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
    public class ReconciliationsController : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/Reconciliations
        public IQueryable<Reconciliation> GetReconciliations()
        {
            return db.Reconciliations;
        }

        // GET: api/Reconciliations/5
        [ResponseType(typeof(Reconciliation))]
        public IHttpActionResult GetReconciliation(int id)
        {
            Reconciliation reconciliation = db.Reconciliations.Find(id);
            if (reconciliation == null)
            {
                return NotFound();
            }

            return Ok(reconciliation);
        }

        // PUT: api/Reconciliations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutReconciliation(int id, Reconciliation reconciliation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reconciliation.Reconciliations_id)
            {
                return BadRequest();
            }

            db.Entry(reconciliation).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReconciliationExists(id))
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

        // POST: api/Reconciliations
        [ResponseType(typeof(Reconciliation))]
        public IHttpActionResult PostReconciliation([FromBody] string reconciliation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ReconciliationModel sm = JsonConvert.DeserializeObject<ReconciliationModel>(reconciliation);

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

        // DELETE: api/Reconciliations/5
        [ResponseType(typeof(Reconciliation))]
        public IHttpActionResult DeleteReconciliation(int id)
        {
            Reconciliation reconciliation = db.Reconciliations.Find(id);
            if (reconciliation == null)
            {
                return NotFound();
            }

            db.Reconciliations.Remove(reconciliation);
            db.SaveChanges();

            return Ok(reconciliation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReconciliationExists(int id)
        {
            return db.Reconciliations.Count(e => e.Reconciliations_id == id) > 0;
        }
    }
}