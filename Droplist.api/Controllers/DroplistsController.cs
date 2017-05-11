using Droplist.api.Data;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Droplist.api.Models;
using Droplist.api.Utility;

namespace Droplist.api.Controllers
{
    public class DroplistsController : BaseApiController
    {
        //private DroplistDataContext db = new DroplistDataContext();

        // GET: api/Droplists
        public IHttpActionResult GetDroplists()
        {
            if (CurrentUser == null) {
                return Unauthorized();
            }

            var resultSet = db.Droplists
                .Where(d=> d.BuildingId == CurrentUser.Employee.BuildingId) //Match userId of logged user
                .Select(droplist => new
                {
                    droplist.DroplistId,
                    droplist.BuildingId,
                    droplist.DroplistName,
                    droplist.CreatedOnDate,
                    droplist.StockerId,
                    droplist.DriverId,
                    droplist.SectionId,
                    DriverName = droplist.Driver.FirstName + " " + droplist.Driver.LastName,
                    StockerName = droplist.Stocker.FirstName,
                    DepartmentName = droplist.Section.Department.DepartmentName,
                    SectionName = droplist.Section.SectionName
                });

            return Ok(resultSet);
        }

        // GET: api/Droplists/5
        [ResponseType(typeof(Models.Droplist))]
        public IHttpActionResult GetDroplist(int id)
        {
            if (CurrentUser == null)
            {
                return Unauthorized();
            }

            Models.Droplist droplist = db.Droplists.Find(id);
            if (droplist == null)
            {
                return NotFound();
            }

            var droplistItems = db.DroplistItems
                .Where(d => d.DroplistId == id)
                .Select(d => new
                {
                    product = new
                    {
                        productId = d.Product.ProductId,
                        itemNumber = d.Product.ItemNumber,
                        description = d.Product.Description
                    },
                    droplistId = d.DroplistId,
                    aisleNumber = d.AisleNumber,
                    aisleRow = d.AisleRow,
                    aisleColumn = d.AisleColumn,
                    completed = d.Completed,
                    rejected = d.Rejected,
                    quantity = d.Quantity,
                    droplistItemId = d.DroplistItemId
                })

                .OrderBy(d => d.aisleNumber)
                .ThenBy(d => d.aisleColumn)
                .ThenBy(d => d.aisleRow);


            var resultSet = new
            {
                droplist.DroplistId,
                droplist.BuildingId,
                droplist.DroplistName,
                droplist.CreatedOnDate,
                droplist.StockerId,
                droplist.DriverId,
                droplist.SectionId,
                DriverName = (droplist.Driver != null) ? droplist.Driver.FirstName + " " + droplist.Driver.LastName : null,
                //StockerName = droplist.Stocker.FirstName + " " + droplist.Stocker.LastName,
                DepartmentName = droplist.Section.Department.DepartmentName,
                SectionName = droplist.Section.SectionName,
                DroplistItems = droplistItems
            };
            return Ok(resultSet);
        }

