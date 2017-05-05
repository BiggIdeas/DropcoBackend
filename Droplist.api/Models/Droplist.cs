using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Droplist.api.Models
{
    public class Droplist
	{
		public Droplist()
		{
			DroplistItems = new Collection<DroplistItem>();
		}

		// Scalar properties
		public int DroplistId { get; set; }
		public int BuildingId { get; set; }
		public int StockerId { get; set; }
		public int? DriverId { get; set; }
		public int SectionId { get; set; }
		public string DroplistName { get; set; }
		public DateTime? CreatedOnDate { get; set; }

		// Navigation properties
		public virtual Building Building { get; set; }
		public virtual Employee Stocker { get; set; }
		public virtual Employee Driver { get; set; }
		public virtual Section Section { get; set; }
		public virtual ICollection<DroplistItem> DroplistItems { get; set; }
	}
}