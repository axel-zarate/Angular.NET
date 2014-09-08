namespace AxSoft.Angular.Net.Validation
{
	/// <summary>
	/// Provides information to create an AngularJS required validation and error message.
	/// </summary>
	public class RequiredValidationRule : ValidationRule
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="RequiredValidationRule"/> class.
		/// </summary>
		/// <param name="errorMessage">The error message.</param>
		public RequiredValidationRule(string errorMessage)
		{
			ErrorMessage = errorMessage;
			//ValidationType = "required";
			ValidationParameters.Add("required", new AttributeValuePair("required", string.Empty));
		}
	}
}