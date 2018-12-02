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

        /// <summary>
        /// Database connection instance.
        /// </summary>
        
        /// <summary>
        /// Method that imlements fourth query.
        /// </summary>
        
        private static void Main(string[] args)
        {
            Console.WriteLine("Type-in query index:\n");
            int x = Int32.Parse(Console.ReadLine());
            Queries q = null;
            try
            {
                q = new Queries();
                q.AskQuery(x);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                q.Close();
            }
            Console.ReadKey();
        }
    }
}
