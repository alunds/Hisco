namespace Hisco.Repositories
{
    using System;
    using System.Collections.Generic;
    using Database;
    using Models;

    public class EntryRepository : IRepository<Entry>
    {
        private readonly IDataAccessor<Entry> _dataAccessor;
        private const string SelectSql = "SELECT id, name, score, created FROM entries WHERE level = @level ORDER BY score DESC LIMIT 100;";
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
                    Id = r.GetInt64(0),
                    Name = r.GetString(2),
                    Score = r.GetDecimal(3),
                    Created = r.GetDateTime(4)
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