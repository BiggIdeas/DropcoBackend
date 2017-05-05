using Droplist.api.Data;
using Droplist.api.Utility;
using System;
using System.Linq;
using System.Web.Http;

namespace Droplist.api.Controllers
{
    public class RolesController : ApiController
    {
        private DroplistDataContext db = new DroplistDataContext();

        // GET: api/Roles
        [Route("api/roles")]
        public IHttpActionResult GetRoles()
        {
            var resultSet =
                Enum.GetValues(typeof(Role))
                    .Cast<Role>()
                    .Select(v => v.ToString())
                    .ToList();

            return Ok(resultSet);
        }
    }
}
