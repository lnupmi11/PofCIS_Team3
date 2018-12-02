using System;
using System.Data;
using System.Data.SqlClient;

namespace ADO.NET.Database
{
    internal class Database
    {
        private readonly string connectionString;

        private SqlConnection connection;

        public Database(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool Connect()
        {
            this.connection = new SqlConnection(this.connectionString);
            this.connection.Open();
            if (this.connection.State == ConnectionState.Open)
            {
                return true;
            }
            return false;
        }

        public bool Disconnect()
        {
            if (this.connection.State == ConnectionState.Open)
            {
                this.connection.Close();
            }
            return this.connection.State == ConnectionState.Closed;
        }
    }
}
