using Binh.Core.Combat;
using Binh.Modules.Player.Domain;
using UnityEngine;

namespace Binh.Modules.Player.Infrastructure.Config
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Binh/Player/PlayerConfig")]
    public sealed class PlayerConfig : ScriptableObject
    {
        [field: SerializeField, Min(0.01f)]
        public float MaxHealth { get; private set; } = 10f;

        [field: SerializeField, Min(0.01f)]
        public float MoveSpeed { get; private set; } = 5f;

        [field: SerializeField, Min(0.01f)]
        public float AttackDamage { get; private set; } = 1f;

        [field: SerializeField, Min(0f)]
        public float AttackCoolDown { get; private set; } = 0.2f;

        [field: SerializeField, Min(0f)]
        public float AttackRange { get; private set; } = 1f;

        [field: SerializeField, Min(0f)]
        public float AttackHitDelay { get; private set; } = 0.1f;

        [field: SerializeField, Min(0f)]
        public int AttackMaxTargetCount { get; private set; } = 1;

        [field: SerializeField, Range(0, 180)]
        public int AttackConeAngleDegrees { get; private set; } = 90;

        [field: SerializeField]
        public AttackHitShape AttackShape { get; private set; } = AttackHitShape.Cone;

        [field: SerializeField, Min(0f)]
        public int MaxWeaponDurability { get; private set; } = 60;

        public PlayerDefinition ToDefinition()
        {
            return new PlayerDefinition(
                MaxHealth,
                MoveSpeed,
                new WeaponDefinition(
                    new AttackDefinition(
                        AttackDamage,
                        AttackCoolDown,
                        AttackRange,
                        AttackHitDelay,
                        AttackShape,
                        AttackMaxTargetCount,
                        AttackConeAngleDegrees),
                    MaxWeaponDurability));
        }
    }
}