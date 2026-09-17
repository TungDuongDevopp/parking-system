

using Abp.UI;
using System;
using System.Net;

namespace ParkingSystem.Exceptions;

public class InvalidSortingException : UserFriendlyException, IHasHttpStatusCode
{
    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

    public InvalidSortingException(string message)
        : base(message)
    {
    }

    public InvalidSortingException(string message, string details)
        : base(message, details)
    {
    }

    public InvalidSortingException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
