using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Droplist.api.data;
using Droplist.api.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Droplist.api.Controllers
{
    public class UsersController : ApiController
    {
		private UserManager<User> _userManager;

		public UsersController()
		{
			var db = new DroplistDataContext();
			var store = new UserStore<User>(db);

			_userManager = new UserManager<User>(store);
		}

		// POST: api/users/register
		[AllowAnonymous]
		[Route("api/users/register")]
		public IHttpActionResult Register(RegistrationModel registration)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var user = new User
			{
				UserName = registration.EmployeeNumber
			};

			var result = _userManager.Create(user, registration.Password);

			if (result.Succeeded)
			{
				return Ok();
			}
			else
			{
				return BadRequest("Invalid user registration");
			}
		}

		protected override void Dispose(bool disposing)
		{
			_userManager.Dispose();
		}

	}
}
