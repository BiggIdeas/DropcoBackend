using System;

namespace Droplist.api.Models
{
    public class DroplistItem
	{
        // Scalar properties
		public int DroplistItemId { get; set; }
		public int ProductId { get; set; }
		public int DroplistId { get; set; }
		public int AisleNumber { get; set; }
		public string AisleRow { get; set; }
		public int AisleColumn { get; set; }
		public DateTime? Completed { get; set; }
		public DateTime? Rejected { get; set; }
		public int? Quantity { get; set; }

        // Navigation properties
		public virtual Droplist Droplist { get; set; }
		public virtual Product Product { get; set; }
	}
}