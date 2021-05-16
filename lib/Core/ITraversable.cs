using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Core
{
    public interface ITraversable
    {
        [Pure]
        IEnumerable<Edge> GetAdjacentEdges(int v);

        [Pure]
        IEnumerable<int> GetVertices();

        [Pure]
        bool IsDirected { get; }
    }

    public static class ITraversableExt
    {
        [Pure]
        public static IEnumerable<Edge> AsEnumerable(this ITraversable t)
            => t.GetVertices().SelectMany(v => t.GetAdjacentEdges(v));
    }
}
