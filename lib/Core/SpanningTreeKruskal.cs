using System;
using System.Linq;

namespace Core
{
    public static partial class Algorithm
    {
        public static ITraversable SpanningTreeKruskal(ITraversable graph)
        {
            if (graph.IsDirected)
            {
                throw new NotImplementedException("Spanning tree is not available on directed graph.");
            }

            var vertices = graph.GetVertices();
            var unionFind = new UnionSet(vertices.Max() + 1);
            var spanningTree = new Graph { IsDirected = false };
            spanningTree.AddVertexRange(vertices.Max() + 1);

            var orderedEdges = vertices
                .SelectMany(v => graph.GetAdjacentEdges(v))
                .ToList();
            orderedEdges.Sort(new EdgeWeightComparer()); // sort based on weight

            // keep adding edges till all components are connected.
            foreach (var e in orderedEdges)
            {
                if (!unionFind.SameComponent(e.Start, e.Stop))
                {
                    unionFind.UnionSets(e.Start, e.Stop);
                    spanningTree.AddDoubleEdge(e);
                }
            }

            return spanningTree;
        }
    }
}
