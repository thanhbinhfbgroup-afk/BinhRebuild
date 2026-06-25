namespace Binh.Core.Reward
{
    public readonly struct RewardBundle
    {
        public float Gold { get; }
        public float Experience { get; }
        public RewardBundle(float gold, float experience)
        {
            Gold = gold;
            Experience = experience;
        }
    }
}