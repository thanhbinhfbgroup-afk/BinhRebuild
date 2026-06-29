using Binh.Modules.Player.Domain;
using Codice.CM.Common.Merge;
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

        [field: SerializeField, Min(0f)]
        public float AttackDamage { get; private set; } = 1f;

        [field: SerializeField, Min(0f)]
        public float AttackCoolDown { get; private set; } = 0.2f;
        public PlayerDefinition ToDefinition()
        {
            return new PlayerDefinition(MaxHealth, MoveSpeed, AttackDamage, AttackCoolDown);
        }
    }
}