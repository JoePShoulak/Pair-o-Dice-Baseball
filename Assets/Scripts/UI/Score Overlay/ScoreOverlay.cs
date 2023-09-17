using UnityEngine;

using FibDev.Baseball.Bases;

namespace FibDev.UI.Score_Overlay
{
    public class ScoreOverlay : MonoBehaviour
    {
        [SerializeField] private ScoreBox awayScore;
        [SerializeField] private ScoreBox homeScore;
        [SerializeField] private InningBox innings;
        private BaseBox bases;
        private OutBox outs;

        private void Start()
        {
            bases = GetComponentInChildren<BaseBox>();
            outs = GetComponentInChildren<OutBox>();
        }

        public void SetColors(Color awayColor, Color homeColor)
        {
            awayScore.SetColor(awayColor);
            homeScore.SetColor(homeColor);
        }

        public void SetScores(int awayRuns, int homeRuns)
        {
            awayScore.SetScore(awayRuns);
            homeScore.SetScore(homeRuns);
        }

        public void SetBases(Bases pBases)
        {
            bases.SetBases(pBases);
        }

        public void SetOuts(int numberOfOuts)
        {
            outs.SetOuts(numberOfOuts);
        }

        public void AdvanceInning()
        {
            innings.Advance();
        }

        public void ClearActivity()
        {
            bases.Reset();
        }

        public void Reset()
        {
            innings.Reset();
            homeScore.Reset();
            awayScore.Reset();
        }
    }
}
