﻿namespace RangeTrees.Nodes
{
    abstract internal class RangeNodeBase<T> where T : RangeNodeBase<T>
    {
        public static double Alpha { get; } = 0.7;

        protected readonly T[] children;
        protected T leftChild
        {
            get
            {
                return children[0];
            }
            set
            {
                children[0] = value;
            }
        }
        protected T rightChild
        {
            get
            {
                return children[1];
            }
            set
            {
                children[1] = value;
            }
        }

        public int Size { get; protected set; } 

        public RangeNodeBase()
        {
            children = new T[2];
            children[0] = null;
            children[1] = null;
            Size = 1;
        }

        public bool IsBalanced()
        {
            if (Size < 3) // TODO change for production (increase)
            {
                return true;
            }
            foreach (T child in children)
            {
                if (child != null)
                {
                    if (child.Size > Alpha * Size)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        // for testing purposes only
        protected abstract bool areInternalsBalanced();

        // for testing purposes only
        public bool IsTheWholeTreeBalanced()
        {
            if (!IsBalanced())
            {
                return false;
            }
            foreach (T child in children)
            {
                if (child != null)
                {
                    if (!child.IsTheWholeTreeBalanced())
                    {
                        return false;
                    }
                }
            }
            return areInternalsBalanced();
        }
    }
}
