

using System.Collections.Generic;

namespace ParkingSystem.Web.Models.Staff
{
    public class StaffUserLookupDto
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class StaffListViewModel
    {
        public IReadOnlyList<StaffUserLookupDto> AvailableUsers { get; set; } = new List<StaffUserLookupDto>();
        public bool IsManager { get; set; }
    }
}

