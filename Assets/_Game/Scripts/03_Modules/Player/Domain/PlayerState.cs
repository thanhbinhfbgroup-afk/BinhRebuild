using System;

namespace Binh.Modules.Player.Domain
{
    public sealed class PlayerState
    {
        public float CurrentHealth { get; private set; }
        public float MaxHealth { get; private set; }
        public float MoveVelocityX { get; private set; }
        public float MoveVelocityY { get; private set; }
        public bool IsDead => CurrentHealth <= 0f;
        public bool IsMoving => !IsDead && (MoveVelocityX * MoveVelocityX + MoveVelocityY * MoveVelocityY) > 0f;
        public PlayerState(float initialMaxHealth)
        {
            if (initialMaxHealth <= 0f)
            {
                throw new ArgumentOutOfRangeException(nameof(initialMaxHealth), "PlayerState requires initialMaxHealth > 0f.");
            }
            MaxHealth = initialMaxHealth;
            CurrentHealth = initialMaxHealth;
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
        public void Revive(float healthAfterRevive)
        {
            if (!IsDead)
            {
                return;
            }
            if (healthAfterRevive <= 0f || healthAfterRevive > MaxHealth)
            {
                throw new ArgumentOutOfRangeException(nameof(healthAfterRevive));
            }
            CurrentHealth = healthAfterRevive;
        }
    }
}
