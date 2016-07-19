using AxSoft.Angular.Net.Validation;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace AxSoft.Angular.Net
{
	/// <summary>
	/// Specifies when to show the validation messages for a field.
	/// </summary>
	public enum ValidationMode
	{
		/// <summary>
		/// Validations are shown when the any control in the form is modified.
		/// </summary>
		FormDirty,
		/// <summary>
		/// Validations are shown for each individual control when it is modified.
		/// </summary>
		ControlDirty,
		/// <summary>
		/// Validations are shown after the user tries to submit the form
		/// </summary>
		FormSubmitted,
		/// <summary>
		/// Validations are always displayed.
		/// </summary>
		Always
	}

	/// <summary>
	/// Represents an HTML form element in an MVC view that contains AngularJS bindings.
	/// </summary>
	/// <typeparam name="TModel">The type of the model.</typeparam>
	public class AngularForm<TModel> : AngularBindingContext<TModel>, IDisposable where TModel : class
	{
		private readonly HtmlHelper<TModel> _helper;
		private readonly IDictionary<string, object> _htmlAttributes;
		private readonly string _name;
		private readonly bool _render;
		private readonly string _submitMethod;
		private bool _disposed;

		/// <summary>
		/// Gets or sets the form's validation mode.
		/// </summary>
		/// <value>
		/// The validation mode.
		/// </value>
		public ValidationMode ValidationMode { get; set; }

		//private readonly Dictionary<string, ModelMetadata> _cachedMetadatas = new Dictionary<string, ModelMetadata>(StringComparer.CurrentCultureIgnoreCase);

		internal AngularForm(HtmlHelper<TModel> helper, string name, string submitMethod, string prefix, IDictionary<string, object> htmlAttributes, bool render)
			: base(helper, prefix)
		{
			if (helper == null)
			{
				throw new ArgumentNullException("helper");
			}

			_helper = helper;
			if (!string.IsNullOrWhiteSpace(name))
			{
				_name = name;
			}

			if (!string.IsNullOrWhiteSpace(submitMethod))
			{
				_submitMethod = submitMethod;
			}
			_htmlAttributes = htmlAttributes;
			_render = render;

			ValidationMode = AngularConfiguration.DefaultControlValidationMode;
		}

		/// <summary>
		/// Gets the name of this form.
		/// </summary>
		public string Name
		{
			get { return _name ?? "form"; }
		}

		internal bool IsNgForm { get; set; }

		/// <summary>
		/// Gets the submit method for this form.
		/// </summary>
		protected string SubmitMethod
		{
			get { return _submitMethod ?? "submit"; }
		}

		/// <summary>
		/// Gets the HTML tag name used to render this form.
		/// </summary>
		protected string TagName
		{
			get { return IsNgForm ? "div" : "form"; }
		}

		/// <summary>
		/// Returns a check box input element for each property in the object that is represented by the specified expression.
		/// </summary>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="expression">An expression that identifies the object that contains the properties to display.</param>
		/// <returns>An HTML input element whose type attribute is set to "checkbox" for each property in the object that is represented by the expression.</returns>
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
		public IHtmlString CheckBox<TProperty>(Expression<Func<TModel, TProperty>> expression)
		{
			return CheckBox(expression, null);
		}

		/// <summary>
		/// Returns a check box input element for each property in the object that is represented by the specified expression.
		/// </summary>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="expression">An expression that identifies the object that contains the properties to display.</param>
		/// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
		/// <returns>An HTML input element whose type attribute is set to "checkbox" for each property in the object that is represented by the expression.</returns>
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
		public IHtmlString CheckBox<TProperty>(Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
		{
			return CheckBox(expression, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		/// <summary>
		/// Returns a check box input element for each property in the object that is represented by the specified expression.
		/// </summary>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="expression">An expression that identifies the object that contains the properties to display.</param>
		/// <param name="htmlAttributes">A dictionary that contains the HTML attributes to set for the element.</param>
		/// <returns>An HTML input element whose type attribute is set to "checkbox" for each property in the object that is represented by the expression.</returns>
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
		public IHtmlString CheckBox<TProperty>(Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes)
		{
			var metadata = ModelMetadata.FromLambdaExpression(expression, _helper.ViewData);
			return Input(expression, "checkbox", htmlAttributes, metadata);
		}

		/// <summary>
		/// Releases all resources that are used by the current instance of the <see cref="AxSoft.Angular.Net.AngularForm{TModel}"/> class.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Returns an HTML select element for each property in the object that is represented by the specified expression using the specified list items.
		/// </summary>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="expression">An expression that identifies the object that contains the properties to display.</param>
		/// <param name="ngOptions">The ng-options directive binding.</param>
		/// <returns>An HTML select element for each property in the object that is represented by the expression.</returns>
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
		public IHtmlString Dropdown<TProperty>(Expression<Func<TModel, TProperty>> expression, string ngOptions)
		{
			return Dropdown(expression, ngOptions, null, null);
		}

		/// <summary>
		/// Returns an HTML select element for each property in the object that is represented by the specified expression using the specified list items.
		/// </summary>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="expression">An expression that identifies the object that contains the properties to display.</param>
		/// <param name="ngOptions">The ng-options directive binding.</param>
		/// <param name="emptyElementText">The text to display when no value is selected.</param>
		/// <returns>An HTML select element for each property in the object that is represented by the expression.</returns>
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
		public IHtmlString Dropdown<TProperty>(Expression<Func<TModel, TProperty>> expression, string ngOptions, string emptyElementText)
		{
			return Dropdown(expression, ngOptions, emptyElementText, null);
		}

		/// <summary>
		/// Returns an HTML select element for each property in the object that is represented by the specified expression using the specified list items.
		/// </summary>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="expression">An expression that identifies the object that contains the properties to display.</param>
		/// <param name="ngOptions">The ng-options directive binding.</param>
		/// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
		/// <returns>An HTML select element for each property in the object that is represented by the expression.</returns>
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
		public IHtmlString Dropdown<TProperty>(Expression<Func<TModel, TProperty>> expression, string ngOptions, object htmlAttributes)
		{
			return Dropdown(expression, ngOptions, null, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		/// <summary>
		/// Returns an HTML select element for each property in the object that is represented by the specified expression using the specified list items.
		/// </summary>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="expression">An expression that identifies the object that contains the properties to display.</param>
		/// <param name="ngOptions">The ng-options directive binding.</param>
		/// <param name="htmlAttributes">A dictionary that contains the HTML attributes to set for the element.</param>
		/// <returns>An HTML select element for each property in the object that is represented by the expression.</returns>
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
		public IHtmlString Dropdown<TProperty>(Expression<Func<TModel, TProperty>> expression, string ngOptions, IDictionary<string, object> htmlAttributes)
		{
			return Dropdown(expression, ngOptions, null, htmlAttributes);
		}

		/// <summary>
		/// Returns an HTML select element for each property in the object that is represented by the specified expression using the specified list items.
		/// </summary>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="expression">An expression that identifies the object that contains the properties to display.</param>
		/// <param name="ngOptions">The ng-options directive binding.</param>
		/// <param name="emptyElementText">The text to display when no value is selected.</param>
		/// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
		/// <returns>An HTML select element for each property in the object that is represented by the expression.</returns>
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
		public IHtmlString Dropdown<TProperty>(Expression<Func<TModel, TProperty>> expression, string ngOptions, string emptyElementText, object htmlAttributes)
		{
			return Dropdown(expression, ngOptions, emptyElementText, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		/// <summary>
		/// Returns an HTML select element for each property in the object that is represented by the specified expression using the specified list items.
		/// </summary>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="expression">An expression that identifies the object that contains the properties to display.</param>
		/// <param name="ngOptions">The ng-options directive binding.</param>
		/// <param name="emptyElementText">The text to display when no value is selected.</param>
		/// <param name="htmlAttributes">A dictionary that contains the HTML attributes to set for the element.</param>
		/// <returns>An HTML select element for each property in the object that is represented by the expression.</returns>
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
		public IHtmlString Dropdown<TProperty>(Expression<Func<TModel, TProperty>> expression, string ngOptions, string emptyElementText, IDictionary<string, object> htmlAttributes)
		{
			var metadata = ModelMetadata.FromLambdaExpression(expression, _helper.ViewData);
			var tagBuilder = CreateTagBuilder(expression, "select", htmlAttributes, metadata);
			if (!string.IsNullOrEmpty(ngOptions))
			{
				tagBuilder.Attributes["ng-options"] = ngOptions;
			}

			if (!string.IsNullOrEmpty(emptyElementText))
			{
				var option = new AngularTagBuilder("option");
				option.Attributes["value"] = string.Empty;
				option.SetInnerText(emptyElementText);
				tagBuilder.InnerHtml = option.ToString(TagRenderMode.Normal);
			}

			return new MvcHtmlString(tagBuilder.ToString(TagRenderMode.Normal));
		}

		/// <summary>
		/// Returns a hidden input element the HTML attributes.
		/// </summary>
		/// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
		/// <returns>An input element whose type attribute is set to "hidden".</returns>
		public IHtmlString Hidden(object htmlAttributes)
		{
			return Hidden(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		/// <summary>
		/// Returns a hidden input element the HTML attributes.
		/// </summary>
		/// <param name="htmlAttributes">A dictionary that contains the HTML attributes to set for the element.</param>
		/// <returns>An input element whose type attribute is set to "hidden".</returns>
		public IHtmlString Hidden(IDictionary<string, object> htmlAttributes)
		{
			return Input(null as Expression<Func<TModel, object>>, "hidden", htmlAttributes);
		}

		/// <summary>
		/// Returns an HTML hidden input element for each property in the object that is represented by the specified expression.
		/// </summary>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="expression">An expression that identifies the property to display.</param>
		/// <returns>An input element whose type attribute is set to "hidden" for each property in the object that is represented by the expression.</returns>
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
		public IHtmlString Hidden<TProperty>(Expression<Func<TModel, TProperty>> expression)
		{
			return Hidden(expression, null);
		}

		/// <summary>
		/// Returns an HTML hidden input element for each property in the object that is represented by the specified expression.
		/// </summary>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="expression">An expression that identifies the property to display.</param>
		/// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
		/// <returns>An input element whose type attribute is set to "hidden" for each property in the object that is represented by the expression.</returns>
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
		public IHtmlString Hidden<TProperty>(Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
		{
			return Hidden(expression, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		/// <summary>
		/// Returns an HTML hidden input element for each property in the object that is represented by the specified expression.
		/// </summary>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="expression">An expression that identifies the property to display.</param>
		/// <param name="htmlAttributes">A dictionary that contains the HTML attributes to set for the element.</param>
		/// <returns>An input element whose type attribute is set to "hidden" for each property in the object that is represented by the expression.</returns>
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
		public IHtmlString Hidden<TProperty>(Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes)
		{
			return Input(expression, "hidden", htmlAttributes);
		}

		/// <summary>
		/// Returns an HTML label element and the property name of the property that is represented by the specified expression.
		/// </summary>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="expression">An expression that identifies the property to display.</param>
		/// <returns>An HTML label element and the property name of the property that is represented by the expression.</returns>
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
		public IHtmlString Label<TProperty>(Expression<Func<TModel, TProperty>> expression)
		{
			return Label(expression, null);
		}

		/// <summary>
		/// Returns an HTML label element and the property name of the property that is represented by the specified expression.
		/// </summary>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="expression">An expression that identifies the property to display.</param>
		/// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
		/// <returns>An HTML label element and the property name of the property that is represented by the expression.</returns>
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
		public IHtmlString Label<TProperty>(Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
		{
			return Label(expression, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		/// <summary>
		/// Returns an HTML label element and the property name of the property that is represented by the specified expression.
		/// </summary>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="expression">An expression that identifies the property to display.</param>
		/// <param name="htmlAttributes">A dictionary that contains the HTML attributes to set for the element.</param>
		/// <returns>An HTML label element and the property name of the property that is represented by the expression.</returns>
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
		public IHtmlString Label<TProperty>(Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes)
		{
			var metadata = ModelMetadata.FromLambdaExpression(expression, _helper.ViewData);
			string displayName = metadata.DisplayName ?? metadata.PropertyName;

			var tagBuilder = new AngularTagBuilder("label");
			tagBuilder.Attributes["class"] = AngularConfiguration.LabelCssClass;

			var expressionText = ExpressionHelper.GetExpressionText(expression);
			var nodeName = GetElementIdentifier(expressionText);

			tagBuilder.MergeAttribute("for", nodeName);

			tagBuilder.SetInnerText(displayName);

			tagBuilder.MergeAttributes(htmlAttributes, true);

			return new MvcHtmlString(tagBuilder.ToString(TagRenderMode.Normal));
		}

		private readonly Regex _tagRegex = new Regex(@"^[a-z]+(-[a-z0-9]+)*$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

		/// <summary>
		/// Returns an HTML label element and the property name of the property that is represented by the specified expression.
		/// </summary>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="tagName">The custom tag name.</param>
		/// <param name="expression">An expression that identifies the property to display.</param>
		/// <returns>A custom HTML element for each property in the object that is represented by the expression.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="tagName"/> is <c>null</c>.</exception>
		/// <exception cref="System.ArgumentException"><paramref name="tagName"/> is not a valid HTML tag name.</exception>
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
		public IHtmlString Custom<TProperty>(string tagName, Expression<Func<TModel, TProperty>> expression)
		{
			return Custom(tagName, expression, null);
		}

		/// <summary>
		/// Returns an HTML label element and the property name of the property that is represented by the specified expression.
		/// </summary>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="tagName">The custom tag name.</param>
		/// <param name="expression">An expression that identifies the property to display.</param>
		/// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
		/// <returns>A custom HTML element for each property in the object that is represented by the expression.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="tagName"/> is <c>null</c>.</exception>
		/// <exception cref="System.ArgumentException"><paramref name="tagName"/> is not a valid HTML tag name.</exception>
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
		public IHtmlString Custom<TProperty>(string tagName, Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
		{
			return Custom(tagName, expression, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		/// <summary>
		/// Returns an HTML label element and the property name of the property that is represented by the specified expression.
		/// </summary>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="tagName">The custom tag name.</param>
		/// <param name="expression">An expression that identifies the property to display.</param>
		/// <param name="htmlAttributes">A dictionary that contains the HTML attributes to set for the element.</param>
		/// <returns>A custom HTML element for each property in the object that is represented by the expression.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="tagName"/> is <c>null</c>.</exception>
		/// <exception cref="System.ArgumentException"><paramref name="tagName"/> is not a valid HTML tag name.</exception>
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
		public IHtmlString Custom<TProperty>(string tagName, Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes)
		{
			if (tagName == null) throw new ArgumentNullException("tagName");
			if (expression == null) throw new ArgumentNullException("expression");

			if (!_tagRegex.IsMatch(tagName)) throw new ArgumentException(string.Format("Invalid tag name {0}", tagName));

			var metadata = ModelMetadata.FromLambdaExpression(expression, _helper.ViewData);
			var tagBuilder = CreateTagBuilder(expression, tagName, htmlAttributes, metadata);
			
			return new MvcHtmlString(tagBuilder.ToString(TagRenderMode.Normal));
		}

		/// <summary>
		/// Retrieves the validation metadata for the specified expression and generates an ng-class directive that applies an error for each validation rule.
		/// </summary>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="expression">An expression that identifies the object that contains the properties to render.</param>
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
		public NgClassErrorBinding<TModel> NgClassError<TProperty>(Expression<Func<TModel, TProperty>> expression)
		{
			return new NgClassErrorBinding<TModel>(ValidationMode, this).And(expression);
		}

		/// <summary>
		/// Retrieves the validation metadata for the specified expression and generates an ng-class directive that applies an error for each validation rule.
		/// </summary>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="expression">An expression that identifies the object that contains the properties to render.</param>
		/// <param name="validationMode">A value that indicates when the validation element should become visible.</param>
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
		public NgClassErrorBinding<TModel> NgClassError<TProperty>(Expression<Func<TModel, TProperty>> expression, ValidationMode validationMode)
		{
			return new NgClassErrorBinding<TModel>(validationMode, this).And(expression);
		}

		/// <summary>
		/// Returns a password input element for each property in the object that is represented by the specified expression.
		/// </summary>
		/// <param name="expression">An expression that identifies the object that contains the properties to render.</param>
		/// <returns>An HTML input element whose type attribute is set to "password" for each property in the object that is represented by the specified expression.</returns>
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
		public IHtmlString Password(Expression<Func<TModel, object>> expression)
		{
			return Password(expression, null);
		}

		/// <summary>
		/// Returns a password input element for each property in the object that is represented by the specified expression.
		/// </summary>
		/// <param name="expression">An expression that identifies the object that contains the properties to render.</param>
		/// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
		/// <returns>An HTML input element whose type attribute is set to "password" for each property in the object that is represented by the specified expression.</returns>
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
		public IHtmlString Password(Expression<Func<TModel, object>> expression, object htmlAttributes)
		{
			return Password(expression, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		/// <summary>
		/// Returns a password input element for each property in the object that is represented by the specified expression.
		/// </summary>
		/// <param name="expression">An expression that identifies the object that contains the properties to render.</param>
		/// <param name="htmlAttributes">A dictionary that contains the HTML attributes to set for the element.</param>
		/// <returns>An HTML input element whose type attribute is set to "password" for each property in the object that is represented by the specified expression.</returns>
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
		public IHtmlString Password(Expression<Func<TModel, object>> expression, IDictionary<string, object> htmlAttributes)
		{
			var metadata = ModelMetadata.FromLambdaExpression(expression, _helper.ViewData);
			return Input(expression, "password", htmlAttributes, metadata);
		}

		/// <summary>
		/// Returns a radio button input element for each property in the object that is represented by the specified expression.
		/// </summary>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="expression">An expression that identifies the object that contains the properties to render.</param>
		/// <returns>An HTML input element whose type attribute is set to "radio" for each property in the object that is represented by the specified expression.</returns>
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
		public IHtmlString RadioButton<TProperty>(Expression<Func<TModel, TProperty>> expression)
		{
			return RadioButton(expression, null);
		}

		/// <summary>
		/// Returns a radio button input element for each property in the object that is represented by the specified expression.
		/// </summary>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="expression">An expression that identifies the object that contains the properties to render.</param>
		/// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
		/// <returns>An HTML input element whose type attribute is set to "radio" for each property in the object that is represented by the specified expression.</returns>
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
		public IHtmlString RadioButton<TProperty>(Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
		{
			return RadioButton(expression, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		/// <summary>
		/// Returns a radio button input element for each property in the object that is represented by the specified expression.
		/// </summary>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="expression">An expression that identifies the object that contains the properties to render.</param>
		/// <param name="htmlAttributes">A dictionary that contains the HTML attributes to set for the element.</param>
		/// <returns>An HTML input element whose type attribute is set to "radio" for each property in the object that is represented by the specified expression.</returns>
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
		public IHtmlString RadioButton<TProperty>(Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes)
		{
			var metadata = ModelMetadata.FromLambdaExpression(expression, _helper.ViewData);
			return Input(expression, "radio", htmlAttributes, metadata);
		}

		/// <summary>
		/// Returns an HTML textarea element for each property in the object that is represented by the specified expression.
		/// </summary>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="expression">An expression that identifies the object that contains the properties to render.</param>
		/// <returns>An HTML textarea element for each property in the object that is represented by the expression.</returns>
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
		public IHtmlString TextArea<TProperty>(Expression<Func<TModel, TProperty>> expression)
		{
			return TextArea(expression, null);
		}

		/// <summary>
		/// Returns an HTML textarea element for each property in the object that is represented by the specified expression.
		/// </summary>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="expression">An expression that identifies the object that contains the properties to render.</param>
		/// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
		/// <returns>An HTML textarea element for each property in the object that is represented by the expression.</returns>
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
		public IHtmlString TextArea<TProperty>(Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
		{
			return TextArea(expression, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		/// <summary>
		/// Returns an HTML textarea element for each property in the object that is represented by the specified expression.
		/// </summary>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="expression">An expression that identifies the object that contains the properties to render.</param>
		/// <param name="htmlAttributes">A dictionary that contains the HTML attributes to set for the element.</param>
		/// <returns>An HTML textarea element for each property in the object that is represented by the expression.</returns>
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
		public IHtmlString TextArea<TProperty>(Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes)
		{
			var metadata = ModelMetadata.FromLambdaExpression(expression, _helper.ViewData);
			var tagBuilder = CreateTagBuilder(expression, "textarea", htmlAttributes, metadata);

			return new MvcHtmlString(tagBuilder.ToString(TagRenderMode.Normal));
		}

		/// <summary>
		/// Returns a text input element for each property in the object that is represented by the specified expression.
		/// </summary>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="expression">An expression that identifies the object that contains the properties to display.</param>
		/// <returns>An HTML input element whose type attribute is set to "text" for each property in the object that is represented by the expression.</returns>
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
		public IHtmlString TextBox<TProperty>(Expression<Func<TModel, TProperty>> expression)
		{
			return TextBox(expression, null);
		}

		/// <summary>
		/// Returns a text input element for each property in the object that is represented by the specified expression.
		/// </summary>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="expression">An expression that identifies the object that contains the properties to display.</param>
		/// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
		/// <returns>An HTML input element whose type attribute is set to "text" for each property in the object that is represented by the expression.</returns>
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
		public IHtmlString TextBox<TProperty>(Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
		{
			return TextBox(expression, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		/// <summary>
		/// Returns a text input element for each property in the object that is represented by the specified expression.
		/// </summary>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="expression">An expression that identifies the object that contains the properties to display.</param>
		/// <param name="htmlAttributes">A dictionary that contains the HTML attributes to set for the element.</param>
		/// <returns>An HTML input element whose type attribute is set to "text" for each property in the object that is represented by the expression.</returns>
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
		public IHtmlString TextBox<TProperty>(Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes)
		{
			var metadata = ModelMetadata.FromLambdaExpression(expression, _helper.ViewData);
			return Input(expression, "text", htmlAttributes, metadata);
		}

		/// <summary>
		/// Retrieves the validation metadata for the specified model and generates an HTML span element with AngularJS bindings for each validation rule.
		/// </summary>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="expression">An expression that identifies the object that contains the properties to render.</param>
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
		public IHtmlString ValidationsFor<TProperty>(Expression<Func<TModel, TProperty>> expression)
		{
			return ValidationsFor(expression, ValidationMode, null);
		}

		/// <summary>
		/// Retrieves the validation metadata for the specified model and generates an HTML span element with AngularJS bindings for each validation rule.
		/// </summary>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="expression">An expression that identifies the object that contains the properties to render.</param>
		/// <param name="validationMode">A value that indicates that the element should become visible only if the field has been modified.</param>
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
		public IHtmlString ValidationsFor<TProperty>(Expression<Func<TModel, TProperty>> expression, ValidationMode validationMode)
		{
			return ValidationsFor(expression, validationMode, null);
		}

		/// <summary>
		/// Retrieves the validation metadata for the specified model and generates an HTML span element with AngularJS bindings for each validation rule.
		/// </summary>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="expression">An expression that identifies the object that contains the properties to render.</param>
		/// <param name="validationMode">A value that indicates that the element should become visible only if the field has been modified.</param>
		/// <param name="htmlAttributes">An object that contains the HTML attributes for the element.</param>
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
		public IHtmlString ValidationsFor<TProperty>(Expression<Func<TModel, TProperty>> expression, ValidationMode validationMode, object htmlAttributes)
		{
			return ValidationsFor(expression, validationMode, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		/// <summary>
		/// Retrieves the validation metadata for the specified model and generates an HTML span element with AngularJS bindings for each validation rule.
		/// </summary>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="expression">An expression that identifies the object that contains the properties to render.</param>
		/// <param name="validationMode">A value that indicates that the element should become visible only if the field has been modified.</param>
		/// <param name="htmlAttributes">A dictionary that contains the HTML attributes for the element.</param>
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
		public IHtmlString ValidationsFor<TProperty>(Expression<Func<TModel, TProperty>> expression, ValidationMode validationMode, IDictionary<string, object> htmlAttributes)
		{
			var metadata = ModelMetadata.FromLambdaExpression(expression, _helper.ViewData);
			var tagBuilders = new List<AngularTagBuilder>();
			var expressionText = ExpressionHelper.GetExpressionText(expression);
			var nodeName = GetPropertyIdentifier(expressionText);
			var formNodeName = GetElementFormIdentifier(nodeName);

			var rules = GetValidationRules(metadata);
			foreach (var rule in rules)
			{
				var tagBuilder = CreateValidationTagBuilder(rule, formNodeName, validationMode, htmlAttributes);
				tagBuilders.Add(tagBuilder);
			}

			//var validations = GetValidationAttributes(expressionText, metadata);
			//foreach (var validation in validations)
			//{
			//	var tagBuilder = CreateValidationTagBuilder(validation.Value.DirectiveKey ?? validation.Key, formNodeName, validation.Value.ErrorMessage ?? string.Empty, onDirtyOnly, htmlAttributes);
			//	tagBuilders.Add(tagBuilder);
			//}

			if (tagBuilders.Any())
			{
				return new MvcHtmlString(tagBuilders.Aggregate(string.Empty, (s, builder) => s + builder.ToString()));
			}

			return null;
		}

		internal string GetElementFormIdentifier(string propertyIdentifier)
		{
			return string.Format("{0}['{1}']",
				Name,
				//PropertyDelimiter,
				propertyIdentifier);
		}

		internal string GetElementIdentifier(string expressionText)
		{
			return string.Format("{0}{1}",
				GetPropertyPrefix(AngularConfiguration.ElementNameDelimiter),
				expressionText)
				.Replace(AngularConfiguration.PropertyDelimiter, AngularConfiguration.ElementNameDelimiter);
		}

		internal void Initialize()
		{
			var tagBuilder = new AngularTagBuilder(TagName);
			tagBuilder.Attributes["id"] = Name;
			tagBuilder.Attributes["name"] = Name;

			if (IsNgForm)
			{
				tagBuilder.Attributes["ng-form"] = string.Empty;
			}
			else
			{
				tagBuilder.Attributes["ng-submit"] = string.Format("{0}({1})", SubmitMethod, Name);
				tagBuilder.Attributes["method"] = "POST";
				tagBuilder.Attributes["novalidate"] = string.Empty;
				tagBuilder.Attributes["autocomplete"] = "off";
			}

			tagBuilder.MergeAttributes(_htmlAttributes, true);

			if (_render)
			{
				_helper.ViewContext.Writer.Write(tagBuilder.ToString(TagRenderMode.StartTag));
			}
		}

		/// <summary>
		/// Releases unmanaged and - optionally - managed resources.
		/// </summary>
		/// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				_disposed = true;
				if (_render)
				{
					_helper.ViewContext.Writer.Write("</{0}>", TagName);
				}
			}
		}

		private AngularTagBuilder CreateValidationTagBuilder(ValidationRule rule, string nodeName, ValidationMode validationMode, IDictionary<string, object> htmlAttributes)
		{
			var tagBuilder = new AngularTagBuilder("span");
			tagBuilder.Attributes["class"] = AngularConfiguration.HelpCssClass;

			string ngShow = string.Empty;
			if (rule.ValidationParameters.Any())
			{
				var validations = rule.ValidationParameters.Select(kvp => string.Format("{0}.$error.{1}", nodeName, kvp.Key));
				ngShow += string.Format("({0})", string.Join(" || ", validations));

				switch (validationMode)
				{
					case ValidationMode.FormSubmitted:
						ngShow += string.Format(" && {0}.$submitted", Name);
						break;
					case ValidationMode.FormDirty:
						ngShow += string.Format(" && {0}.$dirty", Name);
						break;
					case ValidationMode.ControlDirty:
						ngShow += string.Format(" && {0}.$dirty", nodeName);
						break;
				}
			}

			tagBuilder.Attributes["ng-show"] = ngShow.TrimEnd(' ');
			tagBuilder.MergeAttributes(htmlAttributes, true);

			tagBuilder.SetInnerText(rule.ErrorMessage);
			return tagBuilder;
		}

		private AngularTagBuilder CreateTagBuilder<TProperty>(Expression<Func<TModel, TProperty>> expression, string tagName, IDictionary<string, object> htmlAttributes, ModelMetadata metadata)
		{
			var tagBuilder = new AngularTagBuilder(tagName);
			IEnumerable<ValidationRule> rules = null;

			if (expression != null)
			{
				var expressionText = ExpressionHelper.GetExpressionText(expression);
				var propertyName = GetPropertyIdentifier(expressionText);
				var nodeName = GetElementIdentifier(expressionText);

				tagBuilder.MergeAttribute("id", nodeName, true);
				tagBuilder.MergeAttribute("name", propertyName, true);
				tagBuilder.MergeAttribute("ng-model", propertyName, true);

				if (metadata != null)
				{
					rules = GetValidationRules(metadata);
				}
			}

			tagBuilder.MergeAttributes(htmlAttributes, true);
			if (rules != null)
			{
				foreach (var attr in rules.SelectMany(r => r.ValidationParameters))
				{
					tagBuilder.Attributes[attr.Value.Name] = Convert.ToString(attr.Value.Value);
				}
			}
			//tagBuilder.MergeAttributes(extra is IDictionary<string, object> ? (IDictionary<string, object>)extra : HtmlHelper.AnonymousObjectToHtmlAttributes(extra), true);

			return tagBuilder;
		}

		//private AngularTagBuilder CreateTagBuilder<TProperty>(Expression<Func<TModel, TProperty>> expression, string tagName, object htmlAttributes, ModelMetadata metadata, object extra = null)
		//{
		//	return CreateTagBuilder(expression, tagName, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes), metadata, extra);
		//}
		private IEnumerable<ValidationRule> GetValidationRules(ModelMetadata metadata)
		{
			var validationRules = AngularModelValidatorProviders.Providers.GetValidators(metadata, this).SelectMany(v => v.GetClientValidationRules());
			return validationRules;
		}

		private IHtmlString Input<TProperty>(Expression<Func<TModel, TProperty>> expression, string type, IDictionary<string, object> htmlAttributes = null, ModelMetadata metadata = null)
		{
			htmlAttributes = htmlAttributes ?? new Dictionary<string, object>();
			if (!htmlAttributes.ContainsKey("type"))
			{
				htmlAttributes.Add("type", type);
			}

			var tagBuilder = CreateTagBuilder(expression, "input", htmlAttributes, metadata);

			var value = tagBuilder.ToString(TagRenderMode.SelfClosing);
			return new MvcHtmlString(value);
		}
	}
}