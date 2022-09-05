using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.AppContainer;

namespace TDD_Shooter.Tests
{
    [TestClass]
    public class BackgroundTest
    {
        [UITestMethod]
        public void JustScroll()
        {
            var vm = new ViewModel();
            //画面の縦サイズ3328からウィンドウの高さ800を引いた2528分上から画像スクロールを始める
            Assert.AreEqual(-2528, vm.Back.Y);
            vm.Back.Scroll(1);
            Assert.AreEqual(-2527, vm.Back.Y);
        }

        public void JustScrollWrap()
        {
            //背景画像を繰り返す
            var vm = new ViewModel();
            for (var i =0; i < 2528; i++)
            {
                vm.Back.Scroll(1);
            }
            Assert.AreEqual(0, vm.Back.Y);
            vm.Back.Scroll(1);
            Assert.AreEqual(-2528, vm.Back.Y);
        }



    }
}
