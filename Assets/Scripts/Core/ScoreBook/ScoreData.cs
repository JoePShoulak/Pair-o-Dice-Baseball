using System;

namespace FibDev.Core.ScoreBook
{
    [Serializable]
    public class ScoreData
    {
        public string[] records;

        public ScoreData(string[] pRecords)
        {
            records = pRecords;
        }  
        
        public ScoreData()
        {
            records = null;
        }
    }
}