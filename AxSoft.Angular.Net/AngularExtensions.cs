using System.Collections.Generic;
using System.Web.Mvc;

namespace AxSoft.Angular.Net
{
	/// <summary>
	/// Extends the <see cref="HtmlHelper{T}" /> class to create AngularJS bindings.
	/// </summary>
	public static class AngularExtensions
	{
		#region Angular Form

		/// <summary>
		/// Creates a new AngularForm object and writes the opening &lt;form&gt; tag to the response.
		/// </summary>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		/// <param name="helper">The HTML helper instance that this method extends.</param>
		/// <returns>A new AngularForm object.</returns>
		public static AngularForm<TModel> BeginAngularForm<TModel>(this HtmlHelper<TModel> helper) where TModel : class
		{
			return helper.BeginAngularForm(null);
		}

		/// <summary>
		/// Creates a new AngularForm object and writes the opening &lt;form&gt; tag to the response.
		/// </summary>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		/// <param name="helper">The HTML helper instance that this method extends.</param>
		/// <param name="name">The name of the form.</param>
		/// <returns>A new AngularForm object.</returns>
		public static AngularForm<TModel> BeginAngularForm<TModel>(this HtmlHelper<TModel> helper, string name) where TModel : class
		{
			return helper.BeginAngularForm(name, null as IDictionary<string, object>);
		}

		/// <summary>
		/// Creates a new AngularForm object and writes the opening &lt;form&gt; tag to the response.
		/// </summary>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		/// <param name="helper">The HTML helper instance that this method extends.</param>
		/// <param name="name">The name of the form.</param>
		/// <param name="submitMethod">The name of the scope method that will be invoked when the form is submitted.</param>
		/// <returns>A new AngularForm object.</returns>
		public static AngularForm<TModel> BeginAngularForm<TModel>(this HtmlHelper<TModel> helper, string name, string submitMethod) where TModel : class
		{
			return helper.BeginAngularForm(name, submitMethod, null as IDictionary<string, object>);
		}

		/// <summary>
		/// Creates a new AngularForm object and writes the opening &lt;form&gt; tag to the response.
		/// </summary>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		/// <param name="helper">The HTML helper instance that this method extends.</param>
		/// <param name="name">The name of the form.</param>
		/// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
		/// <returns>A new AngularForm object.</returns>
		public static AngularForm<TModel> BeginAngularForm<TModel>(this HtmlHelper<TModel> helper, string name, object htmlAttributes) where TModel : class
		{
			return helper.BeginAngularForm(name, null, null, htmlAttributes);
		}

		/// <summary>
		/// Creates a new AngularForm object and writes the opening &lt;form&gt; tag to the response.
		/// </summary>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		/// <param name="helper">The HTML helper instance that this method extends.</param>
		/// <param name="name">The name of the form.</param>
		/// <param name="htmlAttributes">A dictionary that contains the HTML attributes to set for the element.</param>
		/// <returns>A new AngularForm object.</returns>
		public static AngularForm<TModel> BeginAngularForm<TModel>(this HtmlHelper<TModel> helper, string name, IDictionary<string, object> htmlAttributes) where TModel : class
		{
			return helper.BeginAngularForm(name, null, null, htmlAttributes);
		}

		/// <summary>
		/// Creates a new AngularForm object and writes the opening &lt;form&gt; tag to the response.
		/// </summary>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		/// <param name="helper">The HTML helper instance that this method extends.</param>
		/// <param name="name">The name of the form.</param>
		/// <param name="submitMethod">The name of the scope method that will be invoked when the form is submitted.</param>
		/// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
		/// <returns>A new AngularForm object.</returns>
		public static AngularForm<TModel> BeginAngularForm<TModel>(this HtmlHelper<TModel> helper, string name, string submitMethod, object htmlAttributes) where TModel : class
		{
			return helper.BeginAngularForm(name, submitMethod, null, htmlAttributes);
		}

		/// <summary>
		/// Creates a new AngularForm object and writes the opening &lt;form&gt; tag to the response.
		/// </summary>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		/// <param name="helper">The HTML helper instance that this method extends.</param>
		/// <param name="name">The name of the form.</param>
		/// <param name="submitMethod">The name of the scope method that will be invoked when the form is submitted.</param>
		/// <param name="htmlAttributes">A dictionary that contains the HTML attributes to set for the element.</param>
		/// <returns>A new AngularForm object.</returns>
		public static AngularForm<TModel> BeginAngularForm<TModel>(this HtmlHelper<TModel> helper, string name, string submitMethod, IDictionary<string, object> htmlAttributes) where TModel : class
		{
			return helper.BeginAngularForm(name, submitMethod, null, htmlAttributes);
		}

