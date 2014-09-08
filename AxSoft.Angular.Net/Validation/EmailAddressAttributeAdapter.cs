using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SS.Web.Angular.Validation
{
	public class EmailAddressAttributeAdapter : AngularDataAnnotationsModelValidator<EmailAddressAttribute>
	{
		public EmailAddressAttributeAdapter(ModelMetadata metadata, AngularBindingContext context, EmailAddressAttribute attribute)
			: base(metadata, context, attribute)
		{
		}
	}
}