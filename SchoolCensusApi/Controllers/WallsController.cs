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
    public class WallsController : ApiController
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: api/Walls
        public IQueryable<Wall> GetWalls()
        {
            return db.Walls;
        }

        // GET: api/Walls/5
        [ResponseType(typeof(Wall))]
        public IHttpActionResult GetWall(int id)
        {
            Wall wall = db.Walls.Find(id);
            if (wall == null)
            {
                return NotFound();
            }

            return Ok(wall);
        }

        // PUT: api/Walls/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutWall(int id, Wall wall)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != wall.Walls_id)
            {
                return BadRequest();
            }

            db.Entry(wall).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WallExists(id))
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

        // POST: api/Walls
        [ResponseType(typeof(Wall))]
        public IHttpActionResult PostWall([FromBody] string wall)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            WallsModel sm = JsonConvert.DeserializeObject<WallsModel>(wall);
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

        // DELETE: api/Walls/5
        [ResponseType(typeof(Wall))]
        public IHttpActionResult DeleteWall(int id)
        {
            Wall wall = db.Walls.Find(id);
            if (wall == null)
            {
                return NotFound();
            }

            db.Walls.Remove(wall);
            db.SaveChanges();

            return Ok(wall);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WallExists(int id)
        {
            return db.Walls.Count(e => e.Walls_id == id) > 0;
        }
    }
}