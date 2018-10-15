using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WpfShapes.Utils;
using System.Windows.Media;

namespace WPFTests
{
    [TestClass]
    public class UnitTests
    {
        private const double firstPointCoordinate = 0.0;
        private const double secondPointCoordinate = 1.0;

        [TestMethod]
        public void OnSegmentTestIsTrue()
        {
            System.Windows.Point firstPoint = new System.Windows.Point(firstPointCoordinate, firstPointCoordinate);
            System.Windows.Point secondPoint = new System.Windows.Point(firstPointCoordinate + secondPointCoordinate / 2, firstPointCoordinate + secondPointCoordinate / 2);
            System.Windows.Point thirdPoint = new System.Windows.Point(secondPointCoordinate, secondPointCoordinate);
            
            Assert.IsTrue(Util.OnSegment(firstPoint, secondPoint, thirdPoint));
        }

        [TestMethod]
        public void OnSegmentTestIsFalse()
        {
            System.Windows.Point firstPoint = new System.Windows.Point(firstPointCoordinate, firstPointCoordinate);
            System.Windows.Point secondPoint = new System.Windows.Point(firstPointCoordinate - secondPointCoordinate / 2, firstPointCoordinate - secondPointCoordinate / 2);
            System.Windows.Point thirdPoint = new System.Windows.Point(secondPointCoordinate, secondPointCoordinate);

            Assert.IsFalse(Util.OnSegment(firstPoint, secondPoint, thirdPoint));
        }

        [TestMethod]
        public void OrientationTestAreEqual()
        {
            System.Windows.Point firstPoint = new System.Windows.Point(firstPointCoordinate, firstPointCoordinate);
            System.Windows.Point secondPoint = new System.Windows.Point(firstPointCoordinate + secondPointCoordinate / 2, firstPointCoordinate + secondPointCoordinate / 2);
            System.Windows.Point thirdPoint = new System.Windows.Point(secondPointCoordinate, secondPointCoordinate);

            Assert.AreEqual(Util.Orientation(firstPoint, secondPoint, thirdPoint), 0);
        }

        [TestMethod]
        public void PointTestAreEqual()
        {
            WpfShapes.Classes.Point firstPoint = new WpfShapes.Classes.Point();
            firstPoint.X = firstPointCoordinate;
            firstPoint.Y = firstPointCoordinate;
            WpfShapes.Classes.Point secondPoint = new WpfShapes.Classes.Point(firstPointCoordinate, firstPointCoordinate);

            Assert.AreEqual(firstPoint.X, secondPoint.X);
        }

        [TestMethod]
        public void ReadHexagonsTestIsNull()
        {
            Assert.IsNotNull(Serialization.Deserialize(""));
        }
    }
}
