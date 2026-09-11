using System;
using System.Net;
using Abp.UI;

namespace ParkingSystem.Exceptions
{
    public class DuplicateResourceException : UserFriendlyException, IHasHttpStatusCode
    {
        public HttpStatusCode StatusCode => HttpStatusCode.Conflict;

        public DuplicateResourceException(string message)
            : base(message)
        {
        }

        public DuplicateResourceException(string message, string details)
            : base(message, details)
        {
        }

        public DuplicateResourceException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
