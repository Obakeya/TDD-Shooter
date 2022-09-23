using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using System.ComponentModel;

namespace TDD_Shooter.Model
{
    internal class Enemy : Drawable
    {
        internal Enemy (double x, double y) : base(50,50)
        {
            Source = new BitmapImage(new Uri("ms-appx:///IMages/enemy0_0.png"));
            X = x;
            Y = y;
            SpeedY = 5;
        }
    }
}
