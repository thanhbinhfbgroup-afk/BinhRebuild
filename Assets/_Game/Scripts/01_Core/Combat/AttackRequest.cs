using System;
using Binh.Core.ValueObjects;

namespace Binh.Core.Combat
{
    public readonly struct AttackRequest
    {
        public AttackRequest(BinhEntityId attackerId, AttackDefinition attack, float startedAtTime)
        {
            if (!attackerId.IsValid)
            {
                throw new ArgumentException(nameof(attackerId), "AttackRequest requires a valid attacker id.");
            }

            if (attack.Damage <= 0f)
            {
                throw new ArgumentException(nameof(attack), "AttackRequest requires a valid attack definition.");
            }

            if (startedAtTime < 0f)
            {
                throw new ArgumentOutOfRangeException(nameof(startedAtTime), "AttackRequest requires startedAtTime >= 0.");
            }
            AttackerId = attackerId;
            Attack = attack;
            StartedAtTime = startedAtTime;
        }
        public BinhEntityId AttackerId { get; }
        public AttackDefinition Attack { get; }
        public float StartedAtTime { get; }
        public float HitTime => StartedAtTime + Attack.HitDelay;
    }
}
