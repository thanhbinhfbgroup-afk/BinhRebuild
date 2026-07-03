using System;
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
            if (amount <= 0f)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "DamageInfo requires Amount > 0f.");
            }
            if (!sourceId.IsValid)
            {
                throw new ArgumentException(nameof(sourceId), "DamageInfo requires a valid SourceId.");
            }
            Amount = amount;
            SourceId = sourceId;
        }
    }
}