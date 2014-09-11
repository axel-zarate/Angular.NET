using System;
using System.ComponentModel.DataAnnotations;

namespace AxSoft.Angular.Net.Sample.Models.Validations
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
	public class DoubleAttribute : ValidationAttribute
	{
		private double? _maxValueInternal;
		private double? _minValueInternal;

		public double MaxValue
		{
			get { return _maxValueInternal.GetValueOrDefault(double.MaxValue); }
			set
			{
				if (value >= MinValue)
				{
					_maxValueInternal = value;
				}
			}
		}

		public double MinValue
		{
			get { return _minValueInternal.GetValueOrDefault(double.MinValue); }
			set
			{
				if (value <= MaxValue)
				{
					_minValueInternal = value;
				}
			}
		}

		public double? GetMaxValue()
		{
			return _maxValueInternal;
		}

		public double? GetMinValue()
		{
			return _minValueInternal;
		}

		public override bool IsValid(object value)
		{
			if (value is double)
			{
				double doubleValue = (double)value;

				return MinValue <= doubleValue && doubleValue <= MaxValue;
			}

			return false;
		}
	}
}