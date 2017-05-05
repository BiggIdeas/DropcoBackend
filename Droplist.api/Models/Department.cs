using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Droplist.api.Models
{
	public class Department
	{
        public Department()
        {
            Sections = new Collection<Section>();
        }

        // Scalar properties
        public int DepartmentId { get; set; }
		public string DepartmentName { get; set; }

		// Navigation properties
		public virtual ICollection<Section> Sections { get; set; }
	}
}