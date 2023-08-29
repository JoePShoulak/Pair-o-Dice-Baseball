using static FibDev.Baseball.BaseballPositions;

namespace FibDev.Baseball
{
    public static class BaseballAction
    {
        public enum ActionType
        {
            Throw,
            SwingHit,
            SwingMiss,
            Catch,
            Run,
            Walk,
            
            Cleanup
        };

        public struct Action
        {
            public ActionType aType;
            public Positions? aSubject;
            public Positions? aObject;
            public string description;

            public Action(Positions _aSubject, ActionType _aType, Positions _aObject)
            {
                aSubject = _aSubject;
                aType = _aType;
                aObject = _aObject;

                description = $"{aSubject} {aType} {aObject}";
            }

            public Action(ActionType _aType)
            {
                aType = _aType;
                aSubject = null;
                aObject = null;
                
                description = $"{aType}";
            }
        }

        public static Action BatterHitBall = new Action(Positions.batter, ActionType.SwingHit, Positions.ball);
        public static Action BatterMissBall = new Action(Positions.batter, ActionType.SwingMiss, Positions.ball);
        public static Action BatterRunsFirst = new Action(Positions.batter, ActionType.Run, Positions.base1st);
        public static Action PitcherThrowStrike = new Action(Positions.pitcher, ActionType.Throw, Positions.strikePoint);
        public static Action Baseman1stRunsSecond = new Action(Positions.baseman1st, ActionType.SwingHit, Positions.base2nd);
        public static Action Baseman2ndRunsThird = new Action(Positions.baseman2nd, ActionType.SwingHit, Positions.base3rd);
        public static Action Baseman3rdRunsHome = new Action(Positions.baseman3rd, ActionType.SwingHit, Positions.baseHome);

        public static Action Cleanup = new Action(ActionType.Cleanup);
    }
}