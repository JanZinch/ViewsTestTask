namespace Purchases
{
    public class LevelCondition
    {
        public int RequiredLevel { get; private set; }

        public LevelCondition(int requiredLevel)
        {
            RequiredLevel = requiredLevel;
        }
    }
}