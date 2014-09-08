namespace AxSoft.Angular.Net.Validation
{
	/// <summary>
	/// Provides information to create an AngularJS equals validation and error message.
	/// </summary>
	public class EqualToValidationRule : ValidationRule
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="EqualToValidationRule"/> class.
		/// </summary>
		/// <param name="errorMessage">The error message.</param>
		/// <param name="other">The other property name.</param>
		public EqualToValidationRule(string errorMessage, string other)
		{
			ErrorMessage = errorMessage;
			ValidationParameters.Add("equalTo", new AttributeValuePair("equal-to", other));
		}
	}
}