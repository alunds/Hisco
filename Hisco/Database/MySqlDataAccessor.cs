namespace Hisco.Database
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Linq;
    using MySql.Data.MySqlClient;

    public class MySqlDataAccessor<T> : IDataAccessor<T>
    {
        private readonly string _connStr = ConfigurationManager.ConnectionStrings["mySql"].ToString();

        public IEnumerable<T> Read(
            string cmdText,
            IEnumerable<Tuple<string, object>> parameters,
            Func<IDataReader, T> readRow)
        {
            using (var conn = new MySqlConnection(_connStr))
            {
                using (var cmd = new MySqlCommand(cmdText, conn))
                {
                    conn.Open();

                    if (parameters != null)
                        parameters.ToList().ForEach(x => cmd.Parameters.AddWithValue(x.Item1, x.Item2));
                    
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                            yield return readRow(dr);

                        dr.Close();
                    }

                    conn.Close();
                }
            }
        }

        public void Insert(
            string cmdText,
            IEnumerable<Tuple<string, object>> parameters)
        {
            using (var conn = new MySqlConnection(_connStr))
            {
                using (var cmd = new MySqlCommand(cmdText, conn))
                {
                    conn.Open();

                    if (parameters != null)
                        parameters.ToList().ForEach(x => cmd.Parameters.AddWithValue(x.Item1, x.Item2));

                    cmd.ExecuteNonQuery();

                    conn.Close();
                }
            }
        }
    }
}