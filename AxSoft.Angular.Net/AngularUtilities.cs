using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SS.Web.Angular
{
	static class AngularUtilities
	{
		public static void GetValidationAttributes(IEnumerable<ModelClientValidationRule> clientRules, IDictionary<string, BindingInfo> results)
		{
			if (clientRules == null)
			{
				throw new ArgumentNullException("clientRules");
			}
			if (results == null)
			{
				throw new ArgumentNullException("results");
			}
			//bool flag = false;
			foreach (ModelClientValidationRule rule in clientRules)
			{
				//flag = true;
				string dictionaryKey = rule.ValidationType;
				var bindingInfo = new BindingInfo { ErrorMessage = rule.ErrorMessage ?? string.Empty };
				ValidateUnobtrusiveValidationRule(rule);
				
				//dictionaryKey = dictionaryKey + "-";
				//foreach (KeyValuePair<string, object> pair in rule.ValidationParameters)
				//{
				//	string key = dictionaryKey + pair.Key;
				//	results.Add(key, HttpUtility.HtmlEncode(pair.Value ?? string.Empty));
				//}

				switch (rule.ValidationType)
				{
					case "required":
						break;
					case "date":
						dictionaryKey = "datepicker";
						break;
					case "length":
						var max = rule.ValidationParameters.Where(p => p.Key == "max").Select(p => p.Value).FirstOrDefault();
						if (max != null)
						{
							results.Add("ng-maxlength", new BindingInfo { Value = max, ErrorMessage = rule.ErrorMessage, DirectiveKey = "maxlength" });
						}
						var min = rule.ValidationParameters.Where(p => p.Key == "min").Select(p => p.Value).FirstOrDefault();
						if (min != null)
						{
							results.Add("ng-minlength", new BindingInfo { Value = min, ErrorMessage = rule.ErrorMessage, DirectiveKey = "minlength" });
						}
						continue;
					case "regex":
						dictionaryKey = "ng-pattern";
						var pattern = rule.ValidationParameters.Where(p => p.Key == "pattern").Select(p => p.Value).FirstOrDefault();
						if (pattern != null)
						{
							bindingInfo.Value = string.Format("/{0}/", pattern);
							bindingInfo.ErrorMessage = rule.ErrorMessage;
							bindingInfo.DirectiveKey = "pattern";
						}
						break;
				}

				results.Add(dictionaryKey, bindingInfo);
			}
		}

		private static void ValidateUnobtrusiveValidationRule(ModelClientValidationRule rule)
		{
			if (string.IsNullOrWhiteSpace(rule.ValidationType))
			{
				throw new InvalidOperationException("Validation type cannot be empty.");
			}
			//if (resultsDictionary.ContainsKey(dictionaryKey))
			//{
			//	throw new InvalidOperationException("Validation type must be unique.");
			//}
			if (rule.ValidationType.Any<char>(c => !char.IsLower(c)))
			{
				throw new InvalidOperationException("Validation type must be legal.");
			}
			foreach (string str in rule.ValidationParameters.Keys)
			{
				if (string.IsNullOrWhiteSpace(str))
				{
					throw new InvalidOperationException("Validation parameter cannot be emtpy.");
				}
				if (!char.IsLower(str.First<char>()) || str.Any<char>(c => (!char.IsLower(c) && !char.IsDigit(c))))
				{
					throw new InvalidOperationException("Validation parameter must be legal.");
				}
			}
		}
	}
}
