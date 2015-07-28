using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace AxSoft.Angular.Net
{
	/// <summary>
	/// Represents a binding used to show validation errors.
	/// </summary>
	/// <typeparam name="TModel">The type of the model.</typeparam>
	public class NgClassErrorBinding<TModel> : IHtmlString
		where TModel : class
	{
		private readonly List<string> _conditions = new List<string>();
		private readonly ValidationMode _validationMode;
		private readonly AngularForm<TModel> _form;
		private readonly List<string> _validations = new List<string>();

		internal NgClassErrorBinding(ValidationMode validationMode, AngularForm<TModel> form)
		{
			_validationMode = validationMode;
			_form = form;
		}

		/// <summary>
		/// Retrieves the validation metadata for the specified expression and concatenates a validation statement to the current binding.
		/// </summary>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="expression">An expression that identifies the object that contains the properties to render.</param>
		public NgClassErrorBinding<TModel> And<TProperty>(Expression<Func<TModel, TProperty>> expression)
		{
			var expressionText = ExpressionHelper.GetExpressionText(expression);
			var nodeName = _form.GetPropertyIdentifier(expressionText);
			var formNodeName = _form.GetElementFormIdentifier(nodeName);
			_validations.Add(formNodeName + ".$invalid");

			switch (_validationMode)
			{
				case ValidationMode.FormSubmitted:
					_conditions.Add(_form.Name + ".$submitted");
					break;
				case ValidationMode.FormDirty:
					_conditions.Add(_form.Name + ".$dirty");
					break;
				case ValidationMode.ControlDirty:
					_conditions.Add(formNodeName + ".$dirty");
					break;
			}

			return this;
		}

		/// <summary>
		/// Returns an HTML-encoded string with the validation bindings.
		/// </summary>
		public string ToHtmlString()
		{
			if (_validations.Any())
			{
				var builder = new StringBuilder("ng-class=\"{ '" + AngularConfiguration.ErrorCssClass + "': (");

				builder.Append(string.Join(" || ", _validations));
				builder.Append(")");

				if (_conditions.Any())
				{
					builder.Append(" && (");
					builder.Append(string.Join(" || ", _conditions));
					builder.Append(")");
				}
				builder.Append(" }\"");

				return builder.ToString();
			}

			return null;
		}
	}
}