        // PUT: api/Droplists/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDroplist(int id, Models.Droplist droplist)
        {
            if (CurrentUser == null)
            {
                return Unauthorized();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != droplist.DroplistId)
            {
                return BadRequest();
            }

            var dbDroplist = db.Droplists.Find(id);
            dbDroplist.DroplistId = droplist.DroplistId;
            dbDroplist.BuildingId = droplist.BuildingId;
            dbDroplist.StockerId = droplist.StockerId;
            dbDroplist.DriverId = droplist.DriverId;
            dbDroplist.DroplistName = droplist.DroplistName;
            dbDroplist.SectionId = droplist.SectionId;
            dbDroplist.CreatedOnDate = droplist.CreatedOnDate;

            db.Entry(dbDroplist).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DroplistExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            foreach (var droplistItem in droplist.DroplistItems)
            {
                if (droplistItem.DroplistItemId == 0)
                {
                    db.DroplistItems.Add(droplistItem);
                }
                else
                {
                    var dbDroplistItem = db.DroplistItems.Find(droplistItem.DroplistItemId);

                    // save the properties you want to set
                    // aisle
                    dbDroplistItem.AisleNumber = droplistItem.AisleNumber;
                    // row
                    dbDroplistItem.AisleRow = droplistItem.AisleRow;
                    // column
                    dbDroplistItem.AisleColumn = droplistItem.AisleColumn;
                    // productid 
                    dbDroplistItem.ProductId = droplistItem.Product.ProductId;
                    // quantity
                    dbDroplistItem.Quantity = droplistItem.Quantity;

                    db.Entry(dbDroplistItem).State = EntityState.Modified;
                }

                db.SaveChanges();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Droplists
        [ResponseType(typeof(Models.Droplist))]
        public IHttpActionResult PostDroplist(Models.Droplist droplist)
        {
            if (CurrentUser == null)
            {
                return Unauthorized();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbDroplist = new Models.Droplist
            {
                BuildingId = droplist.BuildingId,
                CreatedOnDate = droplist.CreatedOnDate,
                DriverId = droplist.DriverId,
                DroplistId = droplist.DroplistId,
                DroplistName = droplist.DroplistName,
                SectionId = droplist.SectionId,
                StockerId = droplist.StockerId
            };

            foreach (var di in droplist.DroplistItems)
            {
                if (di.ProductId != 0)
                {
                    var dbdi = new Models.DroplistItem
                    {
                        AisleColumn = di.AisleColumn,
                        AisleNumber = di.AisleNumber,
                        AisleRow = di.AisleRow,
                        Completed = di.Completed,
                        DroplistId = di.DroplistId,
                        DroplistItemId = di.DroplistItemId,
                        ProductId = di.ProductId,
                        Quantity = di.Quantity,
                        Rejected = di.Rejected
                    };

                    dbDroplist.DroplistItems.Add(dbdi);
                }
                else
                {
                    var product = new Product
                    {
                        ItemNumber = di.Product.ItemNumber,
                        Description = di.Product.Description,
                        Price = 0

                    };

                    db.Products.Add(product);
                    db.SaveChanges();

                    var dbdi = new Models.DroplistItem
                    {
                        AisleColumn = di.AisleColumn,
                        AisleNumber = di.AisleNumber,
                        AisleRow = di.AisleRow,
                        Completed = di.Completed,
                        DroplistId = di.DroplistId,
                        DroplistItemId = di.DroplistItemId,
                        ProductId = product.ProductId,
                        Quantity = di.Quantity,
                        Rejected = di.Rejected
                    };

                    dbDroplist.DroplistItems.Add(dbdi);
                }
            }

            db.Droplists.Add(dbDroplist);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = dbDroplist.DroplistId }, new
            {
                dbDroplist.DroplistId,
                droplist.BuildingId,
                droplist.DroplistName,
                droplist.CreatedOnDate,
                droplist.StockerId,
                droplist.DriverId,
                droplist.SectionId,
            });
        }

        // DELETE: api/Droplists/5
        [ResponseType(typeof(Models.Droplist))]
        public IHttpActionResult DeleteDroplist(int id)
        {
            if (CurrentUser == null)
            {
                return Unauthorized();
            }

            Models.Droplist droplist = db.Droplists.Find(id);
            if (droplist == null)
            {
                return NotFound();
            }
            var droplistItems = db.DroplistItems.Where(x => x.DroplistId == id).ToList();
            for (int i = 0; i < droplistItems.Count; i++)
            {
                db.DroplistItems.Remove(droplistItems[i]);
            }

            db.Droplists.Remove(droplist);
            db.SaveChanges();

            return Ok(droplist);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DroplistExists(int id)
        {
            return db.Droplists.Count(e => e.DroplistId == id) > 0;
        }
    }
}