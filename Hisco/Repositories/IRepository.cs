namespace Hisco.Repositories
{
    using System.Collections.Generic;

    public interface IRepository<T>
    {
        IEnumerable<T> Get(ushort level);
        void Add(T item);
    }
}