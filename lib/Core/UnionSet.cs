using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public class UnionSet
    {
        List<int> parents;
        List<int> sizes;

        public UnionSet(int size)
        {
            parents = new List<int>(size);
            parents.AddRange(Enumerable.Range(0, size));
            sizes = new List<int>(size);
            sizes.AddRange(Enumerable.Range(0, size).Select(n => 1));
        }

        public int Find(int x)
        {
            if (parents[x] == x)
            {
                // if the parent is root, quit;
                return x;
            }
            return Find(parents[x]);
        }

        public void UnionSets(int left, int right)
        {
            var leftRoot = Find(left);
            var rightRoot = Find(right);

            if (sizes[left] < sizes[right])
            {
                sizes[left] = sizes[left] + sizes[right];
                // We leave the right size as its no a root any more.
                parents[leftRoot] = rightRoot;
            }
            else
            {
                sizes[right] = sizes[left] + sizes[right];
                // We leave the left size as its no a root any more.
                parents[rightRoot] = leftRoot;
            }
        }

        public bool SameComponent(int x, int y)
            => Find(x) == Find(y);
    }
}
