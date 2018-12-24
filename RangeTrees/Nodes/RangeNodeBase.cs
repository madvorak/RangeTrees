namespace RangeTrees.Nodes
{
    abstract internal class RangeNodeBase<T> where T : RangeNodeBase<T>
    {
        public static double Alpha { get; }

        protected readonly T[] children;
        protected T leftChild => children[0];
        protected T rightChild => children[1];
        public int Size { get; protected set; } 

        public RangeNodeBase()
        {
            children = new T[2];
        }

        public bool IsBalanced()
        {
            if (Size < 5 / (1 - Alpha))
            {
                return true;
            }
            foreach (T child in children)
            {
                if (child.Size > Alpha * Size)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
