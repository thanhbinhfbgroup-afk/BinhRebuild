using System;

namespace Binh.Core.Combat
{
    public readonly struct WeaponDefinition
    {
        public AttackDefinition Attack { get; }
        public int MaxDurability { get; }

        public WeaponDefinition(AttackDefinition attackWeapon, int maxDurability)
        {
            if (attackWeapon.Damage <= 0f)
            {
                throw new ArgumentException("WeaponDefinition requires a valid attack definition.", nameof(attackWeapon));
            }
            if (maxDurability <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maxDurability), "WeaponDefinition requires MaxDurability > 0.");
            }
            Attack = attackWeapon;
            MaxDurability = maxDurability;
        }
    }
}