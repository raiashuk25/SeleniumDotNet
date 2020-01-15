using NUnit.Framework;
using System;
using System.Runtime.Serialization;

namespace SeleniumDotNetCoreSample
{
    [Serializable]
    internal class InvalidBrowserTypeException : Exception
    {
        public InvalidBrowserTypeException()
        {
        }

        public InvalidBrowserTypeException(string message) : base(message)
        {
        }

        public InvalidBrowserTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidBrowserTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
