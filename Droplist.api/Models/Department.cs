using System.Collections.Generic;

namespace Droplist.api.Models
{
	public class Department
	{
		public int DepartmentId { get; set; }
		public string DepartmentName { get; set; }

		//navigation properties
		public virtual ICollection<Section> Sections { get; set; }
	}
}