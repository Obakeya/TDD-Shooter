using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using System.ComponentModel;
using Windows.Security.Cryptography.Core;

namespace TDD_Shooter.Model
{
    internal class Enemy2 : AbstractEnemy
    {
        private double delta;

        internal Enemy2 (double x, double y,double sx, double sy, double t) : base(x, y,75,160)
        {
            Source = new BitmapImage(new Uri("ms-appx:///Images/enemy2.png"));
            X = x;
            Y = y;
            SpeedX = sx;
            SpeedY = sy;
            delta = t;
        }

        public override void Tick()
        {
            Y += SpeedY;
            X += SpeedX;
            Theta += delta;
        }

        internal override bool IsFire
        {
            get { return SpeedY == 0; }
        }
    }
}
