using System;

namespace UniLibrary.Helper
{
    public class MessageDuplicateException : Exception
    {
        public MessageDuplicateException()
        {
        }

        public MessageDuplicateException(string message) : base(message)
        {
        }

        public MessageDuplicateException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}