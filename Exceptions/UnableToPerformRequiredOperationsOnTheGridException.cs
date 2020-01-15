using NUnit.Framework;
using System;
using System.Runtime.Serialization;

namespace SeleniumDotNetCoreSample
{
    [Serializable]
    internal class UnableToPerformRequiredOperationsOnTheGridException : Exception
    {
        public UnableToPerformRequiredOperationsOnTheGridException()
        {
        }

        public UnableToPerformRequiredOperationsOnTheGridException(string message) : base(message)
        {
        }

        public UnableToPerformRequiredOperationsOnTheGridException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnableToPerformRequiredOperationsOnTheGridException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
