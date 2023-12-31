namespace FibDev.Baseball.Plays
{
    public enum Operation
    {
        BatterHitBall,
        BatterMissBall,
        BatterRunsFirst,
            
        PitcherThrowStrike,
        PitcherThrowsBall,
        PitcherHitsPlayer,
            
        Baseman1stRunsSecond,
        Baseman2ndRunsThird,
        Baseman3rdRunsHome,
            
        BasemenAdvanceIfForced,
        BasemanAdvance,
            
        FielderCatchesBall,
        OutAtFirst,
        Error,
        
        OutAtSecond,

        Cleanup,
        RecordHit // smelly
    }
}