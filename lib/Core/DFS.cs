using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public static partial class Algorithm
    {
        public static void DFS(
            ITraversable traversable,
            IDFSVisitor visitor,
            int startVertex)
        {
            var toVisit = new Stack<int>();
            var visited = new HashSet<int>();
            toVisit.Push(startVertex);
            while (toVisit.Any())
            {
                var v = toVisit.Pop();
                visitor.EarlyVisit(v);
                visited.Add(v);
                var adj = traversable.GetAdjacentEdges(v);

                foreach (var a in adj)
                {
                    visitor.Visit(a);
                    if (!visited.Contains(a.Stop))
                    {
                        toVisit.Push(a.Stop);
                    }
                }

                visitor.LateVisit(v);
            }
        }
    }
}
