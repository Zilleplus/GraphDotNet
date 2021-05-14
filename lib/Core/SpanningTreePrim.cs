using C5;
using System.Linq;

namespace Core
{
    public static partial class Algorithm
    {
        public static ITraversable SpanningTreePrim(ITraversable graph, int startVertex = 0)
        {
            // Greedy algorithm:
            // -> Alway's add lowest cost new vertex to the spanning tree.

            var vertices = graph.GetVertices().ToList();

            var spanningTree = new Graph();
            spanningTree.AddVertexRange(vertices.Max() + 1);
            var t = new IntervalHeap<Edge>(new EdgeWeightComparer());
            var verticesInGraph = new System.Collections.Generic.HashSet<int>();

            if (vertices.Count() == 0) { return spanningTree; }
            foreach (var e in graph.GetAdjacentEdges(vertices[startVertex])) { t.Add(e); }
            verticesInGraph.Add(vertices[startVertex]);

            while (!t.IsEmpty)
            {
                var n = t.FindMin();
                t.DeleteMin();
                if (!verticesInGraph.Contains(n.Stop))
                {
                    verticesInGraph.Add(n.Stop);
                    spanningTree.AddEdge(n);
                    spanningTree.AddEdge(n.Invert());
                    foreach (var e in graph
                        .GetAdjacentEdges(vertices[n.Stop])
                        .Where(e => !verticesInGraph.Contains(e.Stop)))
                    { t.Add(e); }
                }
            }

            return spanningTree;
        }
    }
}
