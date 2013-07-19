using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aaa.Common
{
    public class ErrorInfo
    {
        public string PropertyName { get; private set; }
        public string ErrorMessage { get; private set; }
        public object Object { get; private set; }

        public ErrorInfo(string propertyName, string errorMessage, object onObject)
        {
            this.PropertyName = propertyName;
            this.ErrorMessage = errorMessage;
            this.Object = onObject;
        }

        public ErrorInfo(string propertyName, string errorMessage)
            : this(propertyName, errorMessage, null) { }

        public override string ToString()
        {
            var str = this.ErrorMessage;
            if (!string.IsNullOrWhiteSpace(this.PropertyName))
                str = string.Format("Property '{0}' {1}", this.PropertyName, this.ErrorMessage);
            return str;
        }
    }
}