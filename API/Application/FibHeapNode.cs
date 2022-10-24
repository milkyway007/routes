namespace Application
{
    public class FibHeapNode
    {
        public FibHeapNode()
        {
            Prev = this;
            Next = this;
            Degree = 0;

            Parent = null;
            Child = null;
            IsMarked = null;
        }

        public int AirportId { get; set; }
        public decimal Distance { get; set; }
        public FibHeapNode Parent { get; set; }
        public FibHeapNode Child { get; set; }
        public FibHeapNode Prev { get; set; }
        public FibHeapNode Next { get; set; }
        public int Degree { get; set; }
        public bool? IsMarked { get; set; }
    }
}

