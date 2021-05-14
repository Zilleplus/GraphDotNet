using System.Linq;
using Xunit;

namespace Core.Test
{
    public class TransposeTests
    {
        [Fact]
        public static void GiveSmallGraphCheckTranspose()
        {
            var g = new Graph();
            var vs = g.AddVertexRange(5).ToList();
            g.AddEdge(vs[0], vs[1]);
            g.AddEdge(vs[1], vs[0]);
            g.AddEdge(vs[1], vs[2]);
            g.AddEdge(vs[1], vs[3]);
            g.AddEdge(vs[3], vs[1]);

            var res = Algorithm.Transpose(g);

            var expected = new Graph();
            // Reliese on implementation details of graph to
            // make sure the new id's are the same as the old ones.
            var vsExpected = expected.AddVertexRange(5).ToList();
            expected.AddEdge(vsExpected[1], vsExpected[0]);
            expected.AddEdge(vsExpected[0], vsExpected[1]);
            expected.AddEdge(vsExpected[2], vsExpected[1]);
            expected.AddEdge(vsExpected[3], vsExpected[1]);
            expected.AddEdge(vsExpected[1], vsExpected[3]);

            Assert.True(Algorithm.Equals(res, expected));
        }

        [Fact]
        public static void GiveSmallGraphCheckIfTransposedOfTransposedHasNoEffect()
        {
            var g = new Graph();
            var vs = g.AddVertexRange(5).ToList();

            var gT = Algorithm.Transpose(g);
            var gTT = Algorithm.Transpose(gT);

            Assert.True(Algorithm.Equals(g, gTT));
        }
    }
}
