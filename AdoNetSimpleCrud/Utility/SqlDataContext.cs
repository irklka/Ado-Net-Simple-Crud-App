using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Ado.Net.Config;

namespace Ado.Net.Utility
{
    /// <summary>
    /// interacts with .mdf database.
    /// </summary>
    public class SqlDataContext : ISqlDataContext
    {
        private readonly SqlConnection _connection;
        public SqlDataContext(IDatabaseConfig config, string connStringName)
        {
            _connection = new SqlConnection(config.GetDbConnectionString(connStringName));
        }

        public int SaveData(string command, SqlParameter[] parameters)
        {
            var cmd = new SqlCommand(command, this._connection);

            int count = 0;

            cmd.Parameters.AddRange(parameters);
            cmd.Connection.Open();
            count = cmd.ExecuteNonQuery();

            cmd.Dispose();
            _connection.Close();

            return count;
        }
        public int UpdateData(string command, SqlParameter[] parameters)
        {
            var cmd = new SqlCommand(command, this._connection);

            int updated = 0;

            cmd.Parameters.AddRange(parameters);
            cmd.Connection.Open();
            updated += cmd.ExecuteNonQuery();

            cmd.Dispose();
            _connection.Close();

            return updated;
        }
        public bool DeleteData(string command, SqlParameter[] parameters)
        {
            var cmd = new SqlCommand(command, this._connection);

            bool deleted = false;

            cmd.Parameters.AddRange(parameters);
            cmd.Connection.Open();
            deleted = cmd.ExecuteNonQuery() > 0;

            cmd.Dispose();
            _connection.Close();

            return deleted;
        }
        public IDataReader RetriveData(string command)
        {
            var cmd = new SqlCommand(command, this._connection);

            cmd.Connection.Open();
            SqlDataAdapter sde = new SqlDataAdapter(command, this._connection);
            DataTable ds = new DataTable();
            sde.Fill(ds);

            return ds.CreateDataReader();
        }
    }
}
