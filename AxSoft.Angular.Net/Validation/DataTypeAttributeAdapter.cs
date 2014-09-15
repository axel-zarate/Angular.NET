using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;

namespace AxSoft.Angular.Net.Validation
{
	internal class DataTypeAttributeAdapter : AngularDataAnnotationsModelValidator
	{
		private readonly DataType _dataType;

		public DataTypeAttributeAdapter(ModelMetadata metadata, AngularBindingContext context, DataTypeAttribute attribute, string ruleName)
			: base(metadata, context, attribute)
		{
			RuleName = ruleName; //?? attribute.DataType.ToString().ToLower(CultureInfo.CurrentCulture);
			_dataType = attribute.DataType;
		}

		public string RuleName { get; set; }

		public override IEnumerable<ValidationRule> GetClientValidationRules()
		{
			var rule = new ValidationRule { ErrorMessage = ErrorMessage };

			string ruleName = RuleName, attrName = "type", attrValue = RuleName;
			if (string.IsNullOrEmpty(ruleName))
			{
				ruleName = _dataType.ToString().ToLower(CultureInfo.CurrentCulture);
				attrName = "ng-" + ruleName;
				attrValue = string.Empty;
			}

			rule.ValidationParameters.Add(ruleName, new AttributeValuePair(attrName, attrValue));

			yield return rule;
		}

		//private string GetRuleNameFromDataType()
		//{
		//	switch (_dataType)
		//	{
		//		case DataType.Date:
		//	}
		//}
	}
}