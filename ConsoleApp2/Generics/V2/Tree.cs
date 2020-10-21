using System;
using System.Collections.Generic;
using ConsoleApp2.Enums;
using ConsoleApp2.Interfaces;

namespace ConsoleApp2.Generics.V2
{
    public class Tree<TK, TV> where TV : class, IEndCodingAble<TV>, new()
    {
        private readonly Func<TK, TK, CompareValue> _compare;
        private readonly IStorage<Node<TK, TV>> _indexStorage;
        private readonly bool _canDuplicateKey;
        private readonly TK _initKey;

        private Node<TK, TV>? Root => _indexStorage.Read(0);
        public TK MaxIndex => GetMaxIndex(Root);

        public Tree(
            Func<TK, TK, CompareValue> compare,
            TK initStartKey,
            IStorage<Node<TK, TV>> indexStorage,
            bool canDuplicateKey)
        {
            _compare = compare;
            _initKey = initStartKey;
            _indexStorage = indexStorage;
            _canDuplicateKey = canDuplicateKey;
        }

        #region Add

        public void Add(Node<TK, TV> insertData)
        {
            var (parentNode, position) = FindParentNode(Root, insertData);
            UpdateParentNode(parentNode, position, insertData);
            
            // write new node 
            var newNodeOffset = _indexStorage.Write(insertData, insertData.Current);
            
            // update parent node
            if (parentNode != null)
            {
                // replace parent 
                var parentOffset = _indexStorage.Write(parentNode, parentNode.Current);
            }
        }

        private (Node<TK, TV>?, Position?) FindParentNode(Node<TK, TV>? current, Node<TK, TV> newNode)
        {
            if (current == null) return (null, null);

            var compareValue = CompareKey(current.Key, newNode.Key);
            
            if (_canDuplicateKey)
            {
                // middle
                if (compareValue == CompareValue.Equal && current.Duplicate == null) return (current, Position.Middle);
                if (compareValue == CompareValue.Equal && current.Duplicate != null)
                {
                    return FindParentNode(current.GetDuplicateNode(_indexStorage), newNode); // middle
                }
            }
            
            var isMore = compareValue == CompareValue.More;

            // Left
            if (isMore && current.Left == null)
            {
                return (current, Position.Left); // Left less then current
            }
            // Right
            if (!isMore && current.Right == null)
            {
                return (current, Position.Right); // Right more then current
            }
            
            if (isMore && current.Left != null)
            {
                return FindParentNode(current.GetLeftNode(_indexStorage), newNode); // Left
            }
            
            if (!isMore && current.Right != null)
            {
                return FindParentNode(current.GetRightNode(_indexStorage), newNode); // Right
            }

            // no more case
            throw new Exception("no more case");
        }
        private void UpdateParentNode(Node<TK, TV>? parent, Position? position, Node<TK, TV> newNode)
        {
            if (parent == null)
            {
                //Link = newNode;
                return;
            }

            if (position == Position.Left)
            {
                parent.Left = newNode.Current;
                return;
            }

            if (position == Position.Right)
            {
                parent.Right = newNode.Current;
                return;
            }

            if (position == Position.Middle)
            {
                parent.Duplicate = newNode.Current;
                return;
            }

            // no more case
            throw new Exception("no more case");
        }
        #endregion
        
        #region FindByKey
        public IEnumerable<Node<TK, TV>>? FindByKey(TK key)
        {
            var rootNode = Search(Root, key);
            if (rootNode is null) return null;
            if (rootNode.Duplicate is null) return new Node<TK, TV>[1] {rootNode};
            var allNode = GetDuplicate(rootNode);
            return allNode;
        }
        
        private IEnumerable<Node<TK, TV>> GetDuplicate(Node<TK, TV> current)
        {
            if (current.Duplicate is not null)
            {
                foreach (var data in GetDuplicate(current.GetDuplicateNode(_indexStorage)))
                {
                    yield return data;
                }
            }
            yield return current;
        }
        private Node<TK, TV>? Search(Node<TK, TV>? current, TK key)
        {
            if (current == null) return null;
            var compareValue = CompareKey(current.Key, key);
            if (compareValue == CompareValue.Equal) return current;
        
            var isMore = (compareValue == CompareValue.More);
        
            if (isMore && current.Left == null) return null; // L
            if (isMore && current.Left != null) return Search(current.GetLeftNode(_indexStorage), key); // L
        
            if (!isMore && current.Right == null) return null; // R
            if (!isMore && current.Right != null) return Search(current.GetRightNode(_indexStorage), key); // R
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
        
        #region Display
        
        public  IEnumerable<Node<TK, TV>>? GetAll()
        {
            if (Root == null) return null;
            var result = GetAllNode(Root);
            return result;
        }
        
        private IEnumerable<Node<TK, TV>> GetAllNode(Node<TK, TV> current)
        {
            if (current.Left != null)
            {
                foreach (var left in GetAllNode(current.GetLeftNode(_indexStorage)))
                {
                    yield return left;
                    if (left.Duplicate != null)
                    {
                        foreach (var dup in GetDuplicate(left.GetDuplicateNode(_indexStorage)))
                        {
                            yield return dup;
                        }
                    }
                }
            }
        
            yield return current;
        
            if (current.Right != null)
            {
                foreach (var right in GetAllNode(current.GetRightNode(_indexStorage)))
                {
                    yield return right;
                    if (right.Duplicate != null)
                    {
                        foreach (var dup in GetDuplicate(right.GetDuplicateNode(_indexStorage)))
                        {
                            yield return dup;
                        }
                    }
                }
            }
        }
        
         #endregion

         private TK GetMaxIndex(Node<TK, TV>? current)
         {
             if (current == null) return _initKey;
             if (current.Right == null) return current.Key;
             if (current.Right != null) return GetMaxIndex(current.GetRightNode(_indexStorage));
             throw new Exception("no more case");
         }
    }
}