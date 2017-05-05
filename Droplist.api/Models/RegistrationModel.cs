using System.ComponentModel.DataAnnotations;

namespace Droplist.api.Models
{
    public class RegistrationModel
	{
		[Required]
		public int EmployeeNumber { get; set; }

		[Required, MinLength(8), DataType(DataType.Password)]
		public string Password { get; set; }

		[Required]
		[Compare("Password", ErrorMessage = "Passwords do not match.")]
		public string ConfirmPassword { get; set; }

        [Required]
        public int BuildingId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        
		[Required]
        public string Role { get; set; }

        public string EmailAddress { get; set; }

        public string Cellphone { get; set; }
    }
}