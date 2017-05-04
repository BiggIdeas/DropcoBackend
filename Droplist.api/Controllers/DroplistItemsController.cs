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
    public class DroplistItemsController : ApiController
    {
        private DroplistDataContext db = new DroplistDataContext();

		// GET: api/DroplistItems
		public IHttpActionResult GetDroplistItems()
		{
			var resultSet = db.DroplistItems.Select(droplistItem => new

			{
				droplistItem.DroplistItemId,
				droplistItem.ProductId,
				droplistItem.DroplistId,
				droplistItem.AisleNumber,
				droplistItem.AisleRow,
				droplistItem.AisleColumn,
				droplistItem.Completed,
				droplistItem.Rejected

			});

			return Ok(resultSet);
		}

        // GET: api/DroplistItems/5
        [ResponseType(typeof(DroplistItem))]
        public IHttpActionResult GetDroplistItem(int id)
        {
            DroplistItem droplistItem = db.DroplistItems.Find(id);
            if (droplistItem == null)
            {
                return NotFound();
            }
			var resultSet = new

			{
				droplistItem.DroplistItemId,
				droplistItem.ProductId,
				droplistItem.DroplistId,
				droplistItem.AisleNumber,
				droplistItem.AisleRow,
				droplistItem.AisleColumn,
				droplistItem.Completed,
				droplistItem.Rejected

			};

			return Ok(resultSet);
		}

        // PUT: api/DroplistItems/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDroplistItem(int id, DroplistItem droplistItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != droplistItem.DroplistItemId)
            {
                return BadRequest();
            }
			var dbDroplistItem = db.DroplistItems.Find(id);
			dbDroplistItem.DroplistItemId = droplistItem.DroplistItemId;
			dbDroplistItem.ProductId = droplistItem.ProductId;
			dbDroplistItem.DroplistId = droplistItem.DroplistId;
			dbDroplistItem.AisleNumber = droplistItem.AisleNumber;
			dbDroplistItem.AisleRow = droplistItem.AisleRow;
			dbDroplistItem.AisleColumn = droplistItem.AisleColumn;
			dbDroplistItem.Completed = droplistItem.Completed;
			dbDroplistItem.Rejected = droplistItem.Rejected;

			db.Entry(dbDroplistItem).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DroplistItemExists(id))
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

        // POST: api/DroplistItems
        [ResponseType(typeof(DroplistItem))]
        public IHttpActionResult PostDroplistItem(DroplistItem droplistItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DroplistItems.Add(droplistItem);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = droplistItem.DroplistItemId }, new

			{
				droplistItem.DroplistItemId,
				droplistItem.ProductId,
				droplistItem.DroplistId,
				droplistItem.AisleNumber,
				droplistItem.AisleRow,
				droplistItem.AisleColumn,
				droplistItem.Completed,
				droplistItem.Rejected

			});
        }

        // DELETE: api/DroplistItems/5
        [ResponseType(typeof(DroplistItem))]
        public IHttpActionResult DeleteDroplistItem(int id)
        {
            DroplistItem droplistItem = db.DroplistItems.Find(id);
            if (droplistItem == null)
            {
                return NotFound();
            }

            db.DroplistItems.Remove(droplistItem);
            db.SaveChanges();

            return Ok(droplistItem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DroplistItemExists(int id)
        {
            return db.DroplistItems.Count(e => e.DroplistItemId == id) > 0;
        }
    }
}