namespace Hisco.Repositories
{
    using System.Collections.Generic;

    public interface IRepository<T>
    {
        IEnumerable<T> Get();
        void Add(T item);
    }
}