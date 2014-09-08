using System;

namespace AxSoft.Angular.Net
{
	/// <summary>
	/// Represents an HTML attribute and its value.
	/// </summary>
	public class AttributeValuePair
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AttributeValuePair"/> class.
		/// </summary>
		/// <param name="name">The attribute name.</param>
		/// <param name="value">The attribute value (optional).</param>
		/// <exception cref="System.ArgumentNullException">name is null or empty.</exception>
		public AttributeValuePair(string name, object value)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}

			// TODO: Validate attribute name?

			Name = name;
			Value = value;
		}

		/// <summary>
		/// Gets the name for the attribute.
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// Gets or sets the value of the attribute.
		/// </summary>
		public object Value { get; set; }
	}
}