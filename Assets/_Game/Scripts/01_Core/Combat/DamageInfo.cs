using System.Runtime;
using Binh.Core.ValueObjects;

namespace Binh.Core.Combat
{
    public readonly struct DamageInfo
    {
        public float Amount { get; }
        public BinhEntityId SourceId { get; }

        public DamageInfo(float amount, BinhEntityId sourceId)
        {
            Amount = amount;
            SourceId = sourceId;
        }
    }
}