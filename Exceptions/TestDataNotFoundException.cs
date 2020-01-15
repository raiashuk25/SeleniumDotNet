using NUnit.Framework;
using System;
using System.Runtime.Serialization;

namespace SeleniumDotNetCoreSample
{
    [Serializable]
    internal class TestDataNotFoundException : Exception
    {
        public TestDataNotFoundException()
        {
        }

        public TestDataNotFoundException(string message) : base(message)
        {
        }

        public TestDataNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TestDataNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
