using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS.Web.Angular.Validation
{
	public class NgDirectiveAttribute : Attribute
	{
		//private readonly LocalizableString _name = new LocalizableString("Name");
		private readonly LocalizableString _value = new LocalizableString("Value");
		private Type _resourceType;

		public string Name { get; set; }

		public string Value
		{
			get
			{
				return _value.Value;
			}
			set { _value.Value = value; }
		}

		public Type ResourceType
		{
			get { return _resourceType; }
			set
			{
				if (_resourceType != value)
				{
					_resourceType = value;
					//_name.ResourceType = value;
					_value.ResourceType = value;
				}
			}
		}

		public NgDirectiveAttribute(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}

			Name = name;
		}
	}
}
