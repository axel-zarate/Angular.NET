using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AxSoft.Angular.Net.Validation
{
	/// <summary>
	/// Provides an adapter for the <see cref="System.ComponentModel.DataAnnotations.RangeAttribute"/> attribute.
	/// </summary>
	public class RangeAttributeAdapter : AngularDataAnnotationsModelValidator<RangeAttribute>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="RangeAttributeAdapter"/> class.
		/// </summary>
		/// <param name="metadata">The model metadata.</param>
		/// <param name="context">The AngularJS binding context.</param>
		/// <param name="attribute">The range attribute.</param>
		public RangeAttributeAdapter(ModelMetadata metadata, AngularBindingContext context, RangeAttribute attribute)
			: base(metadata, context, attribute)
		{
		}

		/// <summary>
		/// Gets a list of client validation rules for a range check.
		/// </summary>
		public override IEnumerable<ValidationRule> GetClientValidationRules()
		{
			string errorMessage = ErrorMessage; // Per Dev10 Bug #923283, need to make sure ErrorMessage is called before Minimum/Maximum
			return new[] { new RangeValidationRule(errorMessage, Attribute.Minimum, Attribute.Maximum) };
		}
	}
}