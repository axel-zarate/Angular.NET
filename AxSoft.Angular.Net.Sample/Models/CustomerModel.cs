using AxSoft.Angular.Net.Sample.Models.Validations;
using System;
using System.ComponentModel.DataAnnotations;

namespace AxSoft.Angular.Net.Sample.Models
{
	public class CustomerModel
	{
		[Required]
		[Range(1, 100, ErrorMessage = "{0} must be greater than {1} and should not exceed {2} years.")]
		[Display(Name = "Age")]
		public int Age { get; set; }

		[Required]
		[Display(Name = "Credit Amount")]
		[Double(MinValue = 0, MaxValue = 10000, ErrorMessage = "The {0} field must be a valid decimal number and must not exceed 10,000.")]
		public double CreditAmount { get; set; }

		[EmailAddress]
		[Display(Name = "Email Address")]
		public string EmailAddress { get; set; }

		[Required]
		[Display(Name = "First Name")]
		public string FirstName { get; set; }

		public int Id { get; set; }

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