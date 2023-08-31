using System;

namespace FibDev.Baseball.Records
{
    [Serializable]
    public class Stats
    {
        public int runs;
        public int hits;
        public int errors;

        public void Add(RecordType type, int quantity = 1)
        {
            switch (type)
            {
                case RecordType.Run:
                    runs += quantity;
                    break;
                case RecordType.Hit:
                    hits += quantity;
                    break;
                case RecordType.Error:
                    errors += quantity;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}
