using NUnit.Framework;
using System;
using System.Runtime.Serialization;

namespace SeleniumDotNetCoreSample
{
    [Serializable]
    internal class DropDownSelectionValueNotFoundException : Exception
    {
        public DropDownSelectionValueNotFoundException()
        {
        }

        public DropDownSelectionValueNotFoundException(string message) : base(message)
        {
        }

        public DropDownSelectionValueNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DropDownSelectionValueNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
