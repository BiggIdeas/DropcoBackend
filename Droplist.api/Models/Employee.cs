using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Droplist.api.Models
{
    public class Employee
	{
        public Employee()
        {
            Droplists = new Collection<Droplist>();
        }

        // Scalar properties
		public int EmployeeId { get; set; }
		public int BuildingId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string EmailAddress { get; set; }
		public string Cellphone { get; set; }
		public int EmployeeNumber { get; set; }
		public string Role { get; set; }
		
        // Navigation properties
		public virtual ICollection<Droplist> Droplists { get; set; }
		public virtual Building Building { get; set; }
		public virtual User User { get; set; }
	}
}