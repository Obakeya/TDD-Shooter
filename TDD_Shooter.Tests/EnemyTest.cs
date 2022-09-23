
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.AppContainer;
using TDD_Shooter.Model;
using Windows.Devices.Bluetooth.Advertisement;
using Windows.UI.Xaml.Media.Imaging;

namespace TDD_Shooter.Tests
{
    [TestClass]
    public class EnemyTest
    {
        [UITestMethod]
        public void CreateEnemy()
        {
            var vm = new ViewModel();
            Assert.AreEqual(vm.Enemies.Count, 0);

            var enemy = new Enemy(100, -50);
            vm.AddEnemy(enemy);　//敵は複数登場する
            Assert.AreEqual(vm.Enemies.Count, 1);
            Assert.AreEqual(100, enemy.X);
            Assert.AreEqual(-50, enemy.Y);
            vm.Tick(1); //最初の敵は下に移動するだけ
            Assert.AreEqual(-50 + enemy.SpeedY, enemy.Y);
            Assert.AreEqual(vm.Enemies.Count, 1);
            var nFrame = (int)(ViewModel.Field.Height / enemy.SpeedY) + 10;
            vm.Tick(nFrame);
            Assert.AreEqual(vm.Enemies.Count, 0);
        }
    

    }
}
