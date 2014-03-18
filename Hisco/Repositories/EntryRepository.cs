﻿namespace Hisco.Repositories
{
    using System;
    using System.Collections.Generic;
    using Database;
    using Models;

    public class EntryRepository : IRepository<Entry>
    {
        private readonly IDataAccessor<Entry> _dataAccessor;
        private const string SelectSql = "SELECT id, name, score, created FROM entries ORDER BY score DESC LIMIT 100;";
        private const string InsertSql = "INSERT INTO entries (name, score) VALUES (@name, @score);";

        public EntryRepository(IDataAccessor<Entry> dataAccessor)
        {
            _dataAccessor = dataAccessor;
        }

        public IEnumerable<Entry> Get()
        {
            return _dataAccessor.Read(
                SelectSql,
                null,
                r => new Entry
                {
                    Id = r.GetInt64(0),
                    Name = r.GetString(1),
                    Score = r.GetDecimal(2),
                    Created = r.GetDateTime(3)
                });
        }

        public void Add(Entry entry)
        {
            _dataAccessor.Insert(
                InsertSql,
                new []
                {
                    new Tuple<string, object>("@name", entry.Name), 
                    new Tuple<string, object>("@score", entry.Score)
                });
        }
    }
}