namespace FibDev
{
    public static class BaseballActions
    {
        public enum Action
        {
            Hit,
            Out
        };

        public static Action GenerateActionFromD100(int roll)
        {
            return roll > 50 ? Action.Hit : Action.Out;
        }
    }
}