using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Droplist.api.Models
{
    public class Product
	{
        public Product()
        {
            DroplistItems = new Collection<DroplistItem>();
        }

        // Scalar properties
		public int ProductId { get; set; }
		public int ItemNumber { get; set; }
		public int BuildingId { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }

        // Navigation properties
		public virtual ICollection<DroplistItem> DroplistItems { get; set; }
	}
}