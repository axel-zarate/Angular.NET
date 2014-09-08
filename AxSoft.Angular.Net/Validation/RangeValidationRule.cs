namespace AxSoft.Angular.Net.Validation
{
	/// <summary>
	/// Provides information to create an AngularJS range validation and error message.
	/// </summary>
	public class RangeValidationRule : ValidationRule
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="RangeValidationRule"/> class.
		/// </summary>
		/// <param name="errorMessage">The error message.</param>
		/// <param name="minValue">The minimum value.</param>
		/// <param name="maxValue">The maximum value.</param>
		public RangeValidationRule(string errorMessage, object minValue, object maxValue)
		{
			ErrorMessage = errorMessage;

			if (minValue != null)
			{
				ValidationParameters["min"] = new AttributeValuePair("min", minValue);
			}
			if (maxValue != null)
			{
				ValidationParameters["max"] = new AttributeValuePair("max", maxValue);
			}
		}
	}
}