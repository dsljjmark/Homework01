using System;
using System.Text;

namespace ConsoleApp2.Extensions
{
    public static class ByteExtensions
    {
        public static byte[] PaddingLeft(this byte[] nameBytes, int maxChar, Encoding encoding)
        {
            var byteCount = encoding.GetBytes("1").Length;
            var newArray = new byte[maxChar * byteCount];
            var startIndex = newArray.Length - nameBytes.Length;
            Array.Copy(nameBytes, 0, newArray, startIndex, nameBytes.Length);
            return newArray;
        }       
        public static byte[] PaddingRight(this byte[] nameBytes, int maxChar, Encoding encoding)
        {
            var byteCount = encoding.GetBytes("1").Length;
            var newArray = new byte[maxChar * byteCount];
            Array.Copy(nameBytes, 0, newArray, 0, nameBytes.Length);
            return newArray;
        }
    }
}