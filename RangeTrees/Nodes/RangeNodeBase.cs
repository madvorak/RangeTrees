namespace RangeTrees.Nodes
{
    abstract internal class RangeNodeBase<T> where T : RangeNodeBase<T>
    {
        private const double alpha = 0.7;

        protected T leftChild;
        protected T rightChild;

        public int Size { get; protected set; }

        public RangeNodeBase()
        {
            leftChild = null;
            rightChild = null;
            Size = 1;
        }

        public bool IsBalanced()
        {
#if(!DEBUG)
            if (Size < 10)
            {
                return true;
            }
#endif
            if (leftChild != null)
            {
                if (leftChild.Size > alpha * Size)
                {
                    return false;
                }
            }
            if (rightChild != null)
            {
                if (rightChild.Size > alpha * Size)
                {
                    return false;
                }
            }
            return true;
        }

        // for testing purposes only
        public bool IsTheWholeTreeConsistent()
        {
            int totalSize = 1;
            if (!IsBalanced())
            {
                return false;
            }
            if (leftChild != null)
            {
                if (!leftChild.IsTheWholeTreeConsistent())
                {
                    return false;
                }
                totalSize += leftChild.Size;
            }
            if (rightChild != null)
            {
                if (!rightChild.IsTheWholeTreeConsistent())
                {
                    return false;
                }
                totalSize += rightChild.Size;
            }
            return (Size == totalSize) && areInternalsConsistent();
        }

        protected abstract bool areInternalsConsistent();




        private static int nVisitsQueryTotal = 0;
        private static int nVisitsQueryMax = 0;
        private static int nVisitsQueryNow = 0;
        private static int nVisitsInsertTotal = 0;
        private static int nVisitsInsertMax = 0;
        private static int nVisitsInsertNow = 0;

        protected static void registerVisitByQuery()
        {
            nVisitsQueryTotal++;
            nVisitsQueryNow++;
        }

        protected static void registerVisitByInsert()
        {
            nVisitsInsertTotal++;
            nVisitsInsertNow++;
        }

        public static void RegisterQueryEnd()
        {
            if (nVisitsQueryNow > nVisitsQueryMax)
            {
                nVisitsQueryMax = nVisitsQueryNow;
            }
            nVisitsQueryNow = 0;
        }

        public static void RegisterInsertEnd()
        {
            if (nVisitsInsertNow > nVisitsInsertMax)
            {
                nVisitsInsertMax = nVisitsInsertNow;
            }
            nVisitsInsertNow = 0;
        }
    }
}
