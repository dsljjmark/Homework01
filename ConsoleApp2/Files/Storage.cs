using System;
using System.IO;
using System.Text;
using ConsoleApp2.Interfaces;

namespace ConsoleApp2.Files
{
    public class Storage<T> : IStorage<T> where T : IEndCodingAble<T>, new()
    {
        private readonly string _path;
        private readonly long _blockLength;
        private readonly FileStream _fileStream;

        public Storage(string filename, long maxLength)
        {
            var directory = Path.Join(Environment.CurrentDirectory, "AppData");
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            _path = Path.Join(directory, filename);
            _blockLength = maxLength;
            _fileStream = new FileStream(_path, FileMode.OpenOrCreate);
        }

        private bool IsFileExist() => File.Exists(_path);

        #region Write

        public long Write(T data, long? oldOffset = null)
        {
            var block = ConvertToByte(data);

            var offset = (oldOffset ?? GetOffset());

            using (var writer = new BinaryWriter(_fileStream, Encoding.Default, true))
            {
                writer.BaseStream.Seek(offset, SeekOrigin.Begin);
                writer.Write(block, 0, block.Length);
            }

            return offset;
        }

        public int GetOffset()
        {
            var offset = 0;
            if (IsFileExist()) offset = (int) _fileStream.Length;
            return offset;
        }

        private byte[] ConvertToByte(T data)
        {
            var bytes = data.ToByte();
            return bytes;
        }

        #endregion

        #region Read

        public T Read(long offset)
        {
            if (!IsFileExist())
            {
                return default(T);
            }

            if (((int) _fileStream.Length) == 0)
            {  
                return default(T);
            }
            
            var blockData = new byte[_blockLength];

            using (var reader = new BinaryReader(_fileStream, Encoding.Default, true))
            {
                reader.BaseStream.Seek(offset, SeekOrigin.Begin);
                reader.Read(blockData, 0, (int) _blockLength);
            }

            return ConvertToObj(blockData);
        }

        private T ConvertToObj(byte[] bytes)
        {
            var data = new T();
            data = data.ToData(bytes);
            return data;
        }

        #endregion
    }
}