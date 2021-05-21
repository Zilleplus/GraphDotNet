using Xunit;

namespace Core.Test
{
    public class EdmonsKarpAlgoTests
    {
        [Fact]
        public static void GivenExampleGraphFindFlow()
        {
            var exampleGraph = ExampleGraph.Create();

            var res = Algorithm.EdmondsKarpAlgo(exampleGraph, source: 0, sink: 6);

            Assert.Equal(expected: 7, actual: res);
        }
    }
}
