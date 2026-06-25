using System.Runtime;
using Binh.Core.ValueObjects;

namespace Binh.Core.Combat
{
    public readonly struct DamageInfo
    {
        public float Amount { get; }
        public BinhEntityId SourceId { get; }
        public bool IsCritical { get; }
        public DamageInfo(float amount, BinhEntityId sourceId, bool isCritical)
        {
            Amount = amount;
            SourceId = sourceId;
            IsCritical = isCritical;
        }
    }
}