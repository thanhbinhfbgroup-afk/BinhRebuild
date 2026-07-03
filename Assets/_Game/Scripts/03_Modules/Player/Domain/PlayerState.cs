using System;
using Binh.Core.Combat;

namespace Binh.Modules.Player.Domain
{
    public sealed class PlayerState
    {
        public float CurrentHealth { get; private set; }
        public float MaxHealth { get; private set; }
        public float MoveVelocityX { get; private set; }
        public float MoveVelocityY { get; private set; }
        public float NextAttackTime { get; private set; }
        public WeaponDefinition CurrentWeapon { get; private set; }
        public int MaxWeaponDurability => CurrentWeapon.MaxDurability;
        public int CurrentWeaponDurability { get; private set; }

        public bool IsDead => CurrentHealth <= 0f;
        public bool IsMoving => !IsDead && (MoveVelocityX * MoveVelocityX + MoveVelocityY * MoveVelocityY) > 0f;
        public PlayerState(PlayerDefinition definition)
        {
            if (definition == null)
            {
                throw new ArgumentNullException(nameof(definition));
            }

            MaxHealth = definition.MaxHealth;
            CurrentHealth = definition.MaxHealth;
            NextAttackTime = 0f;
            CurrentWeapon = definition.StartWeapon;
            CurrentWeaponDurability = CurrentWeapon.MaxDurability;
        }
        public void SetMoveVelocity(float velocityX, float velocityY)
        {
            if (IsDead)
            {
                MoveVelocityX = 0f;
                MoveVelocityY = 0f;
                return;
            }
            MoveVelocityX = velocityX;
            MoveVelocityY = velocityY;
        }
        public void SetCurrentHealth(float currentHealth)
        {
            if (IsDead) return;
            CurrentHealth = Math.Clamp(currentHealth, 0f, MaxHealth);
            if (IsDead)
            {
                MoveVelocityX = 0f;
                MoveVelocityY = 0f;
            }
        }
        public void SetNextAttackTime(float nextAttackTime)
        {
            if (nextAttackTime < 0f)
            {
                throw new ArgumentOutOfRangeException(nameof(nextAttackTime), "PlayerState requires NextAttackTime >= 0f.");
            }
            NextAttackTime = nextAttackTime;
        }
        public void ConsumeWeaponDurability(int amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "PlayerState requires ConsumeWeaponDurability amount > 0.");
            }
            CurrentWeaponDurability = Math.Clamp(CurrentWeaponDurability - amount, 0, MaxWeaponDurability);
        }
    }
}
