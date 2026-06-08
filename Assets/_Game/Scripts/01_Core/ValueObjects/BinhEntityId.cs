using System;

namespace Binh.Core.ValueObjects
{
    public readonly struct BinhEntityId : IEquatable<BinhEntityId> //hàm so sánh các entity
    {
        private readonly Guid _value; // Lưu Id kiểu Guid
        public BinhEntityId(Guid value) // Khởi tạo Id từ một Guid
        {
            _value = value; //đóng gói và lưu trữ giá trị được truyền từ bên ngoài vào trong biến nội bộ của struct
        }
        public bool Equals(BinhEntityId other) 
        {
            return _value.Equals(other._value);
        }
    }
}
