using System;

namespace ParkingSystem.Web.Helper;

public class ApiErrorResponse
{
    public int Status { get; set; }

    public string Message { get; set; }

    public string Error { get; set; }

    public DateTime Timestamp { get; set; }

    public ApiErrorResponse(
        int status,
        string message,
        string error)
    {
        Status = status;
        Message = message;
        Error = error;
        Timestamp = DateTime.UtcNow;
    }
}