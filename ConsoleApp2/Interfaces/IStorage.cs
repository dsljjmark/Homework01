namespace ConsoleApp2.Interfaces
{
    public interface IStorage<T> where T : IEndCodingAble<T>, new()
    {
        long Write(T data, long? oldOffset = null);
        T Read(long offset);
        int GetOffset();
    }
}