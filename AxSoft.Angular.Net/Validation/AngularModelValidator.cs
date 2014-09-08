using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace AxSoft.Angular.Net.Validation
{
	/// <summary>
	/// Provides a base class for implementing validation logic.
	/// </summary>
	public abstract class AngularModelValidator
	{
		/// <summary>
		/// Called from constructors in derived classes to initialize the <see cref="AngularModelValidator"/> class.
		/// </summary>
		/// <param name="metadata">The model metadata.</param>
		/// <param name="context">The AngularJS binding context.</param>
		/// <exception cref="System.ArgumentNullException">
		/// metadata is <c>null</c>
		/// or
		/// context is <c>null</c>
		/// </exception>
		protected AngularModelValidator(ModelMetadata metadata, AngularBindingContext context)
		{
			if (metadata == null)
			{
				throw new ArgumentNullException("metadata");
			}
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}

			Metadata = metadata;
			Context = context;
		}

		/// <summary>
		/// Gets or sets a value that indicates whether a model property is required
		/// </summary>
		/// <value>
		/// <c>true</c> if the model property is required; otherwise, <c>false</c>.
		/// </value>
		public virtual bool IsRequired
		{
			get { return false; }
		}

		/// <summary>
		/// Gets the AngularJS binding context.
		/// </summary>
		protected internal AngularBindingContext Context { get; private set; }

		/// <summary>
		/// Gets the metadata for the model validator.
		/// </summary>
		protected internal ModelMetadata Metadata { get; private set; }

		/// <summary>
		/// When implemented in a derived class, returns metadata for client validation.
		/// </summary>
		public abstract IEnumerable<ValidationRule> GetClientValidationRules();
	}
}