using NUnit.Framework;
using System;
using System.Runtime.Serialization;

namespace SeleniumDotNetCoreSample
{
    [Serializable]
    internal class TextValidationExpection : Exception
    {
        public TextValidationExpection()
        {
        }

        public TextValidationExpection(string message) : base(message)
        {
        }

        public TextValidationExpection(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TextValidationExpection(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}