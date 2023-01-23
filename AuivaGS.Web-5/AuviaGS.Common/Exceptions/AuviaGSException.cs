using System;

namespace AuviaGS.Common
{
    public class AuviaGSException : Exception
    {
        public int ErrorCode { get; set; }

        public AuviaGSException() : base("AuviaGS Exception")
        {
        }

        public AuviaGSException(string message) : base(message)
        {
        }

        public AuviaGSException(int statusCode, string message) : base(message)
        {
            ErrorCode = statusCode;
        }

        public AuviaGSException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public AuviaGSException(int statusCode, string message, Exception innerException) : base(message, innerException)
        {
            ErrorCode = statusCode;
        }
    }
}
