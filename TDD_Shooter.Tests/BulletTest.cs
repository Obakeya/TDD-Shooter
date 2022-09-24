
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.AppContainer;
using TDD_Shooter.Model;
using Windows.Devices.Bluetooth.Advertisement;
using Windows.UI.Xaml.Media.Imaging;

namespace TDD_Shooter.Tests
{
    [TestClass]
    public class BulletTest
    {
        [UITestMethod]
        ///<summary>弾丸は200フレーム経過後に0になる</summary>
        public void CreateBullet()
        {
            var vm = new ViewModel();
            Assert.AreEqual(0 ,vm.Bullets.Count );

            var bullet = new Bullet(100, -50);
            vm.AddBullet(bullet);

            Assert.AreEqual(1, vm.Bullets.Count);
            vm.Tick(200);
            Assert.AreEqual(0, vm.Bullets.Count); 
        }

        ///<summary>発射後弾丸はY軸のみ変化する</summary>
        [UITestMethod]
        public void ShootBullet()
        {
            var vm = new ViewModel();
            vm.KeyDown(Windows.System.VirtualKey.Space);
            vm.Tick(1);
            Assert.AreEqual(1, vm.Bullets.Count);
            var b = vm.Bullets[0];
            var xPrev = b.X;
            var yPrev = b.Y;
            vm.Tick(1);
            Assert.AreEqual(xPrev, b.X);
            Assert.AreEqual(yPrev + b.SpeedY, b.Y);
        }

        [UITestMethod]
        ///<summary>弾丸の数の仕様を検証する</summary>
        public void BulletKeyRepeat()
        {
            var vm = new ViewModel();
            vm.Ship.Y = ViewModel.Field.Height - vm.Ship.Height;
            vm.Ship.X = ViewModel.Field.Width / 2;
            Assert.AreEqual(0, vm.Bullets.Count);

            vm.KeyDown(Windows.System.VirtualKey.Space);
            vm.Tick(1);
            Assert.AreEqual(1, vm.Bullets.Count);
            vm.Tick(5);
            Assert.AreEqual(1, vm.Bullets.Count);
            vm.KeyUp(Windows.System.VirtualKey.Space);
            Assert.AreEqual(1, vm.Bullets.Count);

            vm.Tick(1);
            vm.KeyDown(Windows.System.VirtualKey.Space);
            vm.Tick(1);
            Assert.AreEqual(2, vm.Bullets.Count);

            vm.Tick(100);
            Assert.AreEqual(0, vm.Bullets.Count);
        }

        [UITestMethod]
        ///<summary>敵が上部にいて自機が真下にいる状態。敵の弾に自機が当たれば自機は消滅</summary>
        public void EnemyShootBullet()
        {
            var vm = new ViewModel();
            vm.Ship.X = 300 - vm.Ship.Width / 2;
            vm.Ship.Y = 300;

            var enemy = new Enemy(300, 0);
            enemy.X -= enemy.Width / 2;
            vm.AddEnemy(enemy);
            vm.Tick(19);

            Assert.AreEqual(0, vm.Bullets.Count);
            vm.Tick(1);
            Assert.AreEqual(1, vm.Bullets.Count);

            var b = (Bullet)vm.Bullets[0];
            var e = (Enemy)vm.Enemies[0];

            Assert.AreEqual(b.X + b.Width / 2 - 5, e.X + e.Width / 2);
            Assert.AreEqual(b.Y + b.Height / 2 - 5, e.Y + e.Height / 2);

            Assert.IsTrue(vm.Ship.IsValid);
            vm.Tick(20);
            Assert.IsFalse(vm.Ship.IsValid);
        }

    }
}
