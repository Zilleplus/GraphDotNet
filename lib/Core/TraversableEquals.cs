using System;
using System.Linq;

namespace Core
{
    public static partial class Algorithm
    {
        public static bool Equals(ITraversable left, ITraversable right)
        {
            if (left.GetVertices().Count() != right.GetVertices().Count())
            {
                return false;
            }
            var verticesEquals = left.GetVertices().SequenceEqual(right.GetVertices());
            var edgesEquals = !left.GetVertices()
                .Any(v => !left.GetAdjacentEdges(v).SequenceEqual(right.GetAdjacentEdges(v)));

            return verticesEquals && edgesEquals;
        }

        public static bool Isomorph(ITraversable left, ITraversable right)
        {
            var leftVertices = left.GetVertices().ToList();
            var rightVertices = left.GetVertices().ToList();

            if (leftVertices.Count != rightVertices.Count)
            {
                return false;
            }
            leftVertices.Sort();
            rightVertices.Sort();

            var leftGroups = leftVertices
                .Select(v => (vertex: v, edges: left.GetAdjacentEdges(v)))
                .GroupBy(v => v.edges.Count()).ToList();
            var rightGroups = rightVertices
                .Select(v => (vertex: v, edges: right.GetAdjacentEdges(v)))
                .GroupBy(v => v.edges.Count()).ToList();

            if (leftGroups.Count() != rightGroups.Count())
            {
                return false;
            }

            if (leftGroups
                .Zip(rightGroups)
                .Any(gs => gs.First.Count() != gs.Second.Count()))
            {
                return false;
            }

            // Maybe we should do topological sort here?
            // or backtracking algorithm, trying all posibilities.

            throw new NotImplementedException();

            // foreach(var (l, r) in leftGroups.Zip(rightGroups)) {}
        }
    }
}
