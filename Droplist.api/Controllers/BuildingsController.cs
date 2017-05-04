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
using Droplist.api.Models;
using Droplist.api.data;

namespace Droplist.api.Controllers
{
    public class BuildingsController : ApiController
    {
        private DroplistDataContext db = new DroplistDataContext();

		// GET: api/Buildings
		public IHttpActionResult GetBuildings()
		{
			var resultSet = db.Buildings.Select(building => new

			{
				building.BuildingId,
				building.BuildingName,
				building.BuildingNumber,
				building.Telephone,
				building.StreetAddress,
				building.City,
				building.State,
				building.Zip
			});

			return Ok(resultSet);
		}

        // GET: api/Buildings/5
        [ResponseType(typeof(Building))]
        public IHttpActionResult GetBuilding(int id)
        {
            Building building = db.Buildings.Find(id);
            if (building == null)
            {
                return NotFound();
            }
			var resultSet = new

			{
				building.BuildingId,
				building.BuildingName,
				building.BuildingNumber,
				building.Telephone,
				building.StreetAddress,
				building.City,
				building.State,
				building.Zip
			};

			return Ok(resultSet);
		}

        // PUT: api/Buildings/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBuilding(int id, Building building)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != building.BuildingId)
            {
                return BadRequest();
            }

			var dbBuilding = db.Buildings.Find(id);

			dbBuilding.BuildingId = building.BuildingId;
			dbBuilding.BuildingName = building.BuildingName;
			dbBuilding.BuildingNumber = building.BuildingNumber;
			dbBuilding.Telephone = building.Telephone;
			dbBuilding.StreetAddress = building.StreetAddress;
			dbBuilding.City = building.City;
			dbBuilding.State = building.State;
			dbBuilding.Zip = building.Zip;


			db.Entry(dbBuilding).State = EntityState.Modified;
			try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BuildingExists(id))
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

        // POST: api/Buildings
        [ResponseType(typeof(Building))]
        public IHttpActionResult PostBuilding(Building building)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Buildings.Add(building);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = building.BuildingId }, new {

				building.BuildingId,
				building.BuildingName,
				building.BuildingNumber,
				building.Telephone,
				building.StreetAddress,
				building.City,
				building.State,
				building.Zip
			});
        }

        // DELETE: api/Buildings/5
        [ResponseType(typeof(Building))]
        public IHttpActionResult DeleteBuilding(int id)
        {
            Building building = db.Buildings.Find(id);
            if (building == null)
            {
                return NotFound();
            }

            db.Buildings.Remove(building);
            db.SaveChanges();

            return Ok(building);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BuildingExists(int id)
        {
            return db.Buildings.Count(e => e.BuildingId == id) > 0;
        }
    }
}