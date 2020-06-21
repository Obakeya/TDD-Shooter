
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.AppContainer;
using TDD_Shooter.Model;
using Windows.UI.Xaml.Media.Imaging;

namespace TDD_Shooter.Tests
{
    [TestClass]
    public class ShipTest
    {
         const double shipWidth = 60;
        const double shipHeight = 60;

        [UITestMethod]
        public void ShipImage()
        {
            Ship ship = new Ship();
            Assert.IsNotNull(ship.Source);
            Assert.IsInstanceOfType(ship.Source, typeof(BitmapImage));
        }

        [UITestMethod]
        public void ShipPos()
        {
            Ship ship = new Ship();
            ship.X = 100;
            ship.Y = 200;
            Assert.AreEqual(100, ship.X);
            Assert.AreEqual(200, ship.Y);

        }

        [UITestMethod]
        public void ShipSize()
        {
            Ship ship = new Ship();
            Assert.AreEqual(shipWidth, ship.Width);
            Assert.AreEqual(shipHeight, ship.Height);
        }

    }
}
