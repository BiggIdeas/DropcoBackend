using Droplist.api.Data;
using System;
using System.Linq;
using System.Web.Http;

namespace Droplist.api.Controllers
{
    public class ChartsController : ApiController
    {
        private DroplistDataContext db = new DroplistDataContext();

        // GET: api/Charts
        [Route("api/getTodaysDroplists")]
        public IHttpActionResult GetTodaysDroplists()
        {
            var today = DateTime.Now;
            var todaysDroplists = db.Droplists
                .Where(x => x.CreatedOnDate.Value.Day == today.Day)
                .Select(droplist => new
                {
                    droplist.DroplistName,
                    droplist.CompletedOnDate
                });

            var completedDroplists = todaysDroplists
                .Where(x => x.CompletedOnDate != null);

            var resultSet = new
            {
                todaysDroplist = todaysDroplists,
                completedDroplists = completedDroplists
            };
            return Ok(resultSet);
        }

        //GET: api/DroplistItems
        [Route("api/getDroplistItems")]
        public IHttpActionResult GetDroplistItems()
        {
            var today = DateTime.Now;
            var droplistItems = db.DroplistItems;

            var completedDroplistItems = droplistItems.Where(x => x.Completed != null);
            var rejectedDroplistItems = droplistItems.Where(x => x.Rejected != null);
            var pendingDroplistItems = droplistItems.Where(x => x.Completed == null && x.Rejected == null);
            //Count() - completedDroplistItems.Count - rejectedDroplistItems.Count;

            //new int[] { 99, 98, 92, 97, 95 }
            var resultSet = new
            {
                data = new int[] { completedDroplistItems.Count(), rejectedDroplistItems.Count(), pendingDroplistItems.Count() },
                labels = new string[] { "Completed", "Rejected", "Pending" }
            };
            return Ok(resultSet);
        }

    }
}
