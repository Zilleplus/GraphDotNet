using System;
using System.Collections.Generic;
using System.Linq;

namespace Core
{

    public static partial class Algorithm
    {
        /// <summary>
        /// Finds the path with remainding residual.
        /// </summary>
        private class EdmondsKarpAlgoSearchVisitor : IBFSVisitor
        {
            private List<int> parents;

            public EdmondsKarpAlgoSearchVisitor(int numberOfVertices)
            {
                parents = Enumerable
                    .Range(0, numberOfVertices).Select(i => -1)
                    .ToList();
            }

            public void EarlyVisit(int e) { }
            public void LateVisit(int e) { }

            public void Visit(Edge e)
            {
                parents[e.Stop] = e.Start;
            }

            public int PathVolume(Graph residualGraph, int start, int end)
            {
                if (parents[end] == -1)
                { return 0; }

                // We can assume that this edge exist, as Visit(Edge e) was called earlier.
                var currentResidual = residualGraph.GetEdge(parents[end], end).Value.Weight;
                if (start == parents[end])
                {
                    return currentResidual;
                }

                // Volume depends on the weakest link.
                return Math.Min(PathVolume(residualGraph, start, parents[end]), currentResidual);
            }

            public void AugmentPath(Graph residualGraph, int start, int end, int volume)
            {
                // When we know the volume, loop over the augmented path
                // and adjust the residuals, before we loop again.
                if (start == end)
                { return; }

                var forwardEdge = residualGraph.GetEdge(parents[end], end).Value;
                var newForwardEdge = forwardEdge with { Weight = forwardEdge.Weight - volume };
                if (newForwardEdge.Weight == 0)
                {
                    // If the forward weight is zero, we should take it out.
                    // If this edge is not removed, it will show up in the next 
                    // dfs, which is not good.. as we won't consider the alternative flows.
                    residualGraph.RemoveEdge(forwardEdge);
                }
                else
                {
                    residualGraph.UpdateEdge(newForwardEdge);
                }

                var backwardEdge = residualGraph.GetEdge(end, parents[end]).Value;
                residualGraph.UpdateEdge(backwardEdge with { Weight = backwardEdge.Weight + volume });

                AugmentPath(residualGraph, start, parents[end], volume);
            }
        }

        private static Graph Residuals(ITraversable graph)
        {
            var res = new Graph();
            var numberOfVertices = graph.GetVertices().Max() + 1;
            res.AddVertexRange(numberOfVertices);

            foreach (var v in graph.GetVertices())
            {
                foreach (var e in graph.GetAdjacentEdges(v))
                {
                    res.AddEdge(e);
                }
            }

            return res;
        }

        public static int EdmondsKarpAlgo(ITraversable graph, int source, int sink)
        {
            // Implementation based on Algorithm design manual page 270-271.
            var res = Residuals(graph);

            var vis = new EdmondsKarpAlgoSearchVisitor(res.GetVertices().Max() + 1);
            BFS(res, vis, source);
            var volume = vis.PathVolume(res, source, sink);
            var flow = volume;

            while (volume > 0)
            {
                vis.AugmentPath(res, source, sink, volume);
                vis = new EdmondsKarpAlgoSearchVisitor(res.GetVertices().Max() + 1);
                BFS(res, vis, source);
                volume = vis.PathVolume(res, source, sink);
                flow = flow + volume;
            }

            return flow;
        }


    }
}
