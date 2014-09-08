using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AxSoft.Angular.Net.Validation
{
	/// <summary>
	/// Provides an adapter for the <see cref="RequiredAttribute" /> attribute.
	/// </summary>
	public class RequiredAttributeAdapter : AngularDataAnnotationsModelValidator<RequiredAttribute>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="RequiredAttributeAdapter"/> class.
		/// </summary>
		/// <param name="metadata">The model metadata.</param>
		/// <param name="context">The AngularJS binding context.</param>
		/// <param name="attribute">The required attribute.</param>
		public RequiredAttributeAdapter(ModelMetadata metadata, AngularBindingContext context, RequiredAttribute attribute)
			: base(metadata, context, attribute)
		{
		}

		/// <summary>
		/// Gets a list of required-value client validation rules.
		/// </summary>
		/// <returns>A list of required-value client validation rules.</returns>
		public override IEnumerable<ValidationRule> GetClientValidationRules()
		{
			return new[] { new RequiredValidationRule(ErrorMessage) };
		}
	}
}