using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public interface ITraversable
    {
        IEnumerable<Edge> GetAdjacentEdges(int v);

        IEnumerable<int> GetVertices();

        bool IsDirected { get; }
    }

    public static class ITraversableExt
    {
        public static IEnumerable<Edge> AsEnumerable(this ITraversable t)
            => t.GetVertices().SelectMany(v => t.GetAdjacentEdges(v));
    }
}
