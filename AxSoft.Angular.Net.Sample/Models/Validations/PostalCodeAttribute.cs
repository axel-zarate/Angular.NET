using System.ComponentModel.DataAnnotations;

namespace AxSoft.Angular.Net.Sample.Models.Validations
{
	public class PostalCodeAttribute : RegularExpressionAttribute
	{
		public PostalCodeAttribute()
			: base(@"^\d{5}$")
		{
			base.ErrorMessage = "{0} must be a 5 digit number.";
		}
	}
}