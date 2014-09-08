using System.Web;
using System.Web.Mvc;

namespace AxSoft.Angular.Net
{
	/// <summary>
	/// Represents an AngularJS subcontext, such as a repeat context.
	/// </summary>
	/// <typeparam name="TModel">The type of the model.</typeparam>
	public abstract class AngularBindingSubcontext<TModel> : AngularBindingContext<TModel>
	{
		private readonly string _subexpression;

		internal AngularBindingSubcontext(HtmlHelper<TModel> helper, string subexpression, string prefix)
			: base(helper, prefix)
		{
			_subexpression = subexpression;
		}

		/// <summary>
		/// Gets the name of the context item.
		/// </summary>
		public abstract string Item { get; }

		/// <summary>
		/// Gets the AngularJS model expression for this subcontext.
		/// </summary>
		protected string Subexpression
		{
			get { return _subexpression; }
		}

		/// <summary>
		/// Gets a string used to prefix properties for bindings.
		/// </summary>
		/// <param name="delimiter">The property delimiter.</param>
		public override string GetPropertyPrefix(string delimiter)
		{
			return Item + AngularConfiguration.PropertyDelimiter;
		}

		/// <summary>
		/// Renders the AngularJS directive that corresponds to this subcontext.
		/// </summary>
		public abstract IHtmlString RenderDirective();
	}
}