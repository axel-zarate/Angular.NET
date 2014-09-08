namespace AxSoft.Angular.Net.Validation
{
	/// <summary>
	/// Provides a container for the current validation provider.
	/// </summary>
	public class AngularModelValidatorProviders
	{
		private static readonly AngularModelValidatorProviderCollection _providers = new AngularModelValidatorProviderCollection
		{
			new AngularDataAnnotationsModelValidatorProvider()
			//new NgDirectiveAttributeAdapterProvider()
		};

		/// <summary>
		/// Gets the model validator provider collection.
		/// </summary>
		public static AngularModelValidatorProviderCollection Providers
		{
			get
			{
				return _providers;
			}
		}
	}
}