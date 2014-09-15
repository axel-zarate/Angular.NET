using System.Collections.Generic;

namespace AxSoft.Angular.Net.Validation
{
	/// <summary>
	/// Provides information to create an AngularJS validation and error message.
	/// </summary>
	public class ValidationRule
	{
		private readonly Dictionary<string, AttributeValuePair> _validationParameters = new Dictionary<string, AttributeValuePair>();

		/// <summary>
		/// Gets or sets the error message to be displayed when the validation fails.
		/// </summary>
		public string ErrorMessage { get; set; }

		/// <summary>
		/// Gets a dictionary of validation parameters. The key corresponds to the normalized AngularJS directive name,
		/// and the value is the corresponding attribute with an optional value.
		/// </summary>
		public IDictionary<string, AttributeValuePair> ValidationParameters
		{
			get { return _validationParameters; }
		}
	}
}