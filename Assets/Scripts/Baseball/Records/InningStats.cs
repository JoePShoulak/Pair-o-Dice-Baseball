using System;

namespace FibDev.Baseball.Records
{
    [Serializable]
    public class InningStats
    {
        public int runs;
        public int hits;
        public int errors;

        public void Add(StatType type, int quantity = 1)
        {
            switch (type)
            {
                case StatType.Run:
                    runs += quantity;
                    break;
                case StatType.Hit:
                    hits += quantity;
                    break;
                case StatType.Error:
                    errors += quantity;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}
