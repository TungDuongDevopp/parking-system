

using Abp.UI;
using System;
using System.Net;

namespace ParkingSystem.Exceptions;

public class UserNotInRoleException : UserFriendlyException, IHasHttpStatusCode
{
    public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

    public UserNotInRoleException(string message)
        : base(message)
    {
    }

    public UserNotInRoleException(string message, string details)
        : base(message, details)
    {
    }

    public UserNotInRoleException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
