using System.Linq;
using Xunit;

namespace Core.Test
{
    public class StronglyConnectedComponentsTests
    {
        [Fact]
        public static void GiveGraphFindStronglyConnectedComponents()
        {
            var g = new Graph();

            var vs = g.AddVertexRange(5).ToList();

            g.AddEdge(vs[0], vs[1]);
            g.AddEdge(vs[1], vs[0]);
            g.AddEdge(vs[1], vs[2]); // 2 is not a strongly connected component
            g.AddEdge(vs[1], vs[3]);
            g.AddEdge(vs[3], vs[1]);

            var expected = new[] { vs[1], vs[3] };

            var res = Algorithm.StronglyConnectedComponents(g, vs[0]).ToList();
            res.Sort();

            Assert.Equal(expected: expected, actual: res);
        }
    }
}
