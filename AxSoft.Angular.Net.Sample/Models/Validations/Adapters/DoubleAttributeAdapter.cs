using AxSoft.Angular.Net.Validation;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AxSoft.Angular.Net.Sample.Models.Validations.Adapters
{
	public class DoubleAttributeAdapter : AngularDataAnnotationsModelValidator<DoubleAttribute>
	{
		public DoubleAttributeAdapter(ModelMetadata metadata, AngularBindingContext context, DoubleAttribute attribute)
			: base(metadata, context, attribute)
		{
		}

		public override IEnumerable<ValidationRule> GetClientValidationRules()
		{
			yield return new DoubleValidationRule(ErrorMessage, Attribute.GetMinValue(), Attribute.GetMaxValue());
		}
	}

	public class DoubleValidationRule : ValidationRule
	{
		public DoubleValidationRule(string errorMessage, double? minValue, double? maxValue)
		{
			ErrorMessage = errorMessage;

			var attsValues = new List<string>();

			if (minValue.HasValue)
			{
				attsValues.Add(string.Format("min: {0}", minValue.Value));
			}

			if (maxValue.HasValue)
			{
				attsValues.Add(string.Format("max: {0}", maxValue.Value));
			}

			string attrValue = attsValues.Any() ? string.Concat("{", string.Join(", ", attsValues), "}") : string.Empty;

			//ValidationType = "pattern";
			ValidationParameters.Add("float", new AttributeValuePair("float", attrValue));
		}
	}
}