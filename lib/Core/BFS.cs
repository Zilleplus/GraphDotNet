using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public static partial class Algorithm
    {
        public static void BFS(
            ITraversable traversable,
            IBFSVisitor visitor,
            int startVertex)
        {
            var toVisit = new Queue<int>();
            var enqueued = new HashSet<int>();
            toVisit.Enqueue(startVertex);
            enqueued.Add(startVertex);

            while (toVisit.Any())
            {
                var v = toVisit.Dequeue();
                visitor.EarlyVisit(v);
                var adj = traversable.GetAdjacentEdges(v);

                foreach (var a in adj)
                {
                    visitor.Visit(a);
                    if (!enqueued.Contains(a.Stop))
                    {
                        toVisit.Enqueue(a.Stop);
                        enqueued.Add(a.Stop);
                    }
                }

                visitor.LateVisit(v);
            }
        }
    }
}