		/// <summary>
		/// Creates a new AngularForm object and writes the opening &lt;form&gt; tag to the response.
		/// </summary>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		/// <param name="helper">The HTML helper instance that this method extends.</param>
		/// <param name="name">The name of the form.</param>
		/// <param name="submitMethod">The name of the scope method that will be invoked when the form is submitted.</param>
		/// <param name="prefix">The name of the model in the scope.</param>
		/// <returns>A new AngularForm object.</returns>
		public static AngularForm<TModel> BeginAngularForm<TModel>(this HtmlHelper<TModel> helper, string name, string submitMethod, string prefix) where TModel : class
		{
			return helper.BeginAngularForm(name, submitMethod, prefix, null);
		}

		/// <summary>
		/// Creates a new AngularForm object and writes the opening &lt;form&gt; tag to the response.
		/// </summary>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		/// <param name="helper">The HTML helper instance that this method extends.</param>
		/// <param name="name">The name of the form.</param>
		/// <param name="submitMethod">The name of the scope method that will be invoked when the form is submitted.</param>
		/// <param name="prefix">The name of the model in the scope.</param>
		/// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
		/// <returns>A new AngularForm object.</returns>
		public static AngularForm<TModel> BeginAngularForm<TModel>(this HtmlHelper<TModel> helper, string name, string submitMethod, string prefix, object htmlAttributes) where TModel : class
		{
			return helper.BeginAngularForm(name, submitMethod, prefix, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		/// <summary>
		/// Creates a new AngularForm object and writes the opening &lt;form&gt; tag to the response.
		/// </summary>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		/// <param name="helper">The HTML helper instance that this method extends.</param>
		/// <param name="name">The name of the form.</param>
		/// <param name="submitMethod">The name of the scope method that will be invoked when the form is submitted.</param>
		/// <param name="prefix">The name of the model in the scope.</param>
		/// <param name="htmlAttributes">A dictionary that contains the HTML attributes to set for the element.</param>
		/// <returns>A new AngularForm object.</returns>
		public static AngularForm<TModel> BeginAngularForm<TModel>(this HtmlHelper<TModel> helper, string name, string submitMethod, string prefix, IDictionary<string, object> htmlAttributes) where TModel : class
		{
			return helper.BeginAngularFormHelper(name, submitMethod, prefix, htmlAttributes, false, true);
		}

        public static AngularForm<TModel> BeginAngularFormStub<TModel>(this HtmlHelper<TModel> helper, string name, string prefix) where TModel : class
        {
            return helper.BeginAngularFormHelper(name, null, prefix, null, false, false);
        }

		#endregion Angular Form

		//#region Ng Form

		//public static AngularForm<TModel> BeginNgForm<TModel>(this HtmlHelper<TModel> helper) where TModel : class
		//{
		//	return helper.BeginNgForm(null);
		//}

		//public static AngularForm<TModel> BeginNgForm<TModel>(this HtmlHelper<TModel> helper, string name) where TModel : class
		//{
		//	return helper.BeginNgForm(name, null);
		//}

		//public static AngularForm<TModel> BeginNgForm<TModel>(this HtmlHelper<TModel> helper, string name, object htmlAttributes) where TModel : class
		//{
		//	return helper.BeginNgForm(name, null, htmlAttributes);
		//}

		//public static AngularForm<TModel> BeginNgForm<TModel>(this HtmlHelper<TModel> helper, string name, string prefix) where TModel : class
		//{
		//	return helper.BeginNgForm(name, prefix, null);
		//}

		//public static AngularForm<TModel> BeginNgForm<TModel>(this HtmlHelper<TModel> helper, string name, string prefix, object htmlAttributes) where TModel : class
		//{
		//	return helper.BeginAngularFormHelper(name, null, prefix, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes), true);
		//}

		//#endregion Ng Form

		#region Binding Context

		/// <summary>
		/// Creates a new AngularBindingContext that can be used to generate AngularJS bindings.
		/// </summary>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		/// <param name="helper">The HTML helper instance that this method extends.</param>
		/// <returns>A new AngularBindingContext</returns>
		public static AngularBindingContext<TModel> CreateAngularContext<TModel>(this HtmlHelper<TModel> helper)
		{
			return helper.CreateAngularContext(null);
		}

		/// <summary>
		/// Creates a new AngularBindingContext that can be used to generate AngularJS bindings.
		/// </summary>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		/// <param name="helper">The HTML helper instance that this method extends.</param>
		/// <param name="prefix">The name of the model in the scope.</param>
		/// <returns>A new AngularBindingContext</returns>
		public static AngularBindingContext<TModel> CreateAngularContext<TModel>(this HtmlHelper<TModel> helper, string prefix)
		{
			return new AngularBindingContext<TModel>(helper, prefix);
		}

		#endregion Binding Context

		private static AngularForm<TModel> BeginAngularFormHelper<TModel>(this HtmlHelper<TModel> helper, string name, string submitMethod, string prefix, IDictionary<string, object> htmlAttributes, bool isNgForm, bool render) where TModel : class
		{
			var form = new AngularForm<TModel>(helper, name, submitMethod, prefix, htmlAttributes, render);
			form.IsNgForm = isNgForm;
			form.Initialize();
			return form;
		}
	}
}