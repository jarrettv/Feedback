namespace Aaa.Common
{
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    [AttributeUsage(AttributeTargets.Property)]
    public class RangeListAttribute : ValidationAttribute
    {
        public int Low { get; set; }
        public int High { get; set; }
        public IList<int> theList { get; set; }
        public bool mustBePopulated = false;
        public string displayName;

        public RangeListAttribute(int low, int high, string displayName)
        {
            this.Low = low;
            this.High = high;
            this.displayName = displayName;
        }

        public RangeListAttribute(int low, int high, string displayName, bool mustBePopulated)
        {
            this.Low = low;
            this.High = high;
            this.displayName = displayName;
            this.mustBePopulated = mustBePopulated;
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                if (mustBePopulated)
                {
                    ErrorMessage = string.Format("The {0} must contain at least one entry.", displayName, Low, High);
                    return false;
                }
                else
                {
                    return true;
                }
            }
            ErrorMessage = string.Format("The {0} must be between {1} and {2} inclusive.", displayName, Low, High);
            theList = (IList<int>)value;
            bool valid = true;
            foreach (int i in theList)
            {
                valid &= (i <= High && i >= Low);
            }
            return valid;
        }
    }
}