using System.Linq;

namespace Core.Test
{
    public class ExampleGraph
    {
        public static Graph Create(bool isDirected = true)
        {
            // Example found on top of page 248 in "The algorithm design manual".
            var g = new Graph { IsDirected = isDirected };

            var vs = g.AddVertexRange(7).ToList();

            g.AddEdge(vs[0], vs[1], 5);
            g.AddEdge(vs[0], vs[3], 7);
            g.AddEdge(vs[0], vs[4], 12);

            g.AddEdge(vs[1], vs[0], 5);
            g.AddEdge(vs[1], vs[2], 7);
            g.AddEdge(vs[1], vs[3], 9);

            g.AddEdge(vs[2], vs[3], 9);
            g.AddEdge(vs[2], vs[1], 7);
            g.AddEdge(vs[2], vs[5], 2);
            g.AddEdge(vs[2], vs[6], 5);

            g.AddEdge(vs[3], vs[0], 7);
            g.AddEdge(vs[3], vs[1], 9);
            g.AddEdge(vs[3], vs[2], 4);
            g.AddEdge(vs[3], vs[4], 4);
            g.AddEdge(vs[3], vs[5], 3);

            g.AddEdge(vs[4], vs[0], 12);
            g.AddEdge(vs[4], vs[3], 4);
            g.AddEdge(vs[4], vs[5], 7);

            g.AddEdge(vs[5], vs[2], 2);
            g.AddEdge(vs[5], vs[3], 3);
            g.AddEdge(vs[5], vs[4], 7);
            g.AddEdge(vs[5], vs[6], 2);

            g.AddEdge(vs[6], vs[2], 5);
            g.AddEdge(vs[6], vs[5], 2);
            g.AddEdge(vs[6], vs[5], 5);

            return g;
        }
    }
}
