using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET
{
    class Queries
    {
        string connectionString = ConfigurationManager.ConnectionStrings["NorthwindConnectionString"].ConnectionString;

        SqlConnection connection;

        SqlCommand command;

        SqlDataReader reader;

        public Queries()
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
        }
        /// <summary>
        /// Method that closes connection.
        /// </summary>
        public void Close()
        {
            connection.Close();
        }


        /// <summary>
        /// Method that imlements query. 
        /// </summary>
        public void AskQuery(int idOfCommand)
        {
            string[] commands = new string[36];
            commands[1] = "SELECT * FROM Employees WHERE EmployeeID = 8";
            commands[2] = "SELECT FirstName,LastName FROM Employees WHERE City = 'London'";
            commands[3] = "SELECT FirstName,LastName FROM Employees WHERE FirstName Like 'A%'";
            commands[4] = "SELECT" +
                " FirstName, LastName, Datediff(year, BirthDate, Getdate()) as Age" +
                " FROM Employees" +
                " WHERE Datediff(year, BirthDate, Getdate()) > 55;";
            commands[5] = "SELECT Count(EmployeeID) as AmountOfLondoners FROM Employees WHERE City = 'London'";
            commands[6] = "SELECT" +
                " Max(Datediff(year, BirthDate, Getdate())) as MaxAge" +
                ", Avg(Datediff(year, BirthDate, Getdate())) as AvgAge" +
                ", Min(Datediff(year, BirthDate, Getdate())) as MinAge" +
                " FROM Employees" +
                " WHERE City = 'London';";
            commands[7] = "SELECT" +
                " Max(Datediff(year, BirthDate, Getdate())) as MaxAge" +
                ", Avg(Datediff(year, BirthDate, Getdate())) as AvgAge" +
                ", Min(Datediff(year, BirthDate, Getdate())) as MinAge" +
                " FROM Employees" +
                " GROUP BY City;";
            commands[8] = "SELECT" +
                " Avg(Datediff(year, BirthDate, Getdate())) as AvgAge" +
                ", City" +
                " FROM Employees" +
                " GROUP BY City" +
                " HAVING Avg(Datediff(year, BirthDate, Getdate())) > 60;";
            commands[9] = "SELECT FirstName, LastName FROM Employees" +
                " WHERE Datediff(year, BirthDate, Getdate()) = (SELECT Max(Datediff(year, BirthDate, Getdate())) FROM Employees)";
            commands[10] = "select Customers.ContactName from Customers " +
                            " where Customers.CustomerID in " +
                            "(select CustomerID from Orders where OrderID in " +
                            "(select OrderID from[Order Details] inner join Products on[Order Details].ProductID = Products.ProductID and Products.ProductName = 'Tofu'))";
            commands[12] = "SELECT FirstName, LastName, BirthDate FROM Employees WHERE Datediff(month, BirthDate, Getdate()) = 0;";
            commands[14] = "SELECT  (FirstName + ' ' + LastName) as Name , Count(Orders.EmployeeID) as OrdersCount FROM Employees " +
                "left join  Orders on Employees.EmployeeID = Orders.EmployeeID" +
                " group by(FirstName + ' ' + LastName)";
            commands[16] = "SELECT  (FirstName + ' ' + LastName) as Name, Count(Orders.EmployeeID) as OrdersCount FROM Employees " +
                "left join  Orders on Employees.EmployeeID = Orders.EmployeeID and Orders.RequiredDate > Orders.ShippedDate and(Orders.ShippedDate between '1997-01-01' and '1997-12-31') " +
                "group by(FirstName + ' ' + LastName)";
            commands[17] = "SELECT(Customers.ContactName) as Name, Count(Orders.CustomerID) as OrdersCount FROM Customers " +
            "inner join Orders on Orders.CustomerID = Customers.CustomerID  and Customers.Country = 'France'" +
                " group by Customers.ContactName";
            commands[18] = "SELECT  (Customers.ContactName) as Name  , Count(Orders.CustomerID) as OrdersCount FROM Customers " +
                "inner join Orders on Orders.CustomerID = Customers.CustomerID  and Country = 'France'" +
                " group by Customers.ContactName having Count(Orders.CustomerID) > 1";
            commands[19] = "SELECT  (Customers.ContactName) as Name  , Count(Orders.CustomerID) as OrdersCount FROM Customers " +
                    "inner join Orders on Orders.CustomerID = Customers.CustomerID  and Country = 'France'" +
                    " group by Customers.ContactName having Count(Orders.CustomerID) > 1";
            if (idOfCommand >= 1 && idOfCommand <= 36)
            {
                SqlCommand command = new SqlCommand(commands[idOfCommand], connection);
                SqlDataReader reader = command.ExecuteReader();
                Console.WriteLine("Result:");
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.Write(reader.GetName(i) + ":\t" + reader.GetValue(i));
                        Console.WriteLine();
                    }
                    Console.WriteLine();
                }
                reader.Close();
            }
        }
    }
}
