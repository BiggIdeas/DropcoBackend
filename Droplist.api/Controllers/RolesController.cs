using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Droplist.api.data;
using Droplist.api.Utility;

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
