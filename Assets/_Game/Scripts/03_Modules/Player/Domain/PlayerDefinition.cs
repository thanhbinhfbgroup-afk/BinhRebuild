using System;

namespace Binh.Modules.Player.Domain
{
    public sealed class PlayerDefinition
    {
        public float MaxHealth { get; }
        public float MoveSpeed { get; }
        public float AttackDamage { get; }
        public float AttackCooldown { get; }

        public PlayerDefinition(float maxHealth, float moveSpeed, float attackDamage, float attackCooldown)
        {
            if (maxHealth <= 0f)
            {
                throw new ArgumentOutOfRangeException(nameof(maxHealth), "PlayerDefinition requires MaxHealth > 0f.");
            }
            if (moveSpeed <= 0f)
            {
                throw new ArgumentOutOfRangeException(nameof(moveSpeed), "PlayerDefinition requires MoveSpeed > 0f.");
            }
            if (attackDamage < 0f)
            {
                throw new ArgumentOutOfRangeException(nameof(attackDamage), "PlayerDefinition requires AttackDamage >= 0f.");
            }
            if (attackCooldown < 0f)
            {
                throw new ArgumentOutOfRangeException(nameof(attackCooldown), "PlayerDefinition requires AttackCooldown >= 0f.");
            }
            MaxHealth = maxHealth;
            MoveSpeed = moveSpeed;
            AttackDamage = attackDamage;
            AttackCooldown = attackCooldown;
        }


    }
}