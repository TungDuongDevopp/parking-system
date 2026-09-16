

using Abp.UI;
using System;
using System.Net;

namespace ParkingSystem.Exceptions;

public class BusinessRuleException : UserFriendlyException, IHasHttpStatusCode
{
    public HttpStatusCode StatusCode => HttpStatusCode.Conflict;

    public BusinessRuleException(string message)
        : base(message)
    {
    }

    public BusinessRuleException(string message, string details)
        : base(message, details)
    {
    }

    public BusinessRuleException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
