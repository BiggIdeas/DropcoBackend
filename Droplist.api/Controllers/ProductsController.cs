using Droplist.api.Data;
using Droplist.api.Models;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace Droplist.api.Controllers
{
    public class ProductsController : ApiController
    {
        private DroplistDataContext db = new DroplistDataContext();

		// GET: api/Products
		public IHttpActionResult GetProducts()
		{
			var resultSet = db.Products.Select(product => new
			{
				product.ProductId,
				product.ItemNumber,
				product.Description,
                product.Price
			});

			return Ok(resultSet);
		}

        // GET: api/Products/5--
        [ResponseType(typeof(Product))]
        public IHttpActionResult GetProduct(int id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

			var resultSet = new
			{
				product.ProductId,
				product.ItemNumber,
				product.Description
			};

			return Ok(resultSet);
        }

        // PUT: api/Products/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProduct(int id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.ProductId)
            {
                return BadRequest();
            }

			var dbProduct = db.Products.Find(id);

			dbProduct.ProductId = product.ProductId;
			dbProduct.ItemNumber = product.ItemNumber;
			dbProduct.Description = product.Description;

			db.Entry(dbProduct).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        // POST: api/Products
        [ResponseType(typeof(Product))]
        [Authorize(Roles = "Manager")]       
        public IHttpActionResult PostProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Products.Add(product);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = product.ProductId }, new
			{
				product.ProductId,
				product.ItemNumber,
				product.Description
			});
        }

        // DELETE: api/Products/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult DeleteProduct(int id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            db.Products.Remove(product);
            db.SaveChanges();

            return Ok(product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int id)
        {
            return db.Products.Count(e => e.ProductId == id) > 0;
        }
    }
}