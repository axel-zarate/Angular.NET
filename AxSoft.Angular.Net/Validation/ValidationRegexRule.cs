namespace AxSoft.Angular.Net.Validation
{
	/// <summary>
	/// Provides information to create an AngularJS pattern validation and error message.
	/// </summary>
	public class ValidationRegexRule : ValidationRule
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ValidationRegexRule"/> class.
		/// </summary>
		/// <param name="errorMessage">The error message.</param>
		/// <param name="pattern">The regex pattern.</param>
		public ValidationRegexRule(string errorMessage, string pattern)
		{
			ErrorMessage = errorMessage;
			//ValidationType = "pattern";
			ValidationParameters.Add("pattern", new AttributeValuePair("ng-pattern", pattern));
		}
	}
}