using Droplist.api.Data;
using Droplist.api.Models;
using Droplist.api.Utility;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Http;

namespace Droplist.api.Controllers
{
    public class UsersController : BaseApiController
    {
        private UserManager<User> _userManager;

        public UsersController()
        {
            var db = new DroplistDataContext();
            var store = new UserStore<User>(db);

            _userManager = new UserManager<User>(store);
        }

        // POST: api/register
        [AllowAnonymous]
        [Route("api/register")]
        public IHttpActionResult Register(RegistrationModel registration)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new User
            {
                UserName = registration.EmployeeNumber.ToString()
            };

            var employee = new Employee
            {
                BuildingId = registration.BuildingId,
                FirstName = registration.FirstName,
                LastName = registration.LastName,
                EmployeeNumber = registration.EmployeeNumber,
                Role = registration.Role,
                EmailAddress = registration.EmailAddress,
                Cellphone = registration.Cellphone
            };

            user.Employee = employee;

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

        // GET: api/me
    

        protected override void Dispose(bool disposing)
        {
            _userManager.Dispose();
        }

    }
}
