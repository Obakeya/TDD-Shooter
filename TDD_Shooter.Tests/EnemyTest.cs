
using System;
using System.ComponentModel.DataAnnotations;
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

        [UITestMethod]
        public void HitEnemy()
        {
            var vm = new ViewModel();
            vm.Ship.X = 300;
            vm.Ship.Y = 300;

            var enemy = new Enemy(300, 0);
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
    

    }
}
