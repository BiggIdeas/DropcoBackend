using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Droplist.api.Models
{
    public class Section
    {
        public Section()
        {
            Droplists = new Collection<Droplist>();
        }

        // Scalar properties
        public int SectionId { get; set; }
        public int DepartmentId { get; set; }
        public string SectionName { get; set; }

        // Navigation properties
        public virtual Department Department { get; set; }
        public virtual ICollection<Droplist> Droplists { get; set; }
    }
}