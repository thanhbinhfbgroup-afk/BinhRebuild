using System;
using Binh.Core.Combat;
using Binh.Core.Rewards;
using Binh.Core.ValueObjects;
using Binh.Modules.Player.Domain;

namespace Binh.Modules.Player.Application
{
    public sealed class PlayerApplication : IDamageReceiver
    {
        private readonly BinhEntityId _entityId;
        private readonly PlayerState _state;
        private readonly PlayerDefinition _definition;
        public bool IsDead => _state.IsDead;
        public event Action<BinhEntityId, RewardBundle> Died;
        public PlayerApplication(BinhEntityId entityId, PlayerState state, PlayerDefinition definition)
        {
            if (!entityId.IsValid)
            {
                throw new ArgumentException("PlayerApplication requires an entity id", nameof(entityId));
            }
            _entityId = entityId;
            _state = state ?? throw new ArgumentNullException((nameof(state)));
            _definition = definition ?? throw new ArgumentNullException(nameof(definition));
        }
        public void ComputeMoveVelocity(float inputX, float inputY, out float velocityX, out float velocityY)
        {
            var magnitudeSquared = (inputX * inputX) + (inputY * inputY);
            if (magnitudeSquared > 1f)
            {
                var magnitude = MathF.Sqrt(magnitudeSquared);
                inputX /= magnitude;
                inputY /= magnitude;
            }
            velocityX = inputX * _definition.MoveSpeed;
            velocityY = inputY * _definition.MoveSpeed;
            _state.SetMoveVelocity(velocityX, velocityY);
        }
        public PlayerReadModel GetReadModel()
        {
            return new PlayerReadModel(
                _state.CurrentHealth,
                _state.MaxHealth,
                _state.MoveVelocityX,
                _state.MoveVelocityY,
                _state.IsDead,
                _state.IsMoving);
        }
        public DamageResult ReceiveDamage(DamageInfo damageInfo)
        {
            var requestDamage = damageInfo.Amount;
            if (requestDamage < 0f)
            {
                requestDamage = 0f;
            }
            var wasAlive = !_state.IsDead;
            var appliedDamage = 0f;
            if (wasAlive)
            {
                if (appliedDamage > _state.CurrentHealth)
                {
                    appliedDamage = _state.CurrentHealth;
                }
                var nextHealth = _state.CurrentHealth - appliedDamage;
                _state.SetCurrentHealth(nextHealth);
            }
            var justDied = wasAlive && !_state.IsDead;
            if (justDied)
            {
                Died?.Invoke(_entityId, new RewardBundle(0, 0));
            }
            return new DamageResult(appliedDamage, _state.CurrentHealth, justDied);
        }

    }
}
