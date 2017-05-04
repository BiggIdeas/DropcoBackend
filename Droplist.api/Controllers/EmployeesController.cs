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
    public class EmployeesController : ApiController
    {
        private DroplistDataContext db = new DroplistDataContext();

		// GET: api/Employees
		public IHttpActionResult GetEmployees()
		{
			var resultSet = db.Employees.Select(employee => new

			{
				employee.EmployeeId,
				employee.BuildingId,
				employee.FirstName,
				employee.LastName,
				employee.EmailAddress,
				employee.Cellphone,
				employee.Address,
				employee.EmployeeNumber,
				employee.Role,

			});

			return Ok(resultSet);
		}

        // GET: api/Employees/5
        [ResponseType(typeof(Employee))]
        public IHttpActionResult GetEmployee(int id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
			var resultSet =  new

			{
				employee.EmployeeId,
				employee.BuildingId,
				employee.FirstName,
				employee.LastName,
				employee.EmailAddress,
				employee.Cellphone,
				employee.Address,
				employee.EmployeeNumber,
				employee.Role

			};
			return Ok(resultSet);
		}

        // PUT: api/Employees/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmployee(int id, Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employee.EmployeeId)
            {
                return BadRequest();
            }
			var dbEmployee = db.Employees.Find(id);

			dbEmployee.EmployeeId = employee.EmployeeId;
			dbEmployee.BuildingId = employee.BuildingId;
			dbEmployee.FirstName = employee.FirstName;
			dbEmployee.LastName = employee.LastName;
			dbEmployee.EmailAddress = employee.EmailAddress;
			dbEmployee.Cellphone = employee.Cellphone;
			dbEmployee.Address = employee.Address;
			dbEmployee.EmployeeNumber = employee.EmployeeNumber;
			dbEmployee.Role = employee.Role;

			db.Entry(dbEmployee).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
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

        // POST: api/Employees
        [ResponseType(typeof(Employee))]
        public IHttpActionResult PostEmployee(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Employees.Add(employee);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = employee.EmployeeId }, new

			{
				employee.EmployeeId,
				employee.BuildingId,
				employee.FirstName,
				employee.LastName,
				employee.EmailAddress,
				employee.Cellphone,
				employee.Address,
				employee.EmployeeNumber,
				employee.Role

			});
        }

        // DELETE: api/Employees/5
        [ResponseType(typeof(Employee))]
        public IHttpActionResult DeleteEmployee(int id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            db.Employees.Remove(employee);
            db.SaveChanges();

            return Ok(employee);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeExists(int id)
        {
            return db.Employees.Count(e => e.EmployeeId == id) > 0;
        }
    }
}