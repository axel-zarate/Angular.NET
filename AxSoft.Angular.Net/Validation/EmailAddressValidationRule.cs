namespace SS.Web.Angular.Validation
{
	public class EmailAddressValidationRule : ValidationRule
	{
		public EmailAddressValidationRule(string errorMessage)
		{
			ErrorMessage = errorMessage;

			ValidationParameters["email"] = new AttributeValuePair("type", "email");
		}
	}
}