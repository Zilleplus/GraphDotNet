using System.Linq;

namespace Core
{
    public static partial class Algorithm
    {
        public static ITraversable Transpose(ITraversable g)
        {
            // Instead of creating a new graph, we could do this lazely as a proxi-object.
            var newGraph = new Graph();

            var maxNode = g.GetVertices().Max();
            var nodes = newGraph.AddVertexRange(maxNode + 1);

            foreach (var n in nodes)
            {
                var es = g.GetAdjacentEdges(n);
                foreach (var e in es)
                {
                    newGraph.AddEdge(e.Stop, e.Start, e.Weight);
                }
            }

            return newGraph;
        }
    }
}
