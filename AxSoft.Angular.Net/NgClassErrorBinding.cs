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
		private readonly List<string> _dirty = new List<string>();
		private readonly AngularForm<TModel> _form;
		private readonly bool _onDirtyOnly;
		private readonly List<string> _validations = new List<string>();

		internal NgClassErrorBinding(bool onDirtyOnly, AngularForm<TModel> form)
		{
			_onDirtyOnly = onDirtyOnly;
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

			if (_onDirtyOnly)
			{
				_dirty.Add(formNodeName + ".$dirty");
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

				if (_dirty.Any())
				{
					builder.Append(" && (");
					builder.Append(string.Join(" || ", _dirty));
					builder.Append(")");
				}
				builder.Append(" }\"");

				return builder.ToString();
			}

			return null;
		}
	}
}