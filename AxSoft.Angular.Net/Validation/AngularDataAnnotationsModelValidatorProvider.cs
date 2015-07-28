using AxSoft.Angular.Net.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web.Mvc;
#if NET45
using DataAnnotationsCompareAttribute = System.ComponentModel.DataAnnotations.CompareAttribute;
#endif

namespace AxSoft.Angular.Net.Validation
{
	/// <summary>
	/// Represents a factory for validators based on <see cref="ValidationAttribute" />
	/// </summary>
	/// <param name="metadata">The model metadata.</param>
	/// <param name="context">The AngularJS binding context.</param>
	/// <param name="attribute">The attribute.</param>
	public delegate AngularModelValidator AngularDataAnnotationsModelValidationFactory(
		ModelMetadata metadata, AngularBindingContext context, ValidationAttribute attribute);

	// A factory for validators based on IValidatableObject
	//public delegate AngularModelValidator AngularDataAnnotationsValidatableObjectAdapterFactory(System.Web.Mvc.ModelMetadata metadata, AngularBindingContext context);

	/// <summary>
	/// An implementation of <see cref="AngularModelValidatorProvider"/> which providers validators
	/// for attributes which derive from <see cref="ValidationAttribute"/>. To support
	/// client side validation, you can register adapters through the static methods
	/// on this class.
	/// </summary>
	public class AngularDataAnnotationsModelValidatorProvider : AngularAssociatedValidatorProvider
	{
		internal static Dictionary<Type, AngularDataAnnotationsModelValidationFactory> AttributeFactories =
			BuildAttributeFactoriesDictionary();

		internal static AngularDataAnnotationsModelValidationFactory DefaultAttributeFactory =
			(metadata, context, attribute) => new AngularDataAnnotationsModelValidator(metadata, context, attribute);

		private static readonly ReaderWriterLockSlim AdaptersLock = new ReaderWriterLockSlim();

		static AngularDataAnnotationsModelValidatorProvider()
		{
			AddImplicitRequiredAttributeForValueTypes = false;
		}

		/// <summary>
		/// Gets or sets a value indicating whether non-nullable value types are required.
		/// </summary>
		/// <value>
		/// <c>true</c> if non-nullable value types are required; otherwise, <c>false</c>.
		/// </value>
		public static bool AddImplicitRequiredAttributeForValueTypes { get; set; }

		/// <summary>
		/// Registers an adapter to provide client-side validation.
		/// </summary>
		/// <param name="attributeType">The type of the validation attribute.</param>
		/// <param name="adapterType">The type of the adapter.</param>
		public static void RegisterAdapter(Type attributeType, Type adapterType)
		{
			ValidateAttributeType(attributeType);
			ValidateAttributeAdapterType(adapterType);
			ConstructorInfo constructor = GetAttributeAdapterConstructor(attributeType, adapterType);

			AdaptersLock.EnterWriteLock();

			try
			{
				AttributeFactories[attributeType] =
					(metadata, context, attribute) =>
						(AngularModelValidator)constructor.Invoke(new object[] { metadata, context, attribute });
			}
			finally
			{
				AdaptersLock.ExitWriteLock();
			}
		}

		/// <summary>
		/// Registers an adapter factory for the validation provider.
		/// </summary>
		/// <param name="attributeType">Type of the attribute.</param>
		/// <param name="factory">The factory that will be used to create the <see cref="AngularModelValidator"/> object for the specified attribute.</param>
		public static void RegisterAdapterFactory(Type attributeType, AngularDataAnnotationsModelValidationFactory factory)
		{
			ValidateAttributeType(attributeType);
			ValidateAttributeFactory(factory);

			AdaptersLock.EnterWriteLock();

			try
			{
				AttributeFactories[attributeType] = factory;
			}
			finally
			{
				AdaptersLock.ExitWriteLock();
			}
		}

		/// <summary>
		/// Registers the default adapter.
		/// </summary>
		/// <param name="adapterType">Type of the adapter.</param>
		public static void RegisterDefaultAdapter(Type adapterType)
		{
			ValidateAttributeAdapterType(adapterType);
			ConstructorInfo constructor = GetAttributeAdapterConstructor(typeof(ValidationAttribute), adapterType);

			DefaultAttributeFactory =
				(metadata, context, attribute) =>
					(AngularModelValidator)constructor.Invoke(new object[] { metadata, context, attribute });
		}

		/// <summary>
		/// Registers the default adapter factory.
		/// </summary>
		/// <param name="factory">The factory that will be used to create the <see cref="AngularModelValidator"/> object for the specified attribute.</param>
		public static void RegisterDefaultAdapterFactory(AngularDataAnnotationsModelValidationFactory factory)
		{
			ValidateAttributeFactory(factory);

			DefaultAttributeFactory = factory;
		}

