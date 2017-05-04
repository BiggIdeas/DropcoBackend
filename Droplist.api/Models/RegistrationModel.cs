using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Droplist.api.Models
{
	public class RegistrationModel
	{

		[Required]
		public string EmployeeNumber { get; set; }

		[Required, MinLength(8), DataType(DataType.Password)]
		public string Password { get; set; }

		[Required]
		[Compare("Password", ErrorMessage = "Passwords do not match.")]
		public string ConfirmPassword { get; set; }
	}
}