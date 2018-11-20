using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WpfApp;
using WpfApp.Models;
using WpfApp.BL;
using System.Collections.Generic;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// List of orders
        /// </summary>
        private List<Order> ordersList = new List<Order>()
        {
            new Order(6, 65, "1hr 2min", "shop", "+23424", "done"),
            new Order(1, 80, "12hr 32min", "university", "+12341", "already assigned"),
            new Order(5, 160, "15hr 46min", "stadium", "+512341", "already assigned"),
            new Order(7, 120, "3hr 54min", "center", "+9234", "already assigned"),
            new Order(2, 120, "17hr 00min", "Forum", "+83234", "not assigned"),
            new Order(3, 120, "14hr 23min", "Horodotska 20", "+52342", "not assigned"),
        };


        /// <summary>
        /// List of drivers
        /// </summary>
        private List<TaxiDriver> driversList = new List<TaxiDriver>()
        {
            new TaxiDriver(1, "Petro", 2,  new List<int>() { 5, 6 } ),
            new TaxiDriver(2, "Vasyl", 2,  new List<int>() { 1, 7 } )
        };


        /// <summary>
        /// TaxiDriverToString method Test
        /// </summary>
        [TestMethod]
        public void TaxiDriverToStringTest1()
        {
            TaxiDriver dr = new TaxiDriver(4, "Andriy", 0, new System.Collections.Generic.List<int>());
            Assert.AreEqual(dr.ToString(), "4 Andriy 0");
        }


        /// <summary>
        /// TaxiDriverToString method Test
        /// </summary>
        [TestMethod]
        public void TaxiDriverToStringTest2()
        {
            TaxiDriver dr = new TaxiDriver(4, "Andriy", 10, new System.Collections.Generic.List<int>());
            Assert.ThrowsException<System.ArgumentOutOfRangeException>(() => dr.ToString());
        }


        /// <summary>
        /// TaxiDriverToString method Test
        /// </summary>
        [TestMethod]
        public void TaxiDriverToStringTest3()
        {
            TaxiDriver dr = new TaxiDriver(44, "Andriy", 0, new System.Collections.Generic.List<int>());
            Assert.AreNotEqual(dr.ToString(), "4 Andriy 0");
        }


        /// <summary>
        /// TaxiDriverToString method Test
        /// </summary>
        [TestMethod]
        public void TaxiDriverToStringTest4()
        {
            var orders = new System.Collections.Generic.List<int>();
            orders.Add(1);
            orders.Add(7);
            orders.Add(4);
            TaxiDriver dr = new TaxiDriver(4, "Andriy", 3, orders);
            Assert.AreEqual(dr.ToString(), "4 Andriy 3 1 7 4");
        }


        /// <summary>
        /// TaxiDriverToString method Test
        /// </summary>
        [TestMethod]
        public void TaxiDriverToStringTest5()
        {
            var orders = new System.Collections.Generic.List<int>();
            orders.Add(1);
            orders.Add(7);
            orders.Add(4);
            TaxiDriver dr = new TaxiDriver(4, "Andriy", 2, orders);
            Assert.AreEqual(dr.ToString(), "4 Andriy 2 1 7");
        }


        /// <summary>
        ///  OrderToString method Test
        /// </summary>
        [TestMethod]
        public void OrderToStringTest1()
        {
            Order or = new Order(6, 65, "1hr 2min", "shop", "+23424", "done");
            Assert.AreEqual(or.ToString(), "6 65 1hr 2min shop +23424 done");
        }


        /// <summary>
        ///  OrderToString method Test
        /// </summary>
        [TestMethod]
        public void OrderToStringTest2()
        {
            Order or = new Order(-0, 65, "1hr 2min", "shop", "+23424", "done");
            Assert.AreNotEqual(or.ToString(), "-0 65 1hr 2min shop +23424 done");
        }


        /// <summary>
        ///  OrderGetAssigned method Test
        /// </summary>
        [TestMethod]
        public void OrderGetAssignedTest1()
        {
            Order or = new Order(0, 65, "1hr 2min", "shop", "+23424", "done");
            Assert.ThrowsException<System.Exception>(() => or.GetAssigned());
        }


        /// <summary>
        ///  OrderGetAssigned method Test
        /// </summary>
        [TestMethod]
        public void OrderGetAssignedTest2()
        {
            Order or = new Order(0, 65, "1hr 2min", "shop", "+23424", "not assigned");
            or.GetAssigned();
            Assert.AreEqual(or.Status, "already assigned");
        }


        /// <summary>
        ///  ConectOrderAndDriver method Test
        /// </summary>
        [TestMethod]
        public void ConectOrderAndDriverTest()
        {
            var or = new Order(0, 65, "1hr 2min", "shop", "+23424", "already assigned");
            var dr = new TaxiDriver(0, "Oleg", 0, new List<int>());
            BL logic = new BL();
            Assert.ThrowsException<System.Exception>(() => logic.ConectOrderAndDriver(ref or, ref dr));
        }


        /// <summary>
        /// AssignOrder method Test
        /// </summary>
        [TestMethod]
        public void AssignOrderTest()
        {
            var or = new Order(0, 65, "1hr 2min", "shop", "+23424", "not assigned");
            var dr = new TaxiDriver(0, "Oleg", 0, new List<int>());
            dr.AssignOrder(or);
            Assert.AreEqual(dr.CountOfOrders, 1);

        }


        /// <summary>
        /// FindTaxiDriverById method Test
        /// </summary>
        [TestMethod]
        public void FindTaxiDriverByIdTest()
        {
            BL logic = new BL();
            logic.TaxiDrivers = driversList;
            logic.Orders = ordersList;
            Assert.AreEqual(logic.FindTaxiDriverById(2), driversList[1]);
        }


        /// <summary>
        /// FindTaxiDriverByName method Test
        /// </summary>
        [TestMethod]
        public void FindTaxiDriverByNameTest()
        {
            BL logic = new BL();
            logic.TaxiDrivers = driversList;
            logic.Orders = ordersList;
            Assert.AreEqual(logic.FindTaxiDriverByName("Petro"), driversList[0]);
        }


        /// <summary>
        /// FindOrderById method Test
        /// </summary>
        [TestMethod]
        public void FindOrderByIdTest()
        {
            BL logic = new BL();
            logic.TaxiDrivers = driversList;
            logic.Orders = ordersList;
            Assert.AreEqual(logic.FindOrderById(5), ordersList[2]);
        }


        /// <summary>
        /// FindOrderByStatus method Test
        /// </summary>
        [TestMethod]
        public void FindOrderByStatusTest()
        {
            BL logic = new BL();
            logic.TaxiDrivers = driversList;
            logic.Orders = ordersList;
            List<Order> expectedResult = new List<Order>();
            expectedResult.Add(ordersList[1]);
            var functionResult = logic.FindOrderByStatus("already assigned");
            Assert.AreEqual(functionResult.Count, expectedResult.Count);
            for (var i = 0; i < functionResult.Count; ++i)
            {
                Assert.AreEqual(functionResult[i].ToString(), expectedResult[i].ToString());
            }
        }



    }
}
