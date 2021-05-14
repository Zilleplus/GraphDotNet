using System.Collections.Generic;

namespace Core
{
    public record Edge
    {
        public int Start { get; set; }

        public int Stop { get; set; }

        public int Weight { get; set; }

        public Edge Invert()
        => this with { Start = Stop, Stop = Start };
    }

    public class EdgeWeightComparer : IComparer<Edge>
    {
        public int Compare(Edge x, Edge y)
        {
            if (x.Weight == y.Weight)
            {
                return 0;
            }
            return x.Weight < y.Weight ? -1 : 1;
        }
    }
}
