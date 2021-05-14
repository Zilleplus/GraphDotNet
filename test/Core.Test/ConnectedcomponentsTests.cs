using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Core.Test
{
    public class ConnectedcomponentsTests
    {
        [Fact]
        public static void GivenGraphGetComponents()
        {
            var g = new Graph();
            var vs = g.AddVertexRange(7).ToList();

            g.AddEdge(vs[0], vs[1]);
            g.AddEdge(vs[0], vs[2]);

            g.AddEdge(vs[3], vs[4]);
            g.AddEdge(vs[4], vs[5]);

            var res = Algorithm.ConnectedComponents(g);

            var expectedComponents = new List<List<int>> { new List<int> { vs[0], vs[1], vs[2] }, new List<int> { vs[3], vs[4], vs[5] }, new List<int> { 6 } };

            Assert.Equal(expected: expectedComponents, actual: res);
        }
    }
}
