using System;


namespace Binh.Core.ValueObjects
{
    public readonly struct BinhEntityId : IEqualtable<BinhEntityId>
    {
        private readonly Guid _value;
        private BinhEntityId(Guid value) {_value = value;}
      
        public static BinhEntityId New() => new BinhEntityId(Guid.NewGuid());
  
        public static BinhEntityId Invalid => new BinhEntityId(Guid.Empty);

        public bool IsValid => _value != Guid.Empty;

        public bool Equals(BinhEntityId other) => _value.Equals(other._value);

        public override bool Equals(object obj) => obj is BinhEntityId other && Equals(other);

        public override int GetHashCode() => _value.GetHashCode();

        public override string ToString() => IsValid ? _value.ToString("N")[..8] : "Invalid";

        public static bool operator ==(BinhEntityId left, BinhEntityId right) => left.Equals(right);

        public static bool operator !=(BinhEntityId left, BinhEntityId right) => !left.Equals(right);
      
    }

}