using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Ado.Net.Utility
{
    /// <summary>
    /// Wrapper for SqlConnection
    /// </summary>
    public interface ISqlDataContext
    {
        public int SaveData(string command, SqlParameter[] parameters);
        public bool DeleteData(string command, SqlParameter[] parameters);
        public int UpdateData(string command, SqlParameter[] parameters);
        public IDataReader RetriveData(string command);
    }
}
