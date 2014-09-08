using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web.Mvc;

namespace AxSoft.Angular.Net.Validation
{
	/// <summary>
	/// Provides a model validator.
	/// </summary>
	public class AngularDataAnnotationsModelValidator : AngularModelValidator
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AngularDataAnnotationsModelValidator"/> class.
		/// </summary>
		/// <param name="metadata">The model metadata.</param>
		/// <param name="context">The AngularJS binding context.</param>
		/// <param name="attribute">The validation attribute for the model.</param>
		/// <exception cref="System.ArgumentNullException">attribute is <code>null</code></exception>
		public AngularDataAnnotationsModelValidator(ModelMetadata metadata, AngularBindingContext context, ValidationAttribute attribute)
			: base(metadata, context)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException("attribute");
			}

			Attribute = attribute;
		}

		/// <summary>
		/// Gets the validation attribute for the model validator.
		/// </summary>
		protected internal ValidationAttribute Attribute { get; private set; }

		/// <summary>
		/// Gets the error message for the validation failure
		/// </summary>
		protected internal string ErrorMessage
		{
			get { return Attribute.FormatErrorMessage(Metadata.GetDisplayName()); }
		}

		/// <summary>
		/// Retrieves a collection of client validation rules.
		/// </summary>
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "This method may perform non-trivial work.")]
		public override IEnumerable<ValidationRule> GetClientValidationRules()
		{
			return Enumerable.Empty<ValidationRule>();
		}
	}

	/// <summary>
	/// Provides a model validator for a specified validation type.
	/// </summary>
	/// <typeparam name="TAttribute">The type of the validation attribute.</typeparam>
	public class AngularDataAnnotationsModelValidator<TAttribute> : AngularDataAnnotationsModelValidator where TAttribute : ValidationAttribute
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AngularDataAnnotationsModelValidator{TAttribute}"/> class.
		/// </summary>
		/// <param name="metadata">The model metadata.</param>
		/// <param name="context">The AngularJS binding context.</param>
		/// <param name="attribute">The validation attribute for the model.</param>
		public AngularDataAnnotationsModelValidator(ModelMetadata metadata, AngularBindingContext context, TAttribute attribute)
			: base(metadata, context, attribute)
		{
		}

		/// <summary>
		/// Gets or sets a value that indicates whether a model property is required
		/// </summary>
		/// <value>
		/// <c>true</c> if the model property is required; otherwise, <c>false</c>.
		/// </value>
		public override bool IsRequired
		{
			get { return Attribute is RequiredAttribute; }
		}

		/// <summary>
		/// Gets the validation attribute for the model validator.
		/// </summary>
		protected internal new TAttribute Attribute
		{
			get { return (TAttribute)base.Attribute; }
		}
	}
}