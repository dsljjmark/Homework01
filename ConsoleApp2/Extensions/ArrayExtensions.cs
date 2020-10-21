using System;

namespace ConsoleApp2.Extensions
{
    public static class ArrayExtensions
    {
        public static T[] Append<T>(this T[] array, T append)
        {
            Array.Resize(ref array, array.Length + 1);
            array[array.Length - 1] = append; // < Adds an extra element to my array
            return array;
        }    
  
    
        // public static T[] Combine<T>(this T[] array, T append)
        // {
        //     // Array.Resize(ref array, array.Length + 1);
        //     // array[array.Length - 1] = append; // < Adds an extra element to my array
        //     // return array;
        // }
    }
}