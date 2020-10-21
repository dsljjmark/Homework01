using System;
using System.Collections.Generic;
using ConsoleApp2.Enums;

namespace ConsoleApp2.Generics
{
    public class Tree<TK, TV>
    {
        private readonly Func<TK, TK, CompareValue> _compare;
        private Node<TK, TV>? _link;

        private TK _lastKey;
        //private readonly TK _initStartKey;


        public Tree(Func<TK, TK, CompareValue> compare, TK initStartKey)
        {
            _compare = compare;
            _lastKey = initStartKey;
            //_initStartKey = _initStartKey;
        }

        #region Add

        public void Add(Node<TK, TV> insertData)
        {
            var (parentNode, position) = FindParentNode(_link, insertData);
            InsertNode(parentNode, position, insertData);
            UpdateLastIndex(insertData.Key);
        }

        private void UpdateLastIndex(TK insertDataKey)
        {
            _lastKey = insertDataKey;
        }

        private void InsertNode(Node<TK, TV>? parentNode, Position? position, Node<TK, TV> insertData)
        {
            if (parentNode == null)
            {
                _link = insertData;
                return;
            }

            if (position == Position.Left)
            {
                parentNode.Left = insertData;
                return;
            }

            if (position == Position.Right)
            {
                parentNode.Right = insertData;
                return;
            }

            // no more case
            throw new Exception("no more case");
        }

        private (Node<TK, TV>?, Position?) FindParentNode(Node<TK, TV>? current, Node<TK, TV> insertData)
        {
            if (current == null) return (null, null);

            var compareValue = CompareKey(current.Key, insertData.Key) == CompareValue.More;
            if (compareValue && current.Left == null)
            {
                return (current, Position.Left); // Left less then current
            }

            if (!compareValue && current.Right == null)
            {
                return (current, Position.Right); // Right more then current
            }

            if (compareValue && current.Left != null)
            {
                return FindParentNode(current.Left, insertData); // Left
            }

            if (!compareValue && current.Right != null)
            {
                return FindParentNode(current.Right, insertData); // Right
            }

            // no more case
            throw new Exception("no more case");
        }

        #endregion

        #region Get

        public Node<TK, TV>? GetAll() => _link;

        #endregion

        #region FindByKey

        public Node<TK, TV>? FindByKey(TK key)
        {
            return Search(_link, key);
        }

        private Node<TK, TV>? Search(Node<TK, TV>? current, TK key)
        {
            if (current == null) return null;
            var compareValue = CompareKey(current.Key, key);
            if (compareValue == CompareValue.Equal) return current;

            var isMore = (compareValue == CompareValue.More);

            if (isMore && current.Left == null) return null; // L
            if (isMore && current.Left != null) return Search(current.Left, key); // L

            if (!isMore && current.Right == null) return null; // R
            if (!isMore && current.Right != null) return Search(current.Right, key); // R
            return null;
            // no more case
            // throw new Exception("no more case");
        }

        #endregion

        #region Comparer

        private CompareValue CompareKey(TK currentKey, TK insertDataKey)
        {
            return _compare(currentKey, insertDataKey);
        }

        #endregion

        #region LastKey

        public TK GetLastKey() => _lastKey;

        #endregion

        #region display

        public IEnumerable<TV>? Display()
        {
            return _link == null ? null : _Display(_link);
        }

        private IEnumerable<TV> _Display(Node<TK, TV> current)
        {
            if (current.Left != null)
            {
                foreach (var xx in _Display(current.Left))
                {
                    yield return xx;
                }
            }

            yield return current.Value;

            if (current.Right != null)
            {
                foreach (var xx in _Display(current.Right))
                {
                    yield return xx;
                }
            }
        }

        #endregion
    }
}