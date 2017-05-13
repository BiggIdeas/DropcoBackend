using Droplist.api.Data;
using Droplist.api.Models;
using Droplist.api.Utility;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Droplist.api.Controllers
{
    public class DroplistItemController : BaseApiController
    {
        //private DroplistDataContext db = new DroplistDataContext();

        // PUT: api/DroplistItem/5
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
            //dbDroplistItem.ProductId = droplistItem.ProductId;
            //dbDroplistItem.DroplistId = droplistItem.DroplistId;
            //dbDroplistItem.AisleNumber = droplistItem.AisleNumber;
            //dbDroplistItem.AisleRow = droplistItem.AisleRow;
            //dbDroplistItem.AisleColumn = droplistItem.AisleColumn;
            dbDroplistItem.Completed = droplistItem.Completed;
            dbDroplistItem.Rejected = droplistItem.Rejected;
            //dbDroplistItem.Quantity = droplistItem.Quantity;

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

            var droplistId = droplistItem.DroplistId;
            CheckIfDriverIsSaved(droplistId);
            CehckIfComplete(droplistId);

            return StatusCode(HttpStatusCode.NoContent);
        }

        private void CheckIfDriverIsSaved(int droplistId)
        {
            var dbDroplist = db.Droplists.Find(droplistId);
            if (dbDroplist.Driver != null) return;
            dbDroplist.DriverId = CurrentUser.Employee.EmployeeId;
            db.Entry(dbDroplist).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CehckIfComplete(int droplistId)
        {
            var dbDroplist = db.Droplists.Find(droplistId);
            if (dbDroplist.DroplistItems.All(x => x.Completed != null || x.Rejected != null))
            {
                dbDroplist.CompletedOnDate = DateTime.Now;
                db.Entry(dbDroplist).State = EntityState.Modified;
            }
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
