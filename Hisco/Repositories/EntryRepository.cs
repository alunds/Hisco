namespace Hisco.Repositories
{
    using System;
    using System.Collections.Generic;
    using Database;
    using Models;

    public class EntryRepository : IRepository<Entry>
    {
        private readonly IDataAccessor<Entry> _dataAccessor;
        private const string SelectSql = "SELECT level, name, score FROM entries WHERE level = @level ORDER BY score DESC LIMIT 100;";
        private const string InsertSql = "INSERT INTO entries (level, name, score) VALUES (@level, @name, @score);";

        public EntryRepository(IDataAccessor<Entry> dataAccessor)
        {
            _dataAccessor = dataAccessor;
        }

        public IEnumerable<Entry> Get(ushort level)
        {
            return _dataAccessor.Read(
                SelectSql,
                new[]
                {
                    new Tuple<string, object>("@level", level)
                },
                r => new Entry
                {
                    Level = (ushort)r.GetInt16(0),
                    Name = r.GetString(1),
                    Score = (ushort)r.GetInt16(2)
                });
        }

        public void Add(Entry entry)
        {
            _dataAccessor.Insert(
                InsertSql,
                new []
                {
                    new Tuple<string, object>("@level", entry.Level),
                    new Tuple<string, object>("@name", entry.Name),
                    new Tuple<string, object>("@score", entry.Score)
                });
        }
    }
}