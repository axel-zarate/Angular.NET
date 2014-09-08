using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SS.Web.Angular.Validation
{
	public class NgDirectiveAttributeAdapter : AngularModelValidator
	{
		private readonly NgDirectiveAttribute _attribute;

		public NgDirectiveAttributeAdapter(ModelMetadata metadata, NgDirectiveAttribute attribute)
			: base(metadata)
		{
			_attribute = attribute;
		}

		public NgDirectiveAttribute Attribute
		{
			get { return _attribute; }
		}

		public override IEnumerable<ValidationRule> GetClientValidationRules()
		{
			var rule = new ValidationRule();
			rule.ValidationParameters[Attribute.Name] = new AttributeValuePair(Attribute.Name, Attribute.Value);
			yield return rule;
		}
	}
}
