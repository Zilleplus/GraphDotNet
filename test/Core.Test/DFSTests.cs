using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Core.Test
{
    class TestDFSVisitor : IDFSVisitor
    {
        public List<int> visited = new List<int>();

        public void EarlyVisit(int e)
        {
            visited.Add(e);
        }

        public void LateVisit(int e)
        {
        }

        public void Visit(Edge e)
        {
        }
    }

    public class DFSTests
    {
        [Fact]
        public void GiveSimpleGraphCheckDFSVisits()
        {
            var g = new Graph();

            var vs = g.AddVertexRange(10).ToList();

            var edges = new[] {
                (start: vs[0],stop: vs[1]),
                (start: vs[1],stop: vs[2]),
                (start: vs[1],stop: vs[3]),
                (start: vs[3],stop: vs[4]),
                (start: vs[4],stop: vs[5]),
                (start: vs[3],stop: vs[6]),
                (start: vs[4],stop: vs[7]),
                (start: vs[6],stop: vs[7]) };

            foreach (var (start, stop) in edges)
            {
                g.AddEdge(start, stop);
            }

            /*
             * 
             * 0
             * |
             * 1 - 2
             * |
             * 3 - 4 - 5
             * |   |
             * 6 - 7
             * 
             */
            var vis = new TestDFSVisitor();
            Algorithm.DFS(g, vis, vs[0]);

            Assert.Equal(vis.visited, new int[] { 0, 1, 3, 6, 7, 4, 5, 2 });
        }
    }
}
