using System;
using Binh.Core.Combat;
using Binh.Core.Rewards;
using Binh.Core.ValueObjects;
using Binh.Modules.Player.Domain;
using Mono.Cecil;
using UnityEditorInternal;

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
        public DamageResult Attack(IDamageReceiver target, float currentTime)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }
            if (_state.IsDead || currentTime < _state.NextAttackTime || _definition.AttackDamage <= 0)
            {
                return new DamageResult(0f, _state.CurrentHealth, false);
            }
            var damageInfo = new DamageInfo(_definition.AttackDamage, _entityId);
            var result = target.ReceiveDamage(damageInfo);
            _state.SetNextAttackTime(currentTime + _definition.AttackCooldown);
            return result;
        }
        public DamageResult ReceiveDamage(DamageInfo damageInfo)
        {
            if (_state.IsDead)
            {
                return new DamageResult(0f, _state.CurrentHealth, false);
            }
            var requestedDamage = damageInfo.Amount;
            if (requestedDamage < 0f)
            {
                requestedDamage = 0f;
            }
            var appliedDamage = requestedDamage;

            if (appliedDamage > _state.CurrentHealth)
            {
                appliedDamage = _state.CurrentHealth;
            }
            var nextHealth = _state.CurrentHealth - appliedDamage;
            _state.SetCurrentHealth(nextHealth);

            var justDied = !_state.IsDead;
            if (justDied)
            {
                Died?.Invoke(_entityId, new RewardBundle(0, 0));
            }
            return new DamageResult(appliedDamage, _state.CurrentHealth, justDied);
        }

    }
}
