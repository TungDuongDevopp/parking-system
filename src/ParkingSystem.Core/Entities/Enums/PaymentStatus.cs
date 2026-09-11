namespace ParkingSystem.Entities.Enums;

public enum PaymentStatus
{
    Pending = 0,
    PartiallyPaid = 1,
    Paid = 2,
    Expired = 3,
    Refunding = 4,
    Refunded = 5,
    Failed = 6
}
