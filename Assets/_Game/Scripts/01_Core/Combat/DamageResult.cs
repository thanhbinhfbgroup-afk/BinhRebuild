namespace Binh.Core.Combat
{
    public readonly struct DamageResult
    {
        public float AppliedDamage { get; }
        public float RemainingHealth { get; }
        public bool JustDied { get; }
        public DamageResult(float appliedDamage, float remainingHealth, bool justDied)
        {
            AppliedDamage = appliedDamage;
            RemainingHealth = remainingHealth;
            JustDied = justDied;
        }
    }
}