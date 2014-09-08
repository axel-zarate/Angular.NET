using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace AxSoft.Angular.Net
{
	/// <summary>
	/// Represents the base class for AngularJS binding contexts.
	/// </summary>
	public abstract class AngularBindingContext
	{
		private readonly string _prefix;

		/// <summary>
		/// Initializes a new instance of the <see cref="AngularBindingContext"/> class.
		/// </summary>
		/// <param name="prefix">The name of the model in the scope.</param>
		protected AngularBindingContext(string prefix)
		{
			if (!string.IsNullOrWhiteSpace(prefix))
			{
				_prefix = prefix;
			}
		}

		/// <summary>
		/// Gets the name of the model in the scope.
		/// </summary>
		public string Prefix
		{
			get { return _prefix; }
		}

		/// <summary>
		/// Gets a string used to prefix properties for bindings.
		/// </summary>
		/// <param name="delimiter">The property delimiter.</param>
		public virtual string GetPropertyPrefix(string delimiter)
		{
			return Prefix != null ? Prefix + (delimiter ?? AngularConfiguration.PropertyDelimiter) : null;
		}

		/// <summary>
		/// Creates an HTML attribute from the name and value provided.
		/// </summary>
		/// <param name="name">The attribute name.</param>
		/// <param name="value">The attribute value.</param>
		protected internal virtual IHtmlString CreateHtmlAttribute(string name, string value)
		{
			string result = string.IsNullOrEmpty(value) ? name : string.Format("{0}=\"{1}\"", name, value);

			return new HtmlString(result);
		}
	}

	/// <summary>
	/// Generic AngularJS binding context that can be used to generate bindings.
	/// </summary>
	/// <typeparam name="TModel">The type of the model.</typeparam>
	public class AngularBindingContext<TModel> : AngularBindingContext
	{
		private readonly HtmlHelper<TModel> _helper;

		internal AngularBindingContext(HtmlHelper<TModel> helper, string prefix)
			: base(prefix)
		{
			_helper = helper;
		}

		/// <summary>
		/// Gets HtmlHelper instance that this context is using.
		/// </summary>
		protected HtmlHelper<TModel> Helper
		{
			get { return _helper; }
		}

		/// <summary>
		/// Gets the display name for the model.
		/// </summary>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="expression">An expression that identifies the object that contains the display name.</param>
		/// <returns>The display name for the model.</returns>
		public IHtmlString DisplayFor<TProperty>(Expression<Func<TModel, TProperty>> expression)
		{
			return Helper.DisplayNameFor(expression);
		}

		/// <summary>
		/// Generates an interpolated AngularJS binding for the specified property.
		/// </summary>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="expression">An expression that identifies the property to interpolate.</param>
		public IHtmlString Interpolate<TProperty>(Expression<Func<TModel, TProperty>> expression)
		{
			return Interpolate(expression, null);
		}

		/// <summary>
		/// Generates an interpolated AngularJS binding for the specified property.
		/// </summary>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="expression">An expression that identifies the property to interpolate.</param>
		/// <param name="filter">An optional filter to add to the interpolation.</param>
		public IHtmlString Interpolate<TProperty>(Expression<Func<TModel, TProperty>> expression, string filter)
		{
			string value = ConvertExpression(expression);
			if (!string.IsNullOrWhiteSpace(filter))
			{
				value += " | " + filter;
			}
			return new MvcHtmlString(string.Format("{{{{{0}}}}}", value));
		}

		/// <summary>
		/// Generates an ng-bind directive attribute for the specified property.
		/// </summary>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="expression">An expression that identifies the property to bind.</param>
		public IHtmlString NgBind<TProperty>(Expression<Func<TModel, TProperty>> expression)
		{
			return NgBind(expression, null);
		}

		/// <summary>
		/// Generates an ng-bind directive attribute for the specified property.
		/// </summary>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="expression">An expression that identifies the property to bind.</param>
		/// <param name="filter">An optional filter to add to the binding.</param>
		public IHtmlString NgBind<TProperty>(Expression<Func<TModel, TProperty>> expression, string filter)
		{
			string value = ConvertExpression(expression);
			if (!string.IsNullOrWhiteSpace(filter))
			{
				value += " | " + filter;
			}
			return CreateHtmlAttribute("ng-bind", value);
		}

		/// <summary>
		/// Generates an ng-bind-html directive attribute for the specified property.
		/// </summary>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="expression">An expression that identifies the property to bind.</param>
		public IHtmlString NgBindHtml<TProperty>(Expression<Func<TModel, TProperty>> expression)
		{
			return CreateHtmlAttribute("ng-bind-html", expression);
		}

		/// <summary>
		/// Creates a new repeat context for the specified property. This context can be used to render the ng-repeat directive and create bindings for the ng-repeat item.
		/// </summary>
		/// <typeparam name="TItem">The type of the item.</typeparam>
		/// <param name="listExpression">An expression that identifies the property to iterate.</param>
		public NgRepeatContext<TItem> Repeat<TItem>(Expression<Func<TModel, IEnumerable<TItem>>> listExpression)
		{
			string value = ExpressionHelper.GetExpressionText(listExpression);
			
			var childHelper = new HtmlHelper<TItem>(Helper.ViewContext, Helper.ViewDataContainer);
			return new NgRepeatContext<TItem>(childHelper, value, Prefix);
		}

		/// <summary>
		/// Converts the expression that identifies a property into its client-side repesentration.
		/// </summary>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="expression">An expression that identifies the property.</param>
		protected virtual string ConvertExpression<TProperty>(Expression<Func<TModel, TProperty>> expression)
		{
			var expressionText = ExpressionHelper.GetExpressionText(expression);
			string prefix = GetPropertyPrefix(AngularConfiguration.PropertyDelimiter);
			return prefix + expressionText;
		}

		/// <summary>
		/// Creates an HTML attribute converting the expression into its client-side repesentration value.
		/// </summary>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="name">The name of the attribute.</param>
		/// <param name="expression">An expression that identifies the property.</param>
		protected virtual IHtmlString CreateHtmlAttribute<TProperty>(string name, Expression<Func<TModel, TProperty>> expression)
		{
			string value = ConvertExpression(expression);
			return CreateHtmlAttribute(name, value);
		}
	}
}