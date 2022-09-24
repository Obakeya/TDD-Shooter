using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using System.ComponentModel;

namespace TDD_Shooter.Model
{
    internal class Enemy0 : AbstractEnemy
    {

        internal Enemy0 (double x, double y) : base(50,50)
        {
            Source = new BitmapImage(new Uri("ms-appx:///IMages/enemy0_0.png"));
            X = x;
            Y = y;
            SpeedY = 5;
        }

        public override void Tick()
        {
            Y += SpeedY;
        }

        internal override  bool IsFire
        {
            get { return ++count == 20; }
        }
    }
}
