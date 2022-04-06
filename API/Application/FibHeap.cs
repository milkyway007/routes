namespace Application
{
    public class FibHeap
    {
        private FibHeapNode _min;

        public static FibHeap CreateHip()
        {
            return new FibHeap();
        }

        private FibHeap()
        {
            _min = null;
        }

        public FibHeapNode Insert(FibHeapNode node)
        {
            _min = MergeLists(_min, node);

            return node;
        }

        public static FibHeapNode MergeLists(FibHeapNode a, FibHeapNode b)
        {
            if (a == null && b == null)
            {
                return null;
            }
            if (a == null)
            {
                return b;
            }
            if (b == null)
            {
                return a;
            }

            var temp = a.Next;
            a.Next = b.Next;
            a.Next.Prev = a;
            b.Next = temp;
            b.Next.Prev = b;

            return a.Distance < b.Distance ? a : b;
        }

        public decimal FindMin()
        {
            return _min.Distance;
        }

        public FibHeapNode ExtractMinimum()
        {
            var extractedMin = _min;
            if (extractedMin != null)
            {
                if (extractedMin.Child != null)
                {
                    var child = extractedMin.Child;
                    do
                    {
                        child.Parent = null;
                        child = child.Next;
                    } while (child != extractedMin.Child);
                }

                FibHeapNode nextInRootList = null;
                if (extractedMin.Next != extractedMin)
                {
                    nextInRootList = extractedMin.Next;
                }

                RemoveNodeFromList(extractedMin);

                _min = MergeLists(nextInRootList, extractedMin.Child);
                if (_min != null)
                {
                    _min = Consolidate(_min);
                }
            }

            return extractedMin;
        }

        public FibHeapNode Consolidate(FibHeapNode min)
        {
            var aux = new Dictionary<int,FibHeapNode>();
            var items = new List<FibHeapNode>();
            var current = min;

            do
            {
                items.Add(current);
                current = current.Next;
            } while (min != current);

            foreach(var item in items)
            {
                current = item;
                while (aux.TryGetValue(current.Degree, out var value) && value != null)
                {
                    if (current.Distance > aux[current.Degree].Distance)
                    {
                        (aux[current.Degree], current) = (current, aux[current.Degree]);
                    }

                    LinkHeaps(aux[current.Degree], current);

                    aux[current.Degree] = null;
                    current.Degree++;
                }

                aux[current.Degree] = current;
            }

            min = null;
            foreach(var i in aux.Keys)
            {
                if (aux[i] != null)
                {
                    aux[i].Next = aux[i];
                    aux[i].Prev = aux[i];
                    min = MergeLists(min, aux[i]);
                }
            }
            return min;
        }

        private void LinkHeaps(FibHeapNode max, FibHeapNode min)
        {
            RemoveNodeFromList(max);
            min.Child = MergeLists(max, min.Child);
            max.Parent = min;
            max.IsMarked = false;
        }

        public void DecreaseKey(FibHeapNode node, decimal weight)
        {
            node.Distance = weight;
            var parent = node.Parent;
            if (parent != null && node.Distance < parent.Distance)
            {
                Cut(node, parent, _min);
                CascadingCut(parent, _min);
            }

            if (node.Distance < _min.Distance)
            {
                _min = node;
            }
        }

        private FibHeapNode Cut(FibHeapNode node, FibHeapNode parent, FibHeapNode min)
        {
            node.Parent = null;
            parent.Degree--;
            if (node.Next == node)
            {
                parent.Child = null;
            }
            else
            {
                parent.Child = node.Next;
            }


            RemoveNodeFromList(node);
            min = MergeLists(min, node);
            node.IsMarked = false;

            return min;
        }

        private FibHeapNode CascadingCut(FibHeapNode node, FibHeapNode min)
        {
            var parent = node.Parent;
            if (parent != null)
            {
                if (node.IsMarked == true)
                {
                    min = Cut(node, parent, min);
                    min = CascadingCut(parent, min);
                }
                else
                {
                    node.IsMarked = true;
                }
            }

            return min;
        }

        public static void RemoveNodeFromList(FibHeapNode node)
        {
            var prev = node.Prev;
            var next = node.Next;
            prev.Next = next;
            next.Prev = prev;
            node.Next = node;
            node.Prev = node;
        }

        public void Delete(FibHeapNode node)
        {
            var parent = node.Parent;
            if (parent != null)
            {
                Cut(node, parent, _min);
                CascadingCut(parent, _min);
            }

            _min = node;

            ExtractMinimum();
        }

        public bool IsEmpty()
        {
            return _min == null;
        }
    }
}
