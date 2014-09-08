using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AxSoft.Angular.Net.Validation
{
	/// <summary>
	/// Provides an adapter for the <see cref="StringLengthAttribute"/> attribute.
	/// </summary>
	public class StringLengthAttributeAdapter : AngularDataAnnotationsModelValidator<StringLengthAttribute>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="StringLengthAttributeAdapter"/> class.
		/// </summary>
		/// <param name="metadata">The model metadata.</param>
		/// <param name="context">The AngularJS binding context.</param>
		/// <param name="attribute">The string-length attribute.</param>
		public StringLengthAttributeAdapter(ModelMetadata metadata, AngularBindingContext context, StringLengthAttribute attribute)
			: base(metadata, context, attribute)
		{
		}

		/// <summary>
		/// Gets a list of string-length client validation rules.
		/// </summary>
		/// <returns>A list of string-length client validation rules.</returns>
		public override IEnumerable<ValidationRule> GetClientValidationRules()
		{
			return new[] { new StringLengthValidationRule(ErrorMessage, Attribute.MinimumLength, Attribute.MaximumLength) };
		}
	}
}