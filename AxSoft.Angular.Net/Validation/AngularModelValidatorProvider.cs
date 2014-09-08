using System.Collections.Generic;
using System.Web.Mvc;

namespace AxSoft.Angular.Net.Validation
{
	/// <summary>
	/// Provides a list of validators for a model.
	/// </summary>
	public abstract class AngularModelValidatorProvider
	{
		/// <summary>
		/// Gets a list of validators.
		/// </summary>
		/// <param name="metadata">The model metadata.</param>
		/// <param name="context">The AngularJS binding context.</param>
		public abstract IEnumerable<AngularModelValidator> GetValidators(ModelMetadata metadata, AngularBindingContext context);
	}
}