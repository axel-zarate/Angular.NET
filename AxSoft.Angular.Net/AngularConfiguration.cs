namespace AxSoft.Angular.Net
{
	/// <summary>
	/// Contains configuration values.
	/// </summary>
	public class AngularConfiguration
	{
		private static string _errorCssClass = "error";
		private static string _labelCssClass = "control-label";
		private static string _inlineHelpCssClass = "help-inline";

		internal const string ElementNameDelimiter = "_";

		internal const string PropertyDelimiter = ".";

		/// <summary>
		/// Gets or sets the name of the CSS class to use for validation errors.
		/// </summary>
		public static string ErrorCssClass
		{
			get { return _errorCssClass; }
			set { _errorCssClass = value; }
		}

		/// <summary>
		///  Gets or sets the name of the CSS class to use for HTML label elements.
		/// </summary>
		public static string LabelCssClass
		{
			get { return _labelCssClass; }
			set { _labelCssClass = value; }
		}

		/// <summary>
		/// Gets or sets the name of the CSS class to use for validation message elements.
		/// </summary>
		public static string HelpCssClass
		{
			get { return _inlineHelpCssClass; }
			set { _inlineHelpCssClass = value; }
		}
	}
}