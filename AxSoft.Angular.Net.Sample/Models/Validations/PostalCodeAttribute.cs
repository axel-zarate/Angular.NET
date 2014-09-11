using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AxSoft.Angular.Net.Sample.Models.Validations
{
	public class PostalCodeAttribute : RegularExpressionAttribute
	{
		public PostalCodeAttribute() : base(@"^\d{5}$")
		{
			base.ErrorMessage = "{0} must be a 5 digit number.";
		}
	}
}