
using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.AppContainer;
using TDD_Shooter.Model;
using Windows.ApplicationModel.Resources.Core;
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

            var enemy = new Enemy0(100, -50);
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

        [UITestMethod]
        public void HitEnemy()
        {
            var vm = new ViewModel();
            vm.Ship.X = 300;
            vm.Ship.Y = 300;

            var enemy = new Enemy0(300, 0);
            vm.AddEnemy(enemy);

            vm.KeyDown(Windows.System.VirtualKey.Space);
            vm.Tick(1);
            var bullet = (Bullet)vm.Bullets[0];

            //弾丸と敵が当たるまでのフレームを計算
            var nFrame = (int)((bullet.Y - (enemy.Y + enemy.Height)) / (enemy.SpeedY - bullet.SpeedY));
            vm.Tick(nFrame + 1);

            //衝突後の判定
            Assert.IsFalse(enemy.IsValid);
            Assert.IsFalse(bullet.IsValid);
            Assert.AreEqual(1, vm.Blasts.Count);

            var blast = (Blast)vm.Blasts[0];
            Assert.AreEqual(bullet.X + bullet.Width / 2, blast.X + blast.Width / 2);
            Assert.AreEqual(bullet.Y + bullet.Height/2, blast.Y + blast.Height / 2);

            vm.Tick(10);
            Assert.AreEqual(0, vm.Blasts.Count);
        }

        [UITestMethod]
        public void Enemy1Movement()
        {
            var vm = new ViewModel();
            var e = new Enemy1(200, 0);
            vm.AddEnemy(e);

            Assert.AreEqual(vm.Enemies.Count, 1);
            Assert.AreEqual(200, e.X);
            Assert.AreEqual(0, e.Y);
            Assert.AreEqual(vm.Bullets.Count, 0);

            double prevX = e.X, prevY = e.Y, prevS = e.SpeedY;
            for (var i = 0; i < 40; i++)
            {
                vm.Tick(1);
                Assert.AreEqual(prevX, e.X);
                Assert.IsTrue(prevY <= e.Y);
                Assert.AreEqual(prevS - 0.5, e.SpeedY);
                prevX = e.X;
                prevY = e.Y;
                prevS = e.SpeedY;
            }

            Assert.AreEqual(vm.Bullets.Count, 1);
            for(var i =0; i<40; i++)
            {
                vm.Tick(1);
                Assert.AreEqual(prevX, e.X);
                Assert.IsTrue(prevY > e.Y);
                Assert.AreEqual(prevS - 0.5, e.SpeedY);
                prevX = e.X;
                prevY = e.Y;
                prevS = e.SpeedY;
            }


        }
    

    }
}
