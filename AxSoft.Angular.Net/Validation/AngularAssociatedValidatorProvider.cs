using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace AxSoft.Angular.Net.Validation
{
	/// <summary>
	/// Provides an abstract class for classes that implement a validation provider.
	/// </summary>
	public abstract class AngularAssociatedValidatorProvider : AngularModelValidatorProvider
	{
		/// <summary>
		/// Gets a list of validators.
		/// </summary>
		/// <param name="metadata">The model metadata.</param>
		/// <param name="context">The AngularJS binding context.</param>
		/// <exception cref="System.ArgumentNullException">metadata</exception>
		public sealed override IEnumerable<AngularModelValidator> GetValidators(ModelMetadata metadata, AngularBindingContext context)
		{
			if (metadata == null)
			{
				throw new ArgumentNullException("metadata");
			}

			if (metadata.ContainerType != null && !String.IsNullOrEmpty(metadata.PropertyName))
			{
				return GetValidatorsForProperty(metadata, context);
			}

			return GetValidatorsForType(metadata, context);
		}

		/// <summary>
		/// Gets a type descriptor for the specified type.
		/// </summary>
		/// <param name="type">The type of the validation provider.</param>
		/// <returns>A type descriptor for the specified type.</returns>
		protected virtual ICustomTypeDescriptor GetTypeDescriptor(Type type)
		{
			return new AssociatedMetadataTypeTypeDescriptionProvider(type).GetTypeDescriptor(type);
		}

		/// <summary>
		/// Gets the validators for the model using the metadata, the controller context, and a list of attributes.
		/// </summary>
		/// <param name="metadata">The model metadata.</param>
		/// <param name="context">The AngularJS binding context.</param>
		/// <param name="attributes">The list of attributes.</param>
		/// <returns>The validators for the model.</returns>
		protected abstract IEnumerable<AngularModelValidator> GetValidators(ModelMetadata metadata, AngularBindingContext context, IEnumerable<Attribute> attributes);

		private IEnumerable<AngularModelValidator> GetValidatorsForProperty(ModelMetadata metadata, AngularBindingContext context)
		{
			ICustomTypeDescriptor typeDescriptor = GetTypeDescriptor(metadata.ContainerType);
			PropertyDescriptor property = typeDescriptor.GetProperties().Find(metadata.PropertyName, true);
			if (property == null)
			{
				throw new ArgumentException(
					string.Format(
						CultureInfo.CurrentCulture,
						Properties.Resources.Common_PropertyNotFound,
						metadata.ContainerType.FullName, metadata.PropertyName),
					"metadata");
			}
			return GetValidators(metadata, context, property.Attributes.OfType<Attribute>());
		}

		private IEnumerable<AngularModelValidator> GetValidatorsForType(ModelMetadata metadata, AngularBindingContext context)
		{
			return GetValidators(metadata, context, GetTypeDescriptor(metadata.ModelType).GetAttributes().OfType<Attribute>());
		}
	}
}