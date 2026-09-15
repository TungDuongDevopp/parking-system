

namespace ParkingSystem.Entities.Enums;

public enum ParkingSpotStatus
{
    Available = 1,   // Trống, có thể sử dụng/đặt
    Reserved = 2,    // Đã được đặt trước
    Occupied = 3,    // Đang có xe đỗ
    Unavailable = 4  // Không sử dụng: bảo trì, khóa...
}
