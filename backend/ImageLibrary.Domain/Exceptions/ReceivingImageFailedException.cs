using System;
using System.Runtime.Serialization;

namespace ImageLibrary.Domain.Exceptions
{
    [Serializable]
    public class ReceivingImageFailedException : Exception
    {
        public ReceivingImageFailedException()
        {
        }

        public ReceivingImageFailedException(string message) : base(message)
        {
        }

        protected ReceivingImageFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ReceivingImageFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}