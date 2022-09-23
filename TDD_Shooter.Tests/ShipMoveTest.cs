using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.AppContainer;
using Windows.Networking.Vpn;
using TDD_Shooter.Model;
using Windows.ApplicationModel.Payments;

namespace TDD_Shooter.Tests
{
    [TestClass]
    public class ShipMoveTest
    {
        [UITestMethod]
        public void ShipKeyMove()
        {
            ViewModel vm = new ViewModel();
            vm.Ship.X = 100;
            vm.Ship.Y = 100;

            vm.KeyDown(VirtualKey.Left);
            vm.Tick(1000); 
            Assert.IsTrue(vm.Ship.X >= 0); ///キーを押し続けても画面上に残るか
            vm.KeyUp(VirtualKey.Left);


            vm.KeyUp(VirtualKey.Right);
            vm.Tick(1000);
            Assert.IsTrue(vm.Ship.X + vm.Ship.Width <= ViewModel.Field.Width) ;
            vm.KeyUp(VirtualKey.Right);

            vm.KeyUp(VirtualKey.Up);
            vm.Tick(1000);
            Assert.IsTrue(vm.Ship.Y >=0);
            vm.KeyUp(VirtualKey.Up);

            vm.KeyDown(VirtualKey.Down);
            vm.Tick(1000);
            Assert.IsTrue(vm.Ship.Y + vm.Ship.Height <= ViewModel.Field.Height);
            vm.KeyUp(VirtualKey.Down);


        }
    }
}
