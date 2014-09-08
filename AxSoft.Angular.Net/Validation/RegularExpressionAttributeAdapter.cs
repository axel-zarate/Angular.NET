using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AxSoft.Angular.Net.Validation
{
	/// <summary>
	/// Provides an adapter for the <see cref="RegularExpressionAttribute"/> attribute.
	/// </summary>
	public class RegularExpressionAttributeAdapter : AngularDataAnnotationsModelValidator<RegularExpressionAttribute>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="RegularExpressionAttributeAdapter"/> class.
		/// </summary>
		/// <param name="metadata">The model metadata.</param>
		/// <param name="context">The AngularJS binding context.</param>
		/// <param name="attribute">The regular expression attribute.</param>
		public RegularExpressionAttributeAdapter(ModelMetadata metadata, AngularBindingContext context, RegularExpressionAttribute attribute)
			: base(metadata, context, attribute)
		{
		}

		/// <summary>
		/// Gets a list of regular-expression client validation rules.
		/// </summary>
		/// <returns>A list of regular-expression client validation rules.</returns>
		public override IEnumerable<ValidationRule> GetClientValidationRules()
		{
			return new[] { new ValidationRegexRule(ErrorMessage, string.Format("/{0}/", Attribute.Pattern)) };
		}
	}
}