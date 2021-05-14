using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Core
{
    public class Graph : ITraversable
    {
        /// <summary>
        /// List of adjacent edges
        /// </summary>
        private Dictionary<int, List<Edge>> edges_
            = new Dictionary<int, List<Edge>>();

        public IEnumerable<int> GetVertices()
            => Enumerable.Range(0, currentVertexCount);

        public bool IsDirected { get; init; } = true;

        public int NumberOfEdges { get { return edges_.Select(es => es.Value.Count).Sum(); } }

        public int NumberOfVertices { get { return currentVertexCount; } }

        private int currentVertexCount = 0;

        public IEnumerable<Edge> GetAdjacentEdges(int vertexIndex)
        {
            Debug.Assert(vertexIndex < currentVertexCount);
            List<Edge> value;
            edges_.TryGetValue(vertexIndex, out value);
            return value ?? Enumerable.Empty<Edge>();
        }

        public int AddVertex()
        {
            var newVertex = currentVertexCount;
            currentVertexCount = currentVertexCount + 1;
            return newVertex;
        }

        public IEnumerable<int> AddVertexRange(int numberOfVertices)
        {
            if (numberOfVertices == 0)
            {
                return Enumerable.Empty<int>();
            }

            var vs = Enumerable.Range(currentVertexCount, numberOfVertices).ToList();
            currentVertexCount = currentVertexCount + numberOfVertices;
            return vs;
        }

        public (Optional<Edge> forward, Optional<Edge> backward) AddDoubleEdge(Edge e)
        {
            var f = AddEdge(e);
            var b = AddEdge(e.Invert());

            return (f, b);
        }

        public Optional<Edge> AddEdge(Edge e)
            => AddEdge(e.Start, e.Stop, e.Weight);

        public (Optional<Edge> forward, Optional<Edge> backward)
            AddDoubleEdge(int source, int target, int weight = 1)
            => AddDoubleEdge(new Edge { Start = source, Stop = target, Weight = weight });

        /// <summary>
        /// Add's and edge between two verices, return's if the edge was added. Returns Empty optional if edge already exists. This edge will never modify an existing edge.
        /// </summary>
        public Optional<Edge> AddEdge(int source, int target, int weight = 1)
        {
            Debug.Assert(source < currentVertexCount);
            Debug.Assert(target < currentVertexCount);
            var newEdge = new Edge { Start = source, Stop = target, Weight = weight };

            List<Edge> outEdges;
            if (edges_.TryGetValue(source, out outEdges))
            {
                if (outEdges.Any(s => s.Stop == target))
                {
                    return new Optional<Edge>();
                }
                else
                {
                    outEdges.Add(newEdge);
                }
            }
            else
            {
                edges_.Add(source, new List<Edge> { newEdge });
            }

            return new Optional<Edge>(newEdge);
        }
    }
}
