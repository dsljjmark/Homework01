using System;
using System.Linq;
using System.Text;
using ConsoleApp2.Extensions;
using ConsoleApp2.Interfaces;

namespace ConsoleApp2.Models
{
    public class Person : IEndCodingAble<Person>
    {
        private const int MaxChar = 255;
        public string Name { get; private set; } = string.Empty;
        public int Age { get; private set; }
        public long Id { get; set; }

        public Person()
        {
         
        }
        
        public Person(string name, int age, long? id = null)
        {
            Id = (id ?? 0);
            Name = name;
            Age = age;
        }


        public override string ToString()
        {
            return  $"Id : {Id} Name : {Name} Age : {Age}";
        }

        public byte[] ToByte()
        {
            
            var idByte = BitConverter.GetBytes(Id);
            var nameBytes = Encoding.UTF32.GetBytes(Name).PaddingRight(MaxChar, Encoding.UTF32);
            var ageByte = BitConverter.GetBytes(Age);
            var result = Combine(new[] {idByte, nameBytes, ageByte});
            return result;
        }

        public Person ToData(byte[] bytes)
        {
            var idBytes = new byte[8];
            var nameBytes = new byte[1020];
            var ageBytes = new byte[4];
            
            Array.Copy(bytes,0,idBytes,0,8);
            Array.Copy(bytes,8,nameBytes,0,1020);
            Array.Copy(bytes,1028,ageBytes,0,4);
            
            this.Id = BitConverter.ToInt32(idBytes);
            this.Name= Encoding.UTF32.GetString(nameBytes).TrimEnd('\0');
            this.Age = BitConverter.ToInt32(ageBytes);
            return this;
        }

        public int GetMaxLenght()
        {
            return 1032;
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
    }
}