		/// <summary>
		/// Gets a list of validators.
		/// </summary>
		/// <param name="metadata">The model metadata.</param>
		/// <param name="context">The AngularJS binding context.</param>
		/// <param name="attributes">The list of validation attributes.</param>
		/// <returns>A list of validators.</returns>
		protected override IEnumerable<AngularModelValidator> GetValidators(ModelMetadata metadata,
			AngularBindingContext context, IEnumerable<Attribute> attributes)
		{
			AdaptersLock.EnterReadLock();

			try
			{
				var results = new List<AngularModelValidator>();

				// Add an implied [Required] attribute for any non-nullable value type,
				// unless they've configured us not to do that.
				if (AddImplicitRequiredAttributeForValueTypes &&
					metadata.IsRequired &&
					!attributes.Any(a => a is RequiredAttribute))
				{
					attributes = attributes.Concat(new[] { new RequiredAttribute() });
				}

				// Produce a validator for each validation attribute we find
				foreach (ValidationAttribute attribute in attributes.OfType<ValidationAttribute>())
				{
					AngularDataAnnotationsModelValidationFactory factory;
					if (!AttributeFactories.TryGetValue(attribute.GetType(), out factory))
					{
						factory = DefaultAttributeFactory;
					}
					results.Add(factory(metadata, context, attribute));
				}

				return results;
			}
			finally
			{
				AdaptersLock.ExitReadLock();
			}
		}

		private static void AddDataTypeAttributeAdapter(
			Dictionary<Type, AngularDataAnnotationsModelValidationFactory> dictionary, Type attributeType, string ruleName)
		{
			AddValidationAttributeAdapter(
				dictionary,
				attributeType,
				(metadata, context, attribute) =>
					new DataTypeAttributeAdapter(metadata, context, (DataTypeAttribute)attribute, ruleName));
		}

		private static void AddValidationAttributeAdapter(
			Dictionary<Type, AngularDataAnnotationsModelValidationFactory> dictionary, Type validataionAttributeType,
			AngularDataAnnotationsModelValidationFactory factory)
		{
			if (validataionAttributeType != null)
			{
				dictionary.Add(validataionAttributeType, factory);
			}
		}

		private static Dictionary<Type, AngularDataAnnotationsModelValidationFactory> BuildAttributeFactoriesDictionary()
		{
			var dict = new Dictionary<Type, AngularDataAnnotationsModelValidationFactory>();

			AddValidationAttributeAdapter(dict, typeof(RangeAttribute),
				(metadata, context, attribute) => new RangeAttributeAdapter(metadata, context, (RangeAttribute)attribute));

			AddValidationAttributeAdapter(dict, typeof(RegularExpressionAttribute),
				(metadata, context, attribute) =>
					new RegularExpressionAttributeAdapter(metadata, context, (RegularExpressionAttribute)attribute));

			AddValidationAttributeAdapter(dict, typeof(RequiredAttribute),
				(metadata, context, attribute) => new RequiredAttributeAdapter(metadata, context, (RequiredAttribute)attribute));

			AddValidationAttributeAdapter(dict, typeof(StringLengthAttribute),
				(metadata, context, attribute) =>
					new StringLengthAttributeAdapter(metadata, context, (StringLengthAttribute)attribute));

#if NET45
			AddValidationAttributeAdapter(dict, typeof(DataAnnotationsCompareAttribute),
				(metadata, context, attribute) =>
					new CompareAttributeAdapter(metadata, context, (DataAnnotationsCompareAttribute)attribute));
			//AddDataTypeAttributeAdapter(dict, typeof(CreditCardAttribute), "creditcard");
			AddDataTypeAttributeAdapter(dict, typeof(EmailAddressAttribute), "email");
			//AddDataTypeAttributeAdapter(dict, typeof(PhoneAttribute), "phone");
			AddDataTypeAttributeAdapter(dict, typeof(UrlAttribute), "url");
#endif

			AddDataTypeAttributeAdapter(dict, typeof(DataTypeAttribute), null);

			return dict;
		}

		private static ConstructorInfo GetAttributeAdapterConstructor(Type attributeType, Type adapterType)
		{
			ConstructorInfo constructor =
				adapterType.GetConstructor(new[] { typeof(ModelMetadata), typeof(AngularBindingContext), attributeType });
			if (constructor == null)
			{
				throw new ArgumentException(
					string.Format(
						CultureInfo.CurrentCulture,
						Resources.DataAnnotationsModelValidatorProvider_ConstructorRequirements,
						adapterType.FullName,
						typeof(ModelMetadata).FullName,
						typeof(AngularBindingContext).FullName,
						attributeType.FullName),
					"adapterType");
			}

			return constructor;
		}

		private static void ValidateAttributeAdapterType(Type adapterType)
		{
			if (adapterType == null)
			{
				throw new ArgumentNullException("adapterType");
			}
			if (!typeof(AngularModelValidator).IsAssignableFrom(adapterType))
			{
				throw new ArgumentException(
					string.Format(
						CultureInfo.CurrentCulture,
						Resources.Common_TypeMustDriveFromType,
						adapterType.FullName,
						typeof(AngularModelValidator).FullName),
					"adapterType");
			}
		}

		private static void ValidateAttributeFactory(AngularDataAnnotationsModelValidationFactory factory)
		{
			if (factory == null)
			{
				throw new ArgumentNullException("factory");
			}
		}

		private static void ValidateAttributeType(Type attributeType)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			if (!typeof(ValidationAttribute).IsAssignableFrom(attributeType))
			{
				throw new ArgumentException(
					string.Format(
						CultureInfo.CurrentCulture,
						Resources.Common_TypeMustDriveFromType,
						attributeType.FullName,
						typeof(ValidationAttribute).FullName),
					"attributeType");
			}
		}
	}
}