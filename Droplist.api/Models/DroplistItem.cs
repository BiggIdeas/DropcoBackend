using System;

namespace Droplist.api.Models
{
	public class DroplistItem
	{
		public int DroplistItemId { get; set; }
		public int ProductId { get; set; }
		public int DroplistId { get; set; }
		public string AisleNumber { get; set; }
		public string AisleRow { get; set; }
		public int AisleColumn { get; set; }
		public DateTime? Completed { get; set; }
		public DateTime? Rejected { get; set; }

		public virtual Droplist Droplist { get; set; }
		public virtual Product Product { get; set; }
	}

}