using System.Diagnostics;
using Xunit;

namespace Core.Test
{
    public class DijkstraTests
    {
        [Theory]
        [MemberData(nameof(PathCases))]
        public static void GivenGraphFindDijkstraSolution(string name, int target, int[] expectedPath, int expectedCost)
        {
            Debug.Write($"Test: {name}.");

            var graph = ExampleGraph.Create();

            var dijkstra = Algorithm.DijkStra(graph, source: 0);

            var path = dijkstra.Path(target);
            var cost = dijkstra.Distance(target);

            Assert.Equal(expected: expectedPath, actual: path);
            Assert.Equal(expected: expectedCost, actual: cost);
        }

        public static TheoryData<string, int, int[], int> PathCases()
        {
            return new TheoryData<string, int, int[], int>
            {
                {
                    "single edge path",
                    3,
                    new [] { 0,3 },
                    7 // direct connection via the edge (0,3) with weight 7
                },
                {
                    "double edge path",
                    2,
                    new [] { 0,3, 2 },
                    7+4
                },
                {
                    "long path",
                    6,
                    new [] { 0,3, 5,6 },
                    7+3 + 2
                }
            };
        }
    }
}
