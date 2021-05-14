using System.Collections.Generic;
using System.Linq;

namespace Core
{
    internal class StronglyConnectedComponentsForwardVisitor : IDFSVisitor
    {
        public Stack<int> visited = new Stack<int>();

        public void EarlyVisit(int e)
        {
            visited.Push(e);
        }

        public void LateVisit(int e) { }

        public void Visit(Edge e) { }
    }
    internal class StronglyConnectedComponentsBackwardVisitor : IDFSVisitor
    {
        private readonly int toFind;

        public bool VertexFound { get; private set; } = false;

        public StronglyConnectedComponentsBackwardVisitor(int toFind)
        {
            this.toFind = toFind;
        }

        public void EarlyVisit(int e)
        {
            if (e == toFind)
            {
                VertexFound = true;
            }
        }

        public void LateVisit(int e) { }

        public void Visit(Edge e) { }
    }

    public static partial class Algorithm
    {
        public static IEnumerable<int> StronglyConnectedComponents(ITraversable traversable, int startVertex)
        {
            var sv = new StronglyConnectedComponentsForwardVisitor();
            DFS(traversable, sv, startVertex);

            // We should make the transpose lazy... will be more interesting.
            var transposed = Algorithm.Transpose(traversable);

            foreach (var v in sv.visited.Where(vv => vv != startVertex))
            {
                var vs = new StronglyConnectedComponentsBackwardVisitor(v);
                DFS(transposed, vs, startVertex);
                if (vs.VertexFound)
                {
                    yield return v;
                }
            }
        }
    }
}
