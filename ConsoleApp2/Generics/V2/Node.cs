using System;
using System.Linq;
using System.Text;
using ConsoleApp2.Enums;
using ConsoleApp2.Extensions;
using ConsoleApp2.Interfaces;

namespace ConsoleApp2.Generics.V2
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TK"> TK can be int or string and if key is string maximum is 255 </typeparam>
    /// <typeparam name="TV"> must be class</typeparam>
    public class Node<TK, TV> : IEndCodingAble<Node<TK, TV>> where TV : class, IEndCodingAble<TV>, new()
    { 
        public TK Key { get; set; }
        // ref index
        public long Current { get; set; }
        // ref index
        public long? Left { get; set; } = null;
        // ref index
        public long? Right { get; set; } = null; 
        // ref index
        public long? Duplicate { get; set; } = null;
        
        // ref data
        public long RefCurrent { get; set; }
        public Node()
        {
            FindKeyType();
        }
        
        // get index storage
        public Node<TK, TV>? GetLeftNode(IStorage<Node<TK, TV>> storage)
        {
            return Left is null ? null : storage.Read(this.Left.Value);
        }

        // get index storage
        public Node<TK, TV>? GetRightNode(IStorage<Node<TK, TV>> storage)
        {
            return Right is null ? null : storage.Read(this.Right.Value);
        }

        // get index storage
        public Node<TK, TV>? GetDuplicateNode(IStorage<Node<TK, TV>> storage)
        {
            return Duplicate is null ? null : storage.Read(this.Duplicate.Value);
        }

        //get value storage
        public TV GetCurrentObject(IStorage<TV> storage)
        {
            return storage.Read(RefCurrent);
        }

        public byte[] ToByte()
        {
            var key = KeyToByte(Key);
            var refCurrent = BitConverter.GetBytes(RefCurrent);
            
            var isLeft = Left is null ? new Byte[] {0} : new Byte[] {1};
            var left = BitConverter.GetBytes(Left ?? 0);

            var isRight = Right is null ? new Byte[] {0} : new Byte[] {1};
            var right = BitConverter.GetBytes(Right ?? 0);

            var isDuplicate = Duplicate is null ? new Byte[] {0} : new Byte[] {1};
            var duplicate = BitConverter.GetBytes(Duplicate ?? 0);

            var current = BitConverter.GetBytes(Current);
            
            var result = Combine(new[] {key, refCurrent, isLeft, left, isRight, right, isDuplicate, duplicate,current});
            return result;
        }

        private static byte[] Combine(params byte[][] arrays)
        {
            byte[] rv = new byte[arrays.Sum(a => a.Length)];
            int offset = 0;
            foreach (byte[] array in arrays)
            {
                Buffer.BlockCopy(array, 0, rv, offset, array.Length);
                offset += array.Length;
            }
            
            return rv;
        }

        public Node<TK, TV> ToData(byte[] bytes)
        {
            var key = new byte[GetKeyMaxLenght()];
            var refCurrent = new byte[8];
            
            var isLeft = new byte[1];
            var left = new byte[8];
            
            var isRight = new byte[1];
            var right = new byte[8];
            
            var isDuplicate = new byte[1];
            var duplicate = new byte[8];
            
            var current = new byte[8];
            
            var cursor = 0;
            Array.Copy(bytes, 0, key, 0, GetKeyMaxLenght());
            
            cursor = GetKeyMaxLenght();
            Array.Copy(bytes,cursor,refCurrent,0,8);
           
            cursor = cursor + 8;
            Array.Copy(bytes,cursor,isLeft,0,1);
           
            cursor = cursor + 1;
            Array.Copy(bytes,cursor,left,0,8);
            
            cursor = cursor + 8;
            Array.Copy(bytes,cursor,isRight,0,1);
            
            cursor = cursor + 1;
            Array.Copy(bytes,cursor,right,0,8);
            
            cursor = cursor + 8; 
            Array.Copy(bytes,cursor,isDuplicate,0,1);
            
            cursor = cursor + 1; 
            Array.Copy(bytes,cursor,duplicate,0,8); 
            
            cursor = cursor + 8; 
            Array.Copy(bytes,cursor,current,0,8);

            
            this.Key = KeyToData(key);
            this.RefCurrent = BitConverter.ToInt64(refCurrent);
            this.Left = isLeft[0] == 1 ? (long?) BitConverter.ToInt64(left) : null;
            this.Right = isRight[0] == 1 ? (long?) BitConverter.ToInt64(right) : null;
            this.Duplicate = isDuplicate[0] == 1 ? (long?) BitConverter.ToInt64(duplicate) : null;
            this.Current = BitConverter.ToInt64(current);
            return this;
        }

        public int GetMaxLenght()
        {
            // key                    // refCurrent // isLeft  // left //isRight  //right  //isDuplicate  //duplicate // current
            return GetKeyMaxLenght()      + 8       + 1        + 8     + 1      + 8        + 1           + 8            +8;

            // base =43
        }
        
        #region Key

        private KeyType KeyType { get; set; } = KeyType.Int;
        private readonly int _maxChar = 255;
        private int GetKeyMaxLenght()
        {
            switch (KeyType)
            {
                case KeyType.Long:
                    return 8;
                case KeyType.Int:
                    return 4;
                case KeyType.String:
                    return 1020;
                default:
                    throw new InvalidOperationException("TK Type not supported");
            }
        }
        private TK KeyToData(Byte[] bytes)
        {
            switch (KeyType)
            {
                case KeyType.Int:
                    return (TK) (object) BitConverter.ToInt32(bytes);
                case KeyType.Long:
                    return (TK) (object) BitConverter.ToInt64(bytes);
                case KeyType.String:
                {
                    var key = Encoding.UTF32.GetString(bytes).TrimEnd('\0');
                    return (TK) (object) key;
                }
                default:
                    throw new InvalidOperationException("TK Type not supported");
            }
        }

        private byte[] KeyToByte(TK key)
        {
            switch (KeyType)
            {
                case KeyType.Long:
                {
                    var longKey = Cast.To<long>(key);
                    var bytes =  BitConverter.GetBytes(longKey);
                    return bytes;
                }
                case KeyType.Int:
                {
                    var intKey = Cast.To<int>(key);
                    var bytes =  BitConverter.GetBytes(intKey);
                    return bytes;
                }
                case KeyType.String:
                {
                    var stringKey = Cast.To<string>(key);
                    var bytes = Encoding.UTF32.GetBytes(stringKey).PaddingRight(_maxChar, Encoding.UTF32);
                    return bytes;
                }
                default:
                    throw new InvalidOperationException("TK Type not supported");
            }
        }

        private void FindKeyType()
        {
            var type = typeof(TK);
            if (typeof(TK) == typeof(string))
            {
                KeyType = KeyType.String;
                return;
            }
            if (typeof(TK) == typeof(int))
            {
                KeyType = KeyType.Int;
                return;
            }           
            if (typeof(TK) == typeof(long))
            {
                KeyType = KeyType.Long;
                return;
            }
            throw new InvalidOperationException("TK Type not supported");
        }
        
        #endregion
     
    }
}