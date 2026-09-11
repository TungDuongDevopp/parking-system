using System.Net;

namespace ParkingSystem.Exceptions
{
    /// <summary>
    /// Cho phép các exception tự khai báo mã HTTP status code mong muốn khi được xử lý bởi ExceptionFilter.
    /// </summary>
    public interface IHasHttpStatusCode
    {
        HttpStatusCode StatusCode { get; }
    }
}
