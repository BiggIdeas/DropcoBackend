﻿using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Droplist.api.Models
{
	public class Building
	{
		public Building()
		{
			Droplists = new Collection<Droplist>();
			Employees = new Collection<Employee>();
		}
		// Scalar Properties
		public int BuildingId { get; set; }
		public int BuildingNumber { get; set; }
		public string BuildingName { get; set; }
		public string Telephone { get; set; }
		public string StreetAddress { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Zip { get; set; }

		// Navigation properties
		public virtual ICollection<Droplist> Droplists { get; set; }
		public virtual ICollection<Employee> Employees { get; set; }
	}
}