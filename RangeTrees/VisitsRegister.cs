namespace RangeTrees
{
    internal class VisitsRegister
    {
        private int nVisitsQueryTotal;
        private int nVisitsQueryMax;
        private int nVisitsQueryNow;
        private int queryCount;
        private int nVisitsInsertTotal;
        private int nVisitsInsertMax;
        private int nVisitsInsertNow;
        private int insertCount;

        private VisitsRegister()
        {
            nVisitsQueryTotal = 0;
            nVisitsQueryMax = 0;
            nVisitsQueryNow = 0;
            queryCount = 0;
            nVisitsInsertTotal = 0;
            nVisitsInsertMax = 0;
            nVisitsInsertNow = 0;
            insertCount = 0;
        }

        private static VisitsRegister instance;
        public static VisitsRegister Instance => instance ?? (instance = new VisitsRegister());

        public void VisitedByQuery()
        {
            nVisitsQueryTotal++;
            nVisitsQueryNow++;
        }

        public void VisitedByInsert()
        {
            nVisitsInsertTotal++;
            nVisitsInsertNow++;
        }

        public void QueryEnded()
        {
            if (nVisitsQueryNow > nVisitsQueryMax)
            {
                nVisitsQueryMax = nVisitsQueryNow;
            }
            nVisitsQueryNow = 0;
            queryCount++;
        }

        public void InsertEnded()
        {
            if (nVisitsInsertNow > nVisitsInsertMax)
            {
                nVisitsInsertMax = nVisitsInsertNow;
            }
            nVisitsInsertNow = 0;
            insertCount++;
        }

        public int GetMaximumVisitedNodesByQuery()
        {
            return nVisitsQueryMax;
        }

        public double GetAverageVisitedNodesByQuery()
        {
            return nVisitsQueryTotal / (double)queryCount;
        }
        
        public int GetMaximumVisitedNodesByInsert()
        {
            return nVisitsInsertMax;
        }

        public double GetAverageVisitedNodesByInsert()
        {
            return nVisitsInsertTotal / (double)insertCount;
        }
    }
}
