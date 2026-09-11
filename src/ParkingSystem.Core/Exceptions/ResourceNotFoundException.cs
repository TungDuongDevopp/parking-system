using System;
using System.Net;
using Abp.Domain.Entities;

namespace ParkingSystem.Exceptions
{
    public class ResourceNotFoundException : EntityNotFoundException, IHasHttpStatusCode
    {
        public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

        public ResourceNotFoundException(string message)
            : base(message)
        {
        }

        public ResourceNotFoundException(Type entityType, object id)
            : base(entityType, id)
        {
        }

        public ResourceNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
