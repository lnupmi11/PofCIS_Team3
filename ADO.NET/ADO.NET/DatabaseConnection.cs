﻿namespace ADO.NET
{
    using System.Data;
    using System.Data.SqlClient;

    internal class DatabaseConnection
    {
        private readonly string connectionString;

        private SqlConnection connection;

        public DatabaseConnection(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public SqlConnection Connection { get { return this.connection; } }

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