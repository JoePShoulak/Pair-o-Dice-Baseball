using FibDev.Baseball.Plays;

namespace FibDev.Baseball.Engine
{
    public static class OddsGrid
    {
        public static readonly PlayEnum[] grid = new[]
        {
            // Outs
            PlayEnum.FoulOut,
            PlayEnum.FoulOut,
            PlayEnum.FoulOut,
            PlayEnum.FoulOut,
            
            PlayEnum.FlyOut,
            PlayEnum.FlyOut,
            PlayEnum.FlyOut,
            PlayEnum.FlyOut,
            PlayEnum.FlyOut,
            PlayEnum.FlyOut,
            PlayEnum.FlyOut,
            PlayEnum.FlyOut,
            PlayEnum.FlyOut,
            PlayEnum.FlyOut,
            PlayEnum.FlyOut,
            PlayEnum.FlyOut,
            PlayEnum.FlyOut,
            PlayEnum.FlyOut,
            PlayEnum.FlyOut,
            PlayEnum.FlyOut,
            PlayEnum.FlyOut,
            
            PlayEnum.LineOut,
            PlayEnum.LineOut,
            PlayEnum.LineOut,
            PlayEnum.LineOut,
            PlayEnum.LineOut,
            PlayEnum.LineOut,
            PlayEnum.LineOut,
            PlayEnum.LineOut,
            
            PlayEnum.PopOut,
            PlayEnum.PopOut,
            PlayEnum.PopOut,
            PlayEnum.PopOut,
            PlayEnum.PopOut,
            PlayEnum.PopOut,
            
            PlayEnum.GroundOut,
            PlayEnum.GroundOut,
            PlayEnum.GroundOut,
            PlayEnum.GroundOut,
            PlayEnum.GroundOut,
            PlayEnum.GroundOut,
            PlayEnum.GroundOut,
            PlayEnum.GroundOut,
            PlayEnum.GroundOut,
            PlayEnum.GroundOut,
            PlayEnum.GroundOut,
            PlayEnum.GroundOut,
            PlayEnum.GroundOut,
            PlayEnum.GroundOut,
            PlayEnum.GroundOut,
            PlayEnum.GroundOut,
            PlayEnum.GroundOut,
            
            PlayEnum.StrikeOut,
            PlayEnum.StrikeOut,
            PlayEnum.StrikeOut,
            PlayEnum.StrikeOut,
            PlayEnum.StrikeOut,
            PlayEnum.StrikeOut,
            PlayEnum.StrikeOut,
            PlayEnum.StrikeOut,
            PlayEnum.StrikeOut,
            PlayEnum.StrikeOut,
            PlayEnum.StrikeOut,
            PlayEnum.StrikeOut,
            PlayEnum.StrikeOut,
            
            // Hits
            PlayEnum.Walk,
            PlayEnum.Walk,
            PlayEnum.Walk,
            PlayEnum.Walk,
            PlayEnum.Walk,
            PlayEnum.Walk,
            PlayEnum.Walk,
            PlayEnum.Walk,
            
            PlayEnum.HitByPitch,
            
            PlayEnum.Error,
            PlayEnum.Error,
            PlayEnum.Error,
            
            PlayEnum.Single,
            PlayEnum.Single,
            PlayEnum.Single,
            PlayEnum.Single,
            PlayEnum.Single,
            PlayEnum.Single,
            PlayEnum.Single,
            PlayEnum.Single,
            PlayEnum.Single,
            PlayEnum.Single,
            PlayEnum.Single,
            PlayEnum.Single,
            PlayEnum.Single,
            PlayEnum.Single,
            PlayEnum.Single,
            
            PlayEnum.Double,
            PlayEnum.Double,
            PlayEnum.Double,
            PlayEnum.Double,
            
            PlayEnum.Triple,
            
            PlayEnum.HomeRun,
            PlayEnum.HomeRun,
            PlayEnum.HomeRun,
        };
    }
}