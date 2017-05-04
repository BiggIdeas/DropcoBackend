using System.Collections.Generic;

namespace Droplist.api.Models
{
	public class Section
	{
		public int SectionId { get; set; }
		public int DepartmentId { get; set; }
		public string SectionName { get; set; } 

		public virtual Department Department { get; set; }
		public virtual ICollection<Droplist> Droplists { get; set; }
	}
}