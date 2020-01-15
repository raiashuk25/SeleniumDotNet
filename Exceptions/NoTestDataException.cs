using NUnit.Framework;
using System;
using System.Runtime.Serialization;

namespace SeleniumDotNetCoreSample
{
    [Serializable]
    class NoTestDataException : Exception
    {

        public NoTestDataException()
        {
        }

        public NoTestDataException(string message) : base(message)
        {
        }

        public NoTestDataException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoTestDataException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
