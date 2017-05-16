using Droplist.api.Data;
using Droplist.api.Utility;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace Droplist.api.Controllers
{
    public class ChartsController : BaseApiController
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
            var droplistItems = db.DroplistItems
                .Where(x=> x.Droplist.BuildingId == CurrentUser.Employee.BuildingId);

            var completedDroplistItems = droplistItems.Where(x => x.Completed != null);
            var pendingDroplistItems = droplistItems.Where(x => x.Completed == null && x.Rejected == null);
            var rejectedDroplistItems = droplistItems.Where(x => x.Rejected != null);

            var resultSet = new
            {
                data = new int[] { completedDroplistItems.Count(), pendingDroplistItems.Count(), rejectedDroplistItems.Count() },
                labels = new string[] { "Completed", "Pending", "Rejected" }
            };
            return Ok(resultSet);
        }

        //GET: api/DroplistItems
        [Route("api/getHardlinesDroplistItems")]
        //[ResponseType(typeof)]
        //[FromUri] string departmentName
        public IHttpActionResult GetHardlinesDroplistItems()
        {
            var departmentId = db.Departments.First(x=> x.DepartmentName == "Hardlines").DepartmentId;
            var droplistItems = db.DroplistItems.Where(x=> x.Droplist.Section.DepartmentId == departmentId);

            var completedDroplistItems = droplistItems.Where(x => x.Completed != null);
            var pendingDroplistItems = droplistItems.Where(x => x.Completed == null && x.Rejected == null);
            var rejectedDroplistItems = droplistItems.Where(x => x.Rejected != null);

            var resultSet = new
            {
                data = new int[] { completedDroplistItems.Count(), pendingDroplistItems.Count(), rejectedDroplistItems.Count() },
                labels = new string[] { "Completed", "Pending", "Rejected" }
            };
            return Ok(resultSet);
        }

        [Route("api/getCenterDroplistItems")]
        public IHttpActionResult GetCenterDroplistItems()
        {
            var departmentId = db.Departments.First(x => x.DepartmentName == "Center").DepartmentId;
            var droplistItems = db.DroplistItems.Where(x => x.Droplist.Section.DepartmentId == departmentId);

            var completedDroplistItems = droplistItems.Where(x => x.Completed != null);
            var pendingDroplistItems = droplistItems.Where(x => x.Completed == null && x.Rejected == null);
            var rejectedDroplistItems = droplistItems.Where(x => x.Rejected != null);

            var resultSet = new
            {
                data = new int[] { completedDroplistItems.Count(), pendingDroplistItems.Count(), rejectedDroplistItems.Count() },
                labels = new string[] { "Completed", "Pending", "Rejected" }
            };
            return Ok(resultSet);
        }

        [Route("api/getFoodsDroplistItems")]
        public IHttpActionResult GetFoodsDroplistItems()
        {
            var departmentId = db.Departments.First(x => x.DepartmentName == "Foods").DepartmentId;
            var droplistItems = db.DroplistItems.Where(x => x.Droplist.Section.DepartmentId == departmentId);

            var completedDroplistItems = droplistItems.Where(x => x.Completed != null);
            var pendingDroplistItems = droplistItems.Where(x => x.Completed == null && x.Rejected == null);
            var rejectedDroplistItems = droplistItems.Where(x => x.Rejected != null);

            var resultSet = new
            {
                data = new int[] { completedDroplistItems.Count(), pendingDroplistItems.Count(), rejectedDroplistItems.Count() },
                labels = new string[] { "Completed", "Pending", "Rejected" }
            };
            return Ok(resultSet);
        }

    }
}
