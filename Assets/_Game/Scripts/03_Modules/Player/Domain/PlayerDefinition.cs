using System;
using Binh.Core.Combat;

namespace Binh.Modules.Player.Domain
{
    public sealed class PlayerDefinition
    {
        public float MaxHealth { get; }
        public float MoveSpeed { get; }
        public WeaponDefinition StartWeapon { get; }

        public PlayerDefinition(float maxHealth, float moveSpeed, WeaponDefinition startWeapon)
        {
            if (maxHealth <= 0f)
            {
                throw new ArgumentOutOfRangeException(nameof(maxHealth), "PlayerDefinition requires MaxHealth > 0f.");
            }
            if (moveSpeed <= 0f)
            {
                throw new ArgumentOutOfRangeException(nameof(moveSpeed), "PlayerDefinition requires MoveSpeed > 0f.");
            }

            MaxHealth = maxHealth;
            MoveSpeed = moveSpeed;
            StartWeapon = startWeapon;

        }
    }
}