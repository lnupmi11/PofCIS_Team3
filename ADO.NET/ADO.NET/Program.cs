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
        private string connectionString = ConfigurationManager.ConnectionStrings["NorthwindConnectionString"].ConnectionString;
        private SqlConnection connection;

        private void FourthQuery()
        {
            Console.WriteLine("First, Last names and Ages of the employees with age greater 55");
            SqlCommand fourthQuery = this.connection.CreateCommand();
            fourthQuery.CommandText = "SELECT" +
                " FirstName, LastName, Datediff(year, BirthDate, Getdate()) as Age" +
                " FROM Employees" +
                " WHERE Datediff(year, BirthDate, Getdate()) > 55;";

            SqlDataReader reader = fourthQuery.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine("{0} {1} {2}", reader["FirstName"], reader["LastName"], reader["Age"]);
            }

            reader.Close();
            Console.WriteLine();
        }

        private void SixthQuery()
        {
            Console.WriteLine("Max, Avg, Min age of the employees in London");
            SqlCommand sixthQuery = this.connection.CreateCommand();
            sixthQuery.CommandText = "SELECT" +
                " Max(Datediff(year, BirthDate, Getdate())) as MaxAge" +
                ", Avg(Datediff(year, BirthDate, Getdate())) as AvgAge" +
                ", Min(Datediff(year, BirthDate, Getdate())) as MinAge" +
                " FROM Employees" +
                " WHERE City = 'London';";

            SqlDataReader reader = sixthQuery.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine("{0} {1} {2}", reader["MaxAge"], reader["AvgAge"], reader["MinAge"]);
            }

            reader.Close();
            Console.WriteLine();
        }

        private void SeventhQuery()
        {
            Console.WriteLine("Max, Avg, Min age of the employees for each city");
            SqlCommand seventhQuery = this.connection.CreateCommand();
            seventhQuery.CommandText = "SELECT" +
                " Max(Datediff(year, BirthDate, Getdate())) as MaxAge" +
                ", Avg(Datediff(year, BirthDate, Getdate())) as AvgAge" +
                ", Min(Datediff(year, BirthDate, Getdate())) as MinAge" +
                " FROM Employees" +
                " GROUP BY City;";

            SqlDataReader reader = seventhQuery.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine("{0} {1} {2}", reader["MaxAge"], reader["AvgAge"], reader["MinAge"]);
            }

            reader.Close();
            Console.WriteLine();
        }

        private void EighthQuery()
        {
            Console.WriteLine("AverageAge and City of the employees where average age is greater 60");
            SqlCommand eighthQuery = this.connection.CreateCommand();
            eighthQuery.CommandText = "SELECT" +
                " Avg(Datediff(year, BirthDate, Getdate())) as AvgAge" +
                ", City" +
                " FROM Employees" +
                " GROUP BY City" +
                " HAVING Avg(Datediff(year, BirthDate, Getdate())) > 60;";

            SqlDataReader reader = eighthQuery.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine("{0} {1}", reader["AvgAge"], reader["City"]);
            }

            reader.Close();
            Console.WriteLine();
        }

        private void NinthQuery()
        {
            Console.WriteLine("First and Last names of the oldest employees");
            SqlCommand ninthQuery = this.connection.CreateCommand();
            ninthQuery.CommandText = "SELECT" +
                " FirstName, LastName " +
                " FROM Employees" +
                " WHERE Datediff(year, BirthDate, Getdate()) = (SELECT Max(Datediff(year, BirthDate, Getdate())) FROM Employees);";

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

            program.connection = new SqlConnection(program.connectionString);
            program.connection.Open();

            program.FourthQuery();
            program.SixthQuery();
            program.SeventhQuery();
            program.EighthQuery();
            program.NinthQuery();

            Console.ReadKey();
        }
    }
}
