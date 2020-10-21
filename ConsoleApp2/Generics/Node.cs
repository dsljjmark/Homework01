namespace ConsoleApp2.Generics
{
    public class Node<TK, TV>
    {
        public TK Key;
        public TV Value;
        public Node<TK, TV>? Left, Right;

        public Node(TK key, TV value, Node<TK, TV>? left = null, Node<TK, TV>? right = null)
        {
            Key = key;
            Value = value;
            Left = left;
            Right = right;
        }
    }
}