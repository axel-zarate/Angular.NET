using AxSoft.Angular.Net.Properties;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace AxSoft.Angular.Net
{
	/// <summary>
	/// A simplified version of the <see cref="TagBuilder" /> class. It supports empty attributes.
	/// </summary>
	public class AngularTagBuilder
	{
		//private string _idAttributeDotReplacement;

		private string _innerHtml;

		/// <summary>
		/// Creates a new tag that has the specified tag name.
		/// </summary>
		/// <param name="tagName">The tag name without the "&lt;", "/", or "&gt;" delimiters.</param>
		/// <exception cref="System.ArgumentException">tagName is null or empty.</exception>
		public AngularTagBuilder(string tagName)
		{
			if (string.IsNullOrEmpty(tagName))
			{
				throw new ArgumentException(Resources.Argument_Cannot_Be_Null_Or_Empty, "tagName");
			}

			TagName = tagName;
			Attributes = new SortedDictionary<string, string>(StringComparer.Ordinal);
		}

		/// <summary>
		/// Gets the collection of attributes.
		/// </summary>
		public IDictionary<string, string> Attributes { get; private set; }

		/// <summary>
		/// Gets or sets the inner HTML value for the element.
		/// </summary>
		public string InnerHtml
		{
			get { return _innerHtml ?? string.Empty; }
			set { _innerHtml = value; }
		}

		/// <summary>
		/// Gets the tag name for this tag.
		/// </summary>
		public string TagName { get; private set; }

		/// <summary>
		/// Adds a CSS class to the list of CSS classes in the tag.
		/// </summary>
		/// <param name="value">The CSS class to add.</param>
		public void AddCssClass(string value)
		{
			string currentValue;

			if (Attributes.TryGetValue("class", out currentValue))
			{
				Attributes["class"] = (value + " " + currentValue).Trim();
			}
			else
			{
				Attributes["class"] = value;
			}
		}

		/// <summary>
		/// Adds a new attribute to the tag.
		/// </summary>
		/// <param name="key">The key for the attribute.</param>
		/// <param name="value">The value of the attribute.</param>
		public void MergeAttribute(string key, string value)
		{
			MergeAttribute(key, value, false);
		}

		/// <summary>
		/// Adds a new attribute to the tag.
		/// </summary>
		/// <param name="key">The key for the attribute.</param>
		/// <param name="value">The value of the attribute.</param>
		/// <param name="replaceExisting"><c>true</c> to replace an existing attribute if an attribute exists that has the
		///     specified key value, or <c>false</c> to leave the original attribute unchanged.</param>
		public void MergeAttribute(string key, string value, bool replaceExisting)
		{
			if (string.IsNullOrEmpty(key))
			{
				throw new ArgumentException(Resources.Argument_Cannot_Be_Null_Or_Empty, "key");
			}

			if (replaceExisting || !Attributes.ContainsKey(key))
			{
				Attributes[key] = value;
			}
		}

		/// <summary>
		/// Adds new attributes to the tag.
		/// </summary>
		/// <typeparam name="TKey">The type of the key object.</typeparam>
		/// <typeparam name="TValue">The type of the value object.</typeparam>
		/// <param name="attributes">The collection of attributes to add.</param>
		public void MergeAttributes<TKey, TValue>(IDictionary<TKey, TValue> attributes)
		{
			MergeAttributes(attributes, false);
		}

		/// <summary>
		/// Adds new attributes to the tag.
		/// </summary>
		/// <typeparam name="TKey">The type of the key object.</typeparam>
		/// <typeparam name="TValue">The type of the value object.</typeparam>
		/// <param name="attributes">The collection of attributes to add.</param>
		/// <param name="replaceExisting"><c>true</c> to replace an existing attribute if an attribute exists that has the
		///     specified key value, or <c>false</c> to leave the original attribute unchanged.</param>
		public void MergeAttributes<TKey, TValue>(IDictionary<TKey, TValue> attributes, bool replaceExisting)
		{
			if (attributes != null)
			{
				foreach (var entry in attributes)
				{
					string key = Convert.ToString(entry.Key, CultureInfo.InvariantCulture);
					string value = Convert.ToString(entry.Value, CultureInfo.InvariantCulture);
					MergeAttribute(key, value, replaceExisting);
				}
			}
		}

		/// <summary>
		/// Sets the System.Web.Mvc.TagBuilder.InnerHtml property of the element to an HTML-encoded version of the specified string.
		/// </summary>
		/// <param name="innerText">The string to HTML-encode.</param>
		public void SetInnerText(string innerText)
		{
			InnerHtml = HttpUtility.HtmlEncode(innerText);
		}

		/// <summary>
		/// Converts this tag to its HTML string representation.
		/// </summary>
		/// <param name="renderMode">The render mode.</param>
		public IHtmlString ToHtmlString(TagRenderMode renderMode)
		{
			return new HtmlString(ToString(renderMode));
		}

		/// <summary>
		/// Renders the element as a <see cref="System.Web.Mvc.TagRenderMode.Normal"/> element.
		/// </summary>
		/// <returns>
		/// The rendered HTML tag.
		/// </returns>
		public override string ToString()
		{
			return ToString(TagRenderMode.Normal);
		}

		/// <summary>
		/// Renders the HTML tag by using the specified render mode.
		/// </summary>
		/// <param name="renderMode">The render mode.</param>
		/// <returns>
		/// The rendered HTML tag.
		/// </returns>
		public string ToString(TagRenderMode renderMode)
		{
			var sb = new StringBuilder();
			switch (renderMode)
			{
				case TagRenderMode.StartTag:
					sb.Append('<')
						.Append(TagName);
					AppendAttributes(sb);
					sb.Append('>');
					break;

				case TagRenderMode.EndTag:
					sb.Append("</")
						.Append(TagName)
						.Append('>');
					break;

				case TagRenderMode.SelfClosing:
					sb.Append('<')
						.Append(TagName);
					AppendAttributes(sb);
					sb.Append(" />");
					break;

				default:
					sb.Append('<')
						.Append(TagName);
					AppendAttributes(sb);
					sb.Append('>')
						.Append(InnerHtml)
						.Append("</")
						.Append(TagName)
						.Append('>');
					break;
			}
			return sb.ToString();
		}

		private void AppendAttributes(StringBuilder sb)
		{
			foreach (var attribute in Attributes)
			{
				string key = attribute.Key;
				if (string.Equals(key, "id", StringComparison.Ordinal /* case-sensitive */) && string.IsNullOrEmpty(attribute.Value))
				{
					continue; // DevDiv Bugs #227595: don't output empty IDs
				}
				string value = HttpUtility.HtmlAttributeEncode(attribute.Value);
				sb.Append(' ')
					.Append(key);
				if (!string.IsNullOrEmpty(value))
				{
					sb.Append("=")
						.Append('"')
						.Append(value)
						.Append('"');
				}
			}
		}
	}
}