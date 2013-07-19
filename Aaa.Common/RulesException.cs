namespace Aaa.Common
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Exception for passing business exceptions
    /// </summary>
    [Serializable]
    public class RulesException : Exception
    {
        public RulesException(IEnumerable<ErrorInfo> errors)
        {
            Errors = errors;
        }

        public RulesException(string propertyName, string errorMessage)
            : this(propertyName, errorMessage, null) { }

        public RulesException(string propertyName, string errorMessage, object onObject)
        {
            Errors = new[] { new ErrorInfo(propertyName, errorMessage, onObject) };
        }

        public IEnumerable<ErrorInfo> Errors { get; private set; }

    }

    /// <summary>
    /// Exception for when a record can't be found
    /// </summary>
    [Serializable]
    public class NotFoundException : Exception
    {
        public NotFoundException() : base("The record was not found.") { }
        public NotFoundException(string message) : base(message) { }
        public NotFoundException(string message, Exception inner) : base(message, inner) { }
        protected NotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
