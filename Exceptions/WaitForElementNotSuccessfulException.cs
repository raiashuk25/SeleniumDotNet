using NUnit.Framework;
using System;
using System.Runtime.Serialization;

namespace SeleniumDotNetCoreSample
{
    [Serializable]
    public class WaitForElementNotSuccessfulException : Exception
    {
        public WaitForElementNotSuccessfulException()
        {
        }

        public WaitForElementNotSuccessfulException(string message) : base(message)
        {
        }

        public WaitForElementNotSuccessfulException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WaitForElementNotSuccessfulException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
