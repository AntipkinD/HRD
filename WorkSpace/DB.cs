using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkSpace
{
    internal class DB
    {
        SqlConnection connection = new SqlConnection(@"Data Source = DELL-G3-3590\MSSQLSERVER01; Initial Catalog = WorkSpace; Integrated Security = True");
        public void openConnect()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }
        public void closeConnect()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }
        public SqlConnection getConnect()
        {
            return connection;
        }
    }
}
