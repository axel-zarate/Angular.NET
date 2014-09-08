using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS.Web.Angular.Validation
{
	public class NgDirectiveAttributeAdapterProvider : AngularModelValidatorProvider
	{
		public override IEnumerable<AngularModelValidator> GetValidators(System.Web.Mvc.ModelMetadata metadata)
		{
			if (metadata.ContainerType != null)
			{
				var attrs = GetTypeDescriptor(metadata.ModelType).GetAttributes().OfType<NgDirectiveAttribute>();
				foreach (var attr in attrs)
				{
					yield return new NgDirectiveAttributeAdapter(metadata, attr);
				}
			}
		}
	}
}
