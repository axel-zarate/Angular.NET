using System;

namespace AxSoft.Angular.Net.Validation
{
	/// <summary>
	/// Provides information to create an AngularJS string length validation and error message.
	/// </summary>
	public class StringLengthValidationRule : ValidationRule
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="StringLengthValidationRule"/> class.
		/// </summary>
		/// <param name="errorMessage">The error message.</param>
		/// <param name="minimumLength">The minimum length.</param>
		/// <param name="maximumLength">The maximum length.</param>
		public StringLengthValidationRule(string errorMessage, int minimumLength, int maximumLength)
		{
			ErrorMessage = errorMessage;

			if (minimumLength != 0)
			{
				ValidationParameters["minlength"] = new AttributeValuePair("ng-minlength", minimumLength);
			}

			if (maximumLength != Int32.MaxValue)
			{
				ValidationParameters["maxlength"] = new AttributeValuePair("ng-maxlength", maximumLength);
			}
		}
	}
}