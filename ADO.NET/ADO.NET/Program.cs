// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ADO.NET
{
    using System;
    using System.Configuration;
    using System.Data.SqlClient;

    internal class Program
    {
        /// <summary>
        /// Connection string.
        /// </summary>
        private string connectionString = ConfigurationManager.ConnectionStrings["NorthwindConnectionString"].ConnectionString;

        /// <summary>
        /// Database connection instance.
        /// </summary>
        private DatabaseConnection databaseConnection;

        /// <summary>
        /// Method that imlements fourth query.
        /// </summary>
        private void FourthQuery()
        {
            Console.WriteLine("First, Last names and Ages of the employees with age greater 55");
            SqlCommand fourthQuery = this.databaseConnection.Connection.CreateCommand();
            string queryText = "SELECT" +
                " FirstName, LastName, Datediff(year, BirthDate, Getdate()) as Age" +
                " FROM Employees" +
                " WHERE Datediff(year, BirthDate, Getdate()) > 55;";
            fourthQuery.CommandText = queryText;

            SqlDataReader reader = fourthQuery.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine("{0} {1} {2}", reader["FirstName"], reader["LastName"], reader["Age"]);
            }

            reader.Close();
            Console.WriteLine();
        }

        /// <summary>
        /// Method that imlements sixth query.
        /// </summary>
        private void SixthQuery()
        {
            Console.WriteLine("Max, Avg, Min age of the employees in London");
            SqlCommand sixthQuery = this.databaseConnection.Connection.CreateCommand();
            string queryText = "SELECT" +
                " Max(Datediff(year, BirthDate, Getdate())) as MaxAge" +
                ", Avg(Datediff(year, BirthDate, Getdate())) as AvgAge" +
                ", Min(Datediff(year, BirthDate, Getdate())) as MinAge" +
                " FROM Employees" +
                " WHERE City = 'London';";
            sixthQuery.CommandText = queryText;

            SqlDataReader reader = sixthQuery.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine("{0} {1} {2}", reader["MaxAge"], reader["AvgAge"], reader["MinAge"]);
            }

            reader.Close();
            Console.WriteLine();
        }

        /// <summary>
        /// Method that imlements seventh query.
        /// </summary>
        private void SeventhQuery()
        {
            Console.WriteLine("Max, Avg, Min age of the employees for each city");
            SqlCommand seventhQuery = this.databaseConnection.Connection.CreateCommand();
            string queryText = "SELECT" +
                " Max(Datediff(year, BirthDate, Getdate())) as MaxAge" +
                ", Avg(Datediff(year, BirthDate, Getdate())) as AvgAge" +
                ", Min(Datediff(year, BirthDate, Getdate())) as MinAge" +
                " FROM Employees" +
                " GROUP BY City;";
            seventhQuery.CommandText = queryText;

            SqlDataReader reader = seventhQuery.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine("{0} {1} {2}", reader["MaxAge"], reader["AvgAge"], reader["MinAge"]);
            }

            reader.Close();
            Console.WriteLine();
        }

        /// <summary>
        /// Method that imlements Eighth query.
        /// </summary>
        private void EighthQuery()
        {
            Console.WriteLine("AverageAge and City of the employees where average age is greater 60");
            SqlCommand eighthQuery = this.databaseConnection.Connection.CreateCommand();
            string queryText = "SELECT" +
                " Avg(Datediff(year, BirthDate, Getdate())) as AvgAge" +
                ", City" +
                " FROM Employees" +
                " GROUP BY City" +
                " HAVING Avg(Datediff(year, BirthDate, Getdate())) > 60;";
            eighthQuery.CommandText = queryText;

            SqlDataReader reader = eighthQuery.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine("{0} {1}", reader["AvgAge"], reader["City"]);
            }

            reader.Close();
            Console.WriteLine();
        }

        /// <summary>
        /// Method that imlements ninth query.
        /// </summary>
        private void NinthQuery()
        {
            Console.WriteLine("First and Last names of the oldest employees");
            SqlCommand ninthQuery = this.databaseConnection.Connection.CreateCommand();
            string queryText = "SELECT" +
                " FirstName, LastName " +
                " FROM Employees" +
                " WHERE Datediff(year, BirthDate, Getdate()) = (SELECT Max(Datediff(year, BirthDate, Getdate())) FROM Employees);";
            ninthQuery.CommandText = queryText;

            SqlDataReader reader = ninthQuery.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine("{0} {1}", reader["FirstName"], reader["LastName"]);
            }

            reader.Close();
            Console.WriteLine();
        }

        private static void Main(string[] args)
        {
            Program program = new Program();

            program.databaseConnection = new DatabaseConnection(program.connectionString);
            program.databaseConnection.Connect();

            program.FourthQuery();
            program.SixthQuery();
            program.SeventhQuery();
            program.EighthQuery();
            program.NinthQuery();

            program.databaseConnection.Disconnect();
            Console.ReadKey();
        }
    }
}
