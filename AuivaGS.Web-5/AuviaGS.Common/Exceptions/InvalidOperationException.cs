using System;
using AuviaGS.Common;

namespace ExceptionsMid.Exceptions
{
    public class InvalidOperationException : AuviaGSException
    {
        public InvalidOperationException() : base("Service Validation Exception")
        {
        }


        public InvalidOperationException(string message) : base(message)
        {
        }

        public InvalidOperationException(int code, string message) : base(code, message)
        {
        } 

        public InvalidOperationException(string message, Exception ex) : base(message, ex)
        {
        }

        public InvalidOperationException(int code, string message, Exception ex) : base(code, message, ex)
        {
        }
    }
}
