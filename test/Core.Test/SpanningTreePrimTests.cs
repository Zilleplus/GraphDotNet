using System.Linq;
using Xunit;

namespace Core.Test
{
    public class SpanningTreePrimTests
    {
        [Fact]
        public static void GivenGraphFindSpanningTreeUsingPrimAlg()
        {
            var g = ExampleGraph.Create();

            var spanningTreeEdges = Algorithm.SpanningTreePrim(g)
                .AsEnumerable()
                .ToList();

            var expectedTree = new Graph();
            expectedTree.AddVertexRange(g.NumberOfVertices);
            expectedTree.AddDoubleEdge(new Edge { Start = 0, Stop = 1, Weight = 5 });
            expectedTree.AddDoubleEdge(new Edge { Start = 0, Stop = 3, Weight = 7 });
            expectedTree.AddDoubleEdge(new Edge { Start = 3, Stop = 4, Weight = 4 });
            expectedTree.AddDoubleEdge(new Edge { Start = 3, Stop = 5, Weight = 3 });
            expectedTree.AddDoubleEdge(new Edge { Start = 5, Stop = 2, Weight = 2 });
            expectedTree.AddDoubleEdge(new Edge { Start = 5, Stop = 6, Weight = 2 });
            var expectedTreeEdges = expectedTree.AsEnumerable().ToList();

            Assert.Equal(
                expected: expectedTreeEdges.Count(),
                actual: spanningTreeEdges.Count());

            foreach (var exp in expectedTreeEdges)
            {
                spanningTreeEdges.Contains(exp);
            }
        }
    }
}
