namespace RangeTrees.Nodes
{
    abstract internal class RangeNodeBase<T>
    {
        public static double Alpha { get; }

        protected T[] children;      
        public int Size { get; protected set; } 
    }
}
