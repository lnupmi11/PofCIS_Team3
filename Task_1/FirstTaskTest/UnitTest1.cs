using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task_1.Classes;
using System.Collections.Generic;
using Task_1;

namespace FirstTaskTest
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// Path to file for test
        /// </summary>
        private const string filePath = "FileToTest.txt";

        /// <summary>
        /// Count of triangle sides
        /// </summary>
        private const int sidesCount = 3;

        /// <summary>
        /// Color of triangle sides for test
        /// </summary>
        private const string firstSideColor = "red";
        private const string secondSideColor = "green";
        private const string thirdSideColor = "blue";

        /// <summary>
        /// Length of triangle sides for test
        /// </summary>
        private const int firstSideLength = 1;
        private const int secondSideLength = 3;
        private const int thirdSideLength = 3;

        /// <summary>
        /// Test Parse method
        /// </summary>
        [TestMethod]
        public void ParseTestAreEqual()
        {
            Triangle firstTriangle = new Triangle(firstSideColor, firstSideLength, secondSideColor, secondSideLength, thirdSideColor, thirdSideLength);
            string[] splitedTriangleData = { firstSideColor, firstSideLength.ToString(), secondSideColor, secondSideLength.ToString(), thirdSideColor, thirdSideLength.ToString() };
            string triangleData = string.Join(",", splitedTriangleData);
            Triangle secondTriangle = new Triangle();
            secondTriangle.Parse(triangleData);
            Assert.AreEqual(firstTriangle.ToString(), secondTriangle.ToString());
        }

        /// <summary>
        /// Test Perimeter method
        /// </summary>
        [TestMethod]
        public void PerimeterTestAreEqual()
        {
            int customPerimeter = firstSideLength + secondSideLength + thirdSideLength;

            Triangle triangle = new Triangle(firstSideColor, firstSideLength, secondSideColor, secondSideLength, thirdSideColor, thirdSideLength);
            int perimeter = triangle.Perimeter();

            Assert.AreEqual(customPerimeter, perimeter);
        }

        /// <summary>
        /// Test checks for equality of return of method "ToString" and null 
        /// </summary>
        [TestMethod]
        public void ToStringTestIsNotNull()
        {
            Triangle triangle = new Triangle(firstSideColor, firstSideLength, secondSideColor, secondSideLength, thirdSideColor, thirdSideLength);
            string stringTriangleRepresentation = triangle.ToString();
            Assert.IsNotNull(stringTriangleRepresentation);
        }

        /// <summary>
        /// Test MakeUniqueColor method
        /// </summary>
        [TestMethod]
        public void MakeUniqueColorTestAreEqual()
        {
            Triangle triangleWithTwoSameSideColors = new Triangle(firstSideColor, firstSideLength, firstSideColor, secondSideLength, thirdSideColor, thirdSideLength);
            Triangle triangleWithThreeSameSideColors = new Triangle(firstSideColor, firstSideLength, firstSideColor, secondSideLength, firstSideColor, thirdSideLength);

            Triangle remakedTriangleWithTwoSameSideColors = triangleWithTwoSameSideColors.MakeUniqueColor();

            Assert.AreEqual(remakedTriangleWithTwoSameSideColors.ToString(), triangleWithThreeSameSideColors.ToString());
        }

        /// <summary>
        /// Test DistinctColors method
        /// </summary>
        [TestMethod]
        public void DistinctColorsTestIsTrue()
        {
            Triangle triangleWithThreeSameSideColors = new Triangle(firstSideColor, firstSideLength, firstSideColor, secondSideLength, firstSideColor, thirdSideLength);
            List<string> colorsOfTriangle = triangleWithThreeSameSideColors.DistinctColors();
            bool isDistinctsuccessfull = colorsOfTriangle.Count == 1;
            Assert.IsTrue(isDistinctsuccessfull);
        }

        /// <summary>
        /// Test ReadData method
        /// </summary>
        [TestMethod]
        public void ReadDataTestAreEqual()
        {
            SortedList<int, Triangle> resultOfReading = Program.ReadData(filePath);
            Triangle firstTriangleInFile = new Triangle();
            Assert.AreEqual(firstTriangleInFile.ToString(), resultOfReading[firstTriangleInFile.Perimeter()].ToString());
        }

        /// <summary>
        /// Test RePaintSides method
        /// </summary>
        [TestMethod]
        public void RePaintSidesTestIsNotNull()
        {
            Triangle triangleWithTwoSameSideColors = new Triangle(firstSideColor, firstSideLength, firstSideColor, secondSideLength, thirdSideColor, thirdSideLength);
            Triangle triangleWithThreeSameSideColors = new Triangle(firstSideColor, firstSideLength, firstSideColor, secondSideLength, firstSideColor, thirdSideLength);
            List<Triangle> repaintedTriangles = new List<Triangle>();
            repaintedTriangles.Add(triangleWithTwoSameSideColors);
            repaintedTriangles.Add(triangleWithThreeSameSideColors);
            Assert.IsNotNull(repaintedTriangles);
        }
    }
}
