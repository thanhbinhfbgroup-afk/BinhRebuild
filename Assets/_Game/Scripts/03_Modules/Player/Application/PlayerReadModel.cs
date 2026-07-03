
namespace Binh.Modules.Player.Application
{
    public readonly struct PlayerReadModel
    {
        public float MaxHealth { get; }
        public float CurrentHealth { get; }
        public float MoveVelocityX { get; }
        public float MoveVelocityY { get; }
        public int CurrentWeaponDurability { get; }
        public int MaxWeaponDurability { get; }
        public bool IsDead { get; }
        public bool IsMoving { get; }
        public PlayerReadModel(float currentHealth, float maxHealth, float moveVelocityX,
        float moveVelocityY, int currentWeaponDurability, int maxWeaponDurability, bool isDead, bool isMoving)
        {
            MaxHealth = maxHealth;
            CurrentHealth = currentHealth;
            MoveVelocityX = moveVelocityX;
            MoveVelocityY = moveVelocityY;
            CurrentWeaponDurability = currentWeaponDurability;
            MaxWeaponDurability = maxWeaponDurability;
            IsDead = isDead;
            IsMoving = isMoving;
        }
    }
}