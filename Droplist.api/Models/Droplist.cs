using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Droplist.api.Models
{
	public class Droplist
	{
		// scalar properties
		public int DroplistId { get; set; }
		public int BuildingId { get; set; }
		public int StockerId { get; set; }
		public int DriverId { get; set; }
		public int SectionId { get; set; }
		public string DroplistName { get; set; }
		public DateTime? CreatedOnDate { get; set; }

		// navigation properties

		public virtual Building Building { get; set; }
		public virtual Employee Stocker { get; set; }
		public virtual Employee Driver { get; set; }
		public virtual Section Section { get; set; }
		public virtual ICollection<DroplistItem> DroplistItems { get; set; }
	}
}