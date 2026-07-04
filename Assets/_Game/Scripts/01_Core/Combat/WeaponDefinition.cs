using System;

namespace Binh.Core.Combat
{
    public readonly struct WeaponDefinition
    {
        public AttackDefinition Attack { get; }
        public int MaxDurability { get; }

        public WeaponDefinition(AttackDefinition attack, int maxDurability)
        {
            if (attack.Damage <= 0f)
            {
                throw new ArgumentException("WeaponDefinition requires a valid attack definition.", nameof(attack));
            }
            if (maxDurability <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maxDurability), "WeaponDefinition requires MaxDurability > 0.");
            }
            Attack = attack;
            MaxDurability = maxDurability;
        }
    }
}