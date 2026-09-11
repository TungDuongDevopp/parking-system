using System;
using System.Net;
using Abp.UI;

namespace ParkingSystem.Exceptions
{
    public class CannotManipulateException : UserFriendlyException, IHasHttpStatusCode
    {
        public HttpStatusCode StatusCode => HttpStatusCode.Conflict;

        public CannotManipulateException(string message)
            : base(message)
        {
        }

        public CannotManipulateException(string message, string details)
            : base(message, details)
        {
        }

        public CannotManipulateException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
