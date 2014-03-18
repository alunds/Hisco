namespace Hisco.Database
{
    using System;
    using System.Collections.Generic;
    using System.Data;

    public interface IDataAccessor<T>
    {
        IEnumerable<T> Read(
            string cmdText,
            IEnumerable<Tuple<string, object>> parameters,
            Func<IDataReader, T> readRow);

        void Insert(
            string cmdText,
            IEnumerable<Tuple<string, object>> parameters);
    }
}