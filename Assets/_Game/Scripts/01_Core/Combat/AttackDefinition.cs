using System;

namespace Binh.Core.Combat
{
    public enum AttackHitShape
    {
        Cone = 0,
        Circle = 1,
        Raycast = 2
    }
    public readonly struct AttackDefinition
    {
        public float Damage { get; }
        public float Range { get; }
        public float Cooldown { get; }
        public float HitDelay { get; }
        public int MaxTargetCount { get; }
        public int ConeAngleDegrees { get; }
        public AttackHitShape HitShape { get; }


        public AttackDefinition(float damage, float range, float cooldown, float hitDelay, AttackHitShape hitShape, int maxTargetCount, int coneAngleDegrees)
        {
            if (damage <= 0f)
            {
                throw new ArgumentOutOfRangeException(nameof(damage), "AttackDefinition requires Damage > 0f.");
            }
            if (range <= 0f)
            {
                throw new ArgumentOutOfRangeException(nameof(range), "AttackDefinition requires Range > 0f.");
            }
            if (cooldown < 0f)
            {
                throw new ArgumentOutOfRangeException(nameof(cooldown), "AttackDefinition requires Cooldown >= 0f.");
            }
            if (hitDelay < 0f)
            {
                throw new ArgumentOutOfRangeException(nameof(hitDelay), "AttackDefinition requires HitDelay >= 0f.");
            }
            if (maxTargetCount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maxTargetCount), "AttackDefinition requires MaxTargetCount > 0.");
            }
            if (hitShape == AttackHitShape.Cone && (coneAngleDegrees <= 0 || coneAngleDegrees > 360))
            {
                throw new ArgumentOutOfRangeException(nameof(coneAngleDegrees), "AttackDefinition requires 0 < coneAngleDegrees <= 360 for cone attacks.");
            }

            Damage = damage;
            Range = range;
            Cooldown = cooldown;
            HitDelay = hitDelay;
            HitShape = hitShape;
            MaxTargetCount = maxTargetCount;
            ConeAngleDegrees = hitShape == AttackHitShape.Cone ? coneAngleDegrees : 0;
        }
    }
}