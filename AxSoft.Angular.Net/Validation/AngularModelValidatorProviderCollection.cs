using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.Mvc;

namespace AxSoft.Angular.Net.Validation
{
	/// <summary>
	/// Provides a container for a list of validation providers.
	/// </summary>
	public class AngularModelValidatorProviderCollection : Collection<AngularModelValidatorProvider>
	{
		/// <summary>
		/// Returns the list of model validators.
		/// </summary>
		/// <param name="metadata">The model metadata.</param>
		/// <param name="context">The AngularJS binding context.</param>
		public IEnumerable<AngularModelValidator> GetValidators(ModelMetadata metadata, AngularBindingContext context)
		{
			for (int i = 0; i < Items.Count; i++)
			{
				AngularModelValidatorProvider provider = Items[i];
				foreach (AngularModelValidator validator in provider.GetValidators(metadata, context))
				{
					yield return validator;
				}
			}
		}
	}
}