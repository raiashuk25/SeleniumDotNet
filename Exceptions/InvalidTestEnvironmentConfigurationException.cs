using NUnit.Framework;
using System;
using System.Runtime.Serialization;

namespace SeleniumDotNetCoreSample
{
    [Serializable]
    internal class InvalidTestEnvironmentConfigurationException : Exception
    {
        public InvalidTestEnvironmentConfigurationException()
        {
        }

        public InvalidTestEnvironmentConfigurationException(string message) : base(message)
        {
        }

        public InvalidTestEnvironmentConfigurationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidTestEnvironmentConfigurationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}