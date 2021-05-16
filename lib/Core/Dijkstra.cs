using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Core
{
    public static partial class Algorithm
    {
        internal class DijkstraDistanceComparer : IComparer<Edge>
        {
            private readonly int[] distances;

            public DijkstraDistanceComparer(int[] distances)
            {
                this.distances = distances;
            }

            public int Compare(Edge x, Edge y)
            {
                if (distances[x.Stop] == distances[y.Stop]) { return 0; }
                if (distances[x.Stop] < distances[y.Stop]) { return -1; }
                if (distances[x.Stop] > distances[y.Stop]) { return 1; }

                throw new NotImplementedException();
            }
        }

        public record DijkstraSolution(int[] Previous, int[] Distances)
        {
            public ReadOnlyCollection<int> Path(int target)
            {
                var prev = Previous[target];
                var path = new List<int> { target };
                while (target != prev)
                {
                    path.Add(prev);
                    target = prev;
                    prev = Previous[target];
                }
                path.Reverse();

                return path.AsReadOnly();
            }

            public int Distance(int target)
                => Distances[target];
        }

        public static DijkstraSolution DijkStra(ITraversable graph, int source)
        {
            var numberOfVertices = graph.GetVertices().Max() + 1;

            var distances = Enumerable.Range(0, numberOfVertices).Select(n => int.MaxValue).ToArray();
            var previous = distances.ToArray();
            var knowns = new HashSet<int>();
            var t = new C5.IntervalHeap<Edge>(new EdgeWeightComparer());

            knowns.Add(source);
            distances[source] = 0;
            previous[source] = source;
            foreach (var e in graph.GetAdjacentEdges(source)) { t.Add(e); }

            while (!t.IsEmpty)
            {
                var min = t.FindMin();
                t.DeleteMin();

                // Default value of distances = Int.Max
                var newDistance = distances[min.Start] + min.Weight;
                if (newDistance < distances[min.Stop])
                {
                    previous[min.Stop] = min.Start;
                    distances[min.Stop] = newDistance;
                }

                if (!knowns.Contains(min.Stop)) // If this edge reaches an undiscovered node.
                {
                    knowns.Add(min.Stop);
                    foreach (var e in graph.GetAdjacentEdges(min.Stop))
                    {
                        t.Add(e);
                    }
                }
            }

            return new DijkstraSolution(
                Distances: distances,
                Previous: previous);
        }
    }
}
