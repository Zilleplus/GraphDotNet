using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public static partial class Algorithm
    {
        internal class ConnectedComponentsVistor : IBFSVisitor
        {
            public List<int> Component = new List<int>();

            public void EarlyVisit(int e)
            {
                Component.Add(e);
            }

            public void LateVisit(int e) { }

            public void Visit(Edge e) { }
        }

        public static IEnumerable<IEnumerable<int>> ConnectedComponents(ITraversable g)
        {
            var vs = g.GetVertices().ToList();
            var comps = new List<List<int>>();
            while (vs.Any())
            {
                var vis = new ConnectedComponentsVistor();
                BFS(g, vis, vs.First());
                vs.RemoveAll(e => vis.Component.Contains(e));
                comps.Add(vis.Component);
            }

            return comps;
        }
    }
}
