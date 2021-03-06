namespace Core
{
    public interface IBFSVisitor
    {
        /// <summary>
        /// Before the edges are called.
        /// </summary>
        void EarlyVisit(int e);

        /// <summary>
        /// Edges of last early visit that 
        /// lead to undiscovered nodes.
        /// </summary>
        void Visit(Edge e);

        /// <summary>
        /// After all edges are Visited.
        /// </summary>
        void LateVisit(int e);
    }
}
