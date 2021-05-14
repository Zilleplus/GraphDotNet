using System;
using System.Collections.Generic;

namespace Core
{
    public static partial class Algorithm
    {
        public class DFSTracker : IDFSVisitor
        {
            private readonly HashSet<int> processed = new HashSet<int>();

            private readonly HashSet<int> discovered = new HashSet<int>();

            public bool IsProcessed(int i)
                => processed.Contains(i);

            public bool IsDiscovered(int i)
                => discovered.Contains(i);

            public void EarlyVisit(int e)
            {
                discovered.Add(e);
            }

            public void LateVisit(int e)
            {
                processed.Add(e);
            }

            public void Visit(Edge e)
            {
            }

            enum EdgeClass { TREE, BACK, FORWARD, CROSS, SELFLOOP }

            private EdgeClass EdgeClasification(Edge e, ITraversable g)
            {
                return EdgeClass.SELFLOOP;
            }

            public void ProcessEdge(Edge e)
            {

            }
        }

        class TopSortVisitor : IDFSVisitor
        {
            private DFSTracker tracker = new DFSTracker();

            public void EarlyVisit(int e)
            {
                tracker.EarlyVisit(e);
            }

            public void LateVisit(int e)
            {
                tracker.LateVisit(e);
            }

            public void Visit(Edge e)
            {
                tracker.Visit(e);
            }
        }

        public static IEnumerable<int> TopSort(ITraversable g)
        {
            throw new NotImplementedException();
        }
    }
}
