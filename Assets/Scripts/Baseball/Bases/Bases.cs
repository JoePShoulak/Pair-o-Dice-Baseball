using System;

namespace FibDev.Baseball.Bases
{
    [Serializable]
    public class Bases
    {
        public Base first = new();
        public Base second = new();
        public Base third = new();
        public Base home = new();

        public void Reset()
        {
            first.runnerOn = false;
            second.runnerOn = false;
            third.runnerOn = false;
            home.runnerOn = false;
        }

    }
}