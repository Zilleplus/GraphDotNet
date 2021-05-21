using System;
using System.Linq;

namespace Core
{
    public static partial class Algorithm
    {
        public static ITraversable AllPairsShortestPathFloyd(ITraversable graph)
        {
            var output = new GraphMatrix(graph.GetVertices().Max() + 1);
            foreach (var e in graph.GetVertices().SelectMany(v => graph.GetAdjacentEdges(v)))
            {
                output.SetWeight(e.Start, e.Stop, e.Weight);
            }

            foreach (var k in graph.GetVertices())
            {
                foreach (var i in graph.GetVertices())
                {
                    foreach (var j in graph.GetVertices())
                    {
                        var through_k = output
                            .GetEdge(j, k)
                            .Bind(w1 => graph.GetEdge(k, i)
                            .Map(w2 => w1.Weight + w2.Weight));

                        var currentCost = output
                            .GetEdge(j, i)
                            .Map(e => e.Weight);

                        if (through_k.HasValue)
                        {
                            output.SetWeight(
                                j,
                                i,
                                Math.Min(
                                    through_k.Value,
                                    currentCost.HasValue ? currentCost.Value : int.MaxValue));
                        }
                    }
                }

            }

            return output;
        }
    }
}
