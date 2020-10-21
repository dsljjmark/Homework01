namespace ConsoleApp2.Interfaces
{
    public interface IEndCodingAble<T>
    {
        byte[] ToByte();
        T ToData(byte[] bytes);
        int GetMaxLenght();
    }
}