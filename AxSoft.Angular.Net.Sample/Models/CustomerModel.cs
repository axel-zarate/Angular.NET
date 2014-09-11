using AxSoft.Angular.Net.Sample.Models.Validations;
using System.ComponentModel.DataAnnotations;

namespace AxSoft.Angular.Net.Sample.Models
{
	public class CustomerModel
	{
		public int Id { get; set; }

		[Required]
		[Range(1, 100, ErrorMessage = "{0} must be greater than {1} and should not exceed {2} years.")]
		[Display(Name = "Age")]
		public int Age { get; set; }

		[EmailAddress]
		[Display(Name = "Email Address")]
		public string EmailAddress { get; set; }

		[Required]
		[Display(Name = "First Name")]
		public string FirstName { get; set; }

		[Required]
		[Display(Name = "Last Name")]
		public string LastName { get; set; }

		[PostalCode]
		[Display(Name = "Postal Code")]
		public string PostalCode { get; set; }

		[Display(Name = "Street Address")]
		public string StreetAddress { get; set; }
	}
}