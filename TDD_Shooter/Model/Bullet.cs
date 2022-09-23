using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using System.ComponentModel;

namespace TDD_Shooter.Model
{

    internal class Bullet : Drawable
    {
        internal Bullet (double x, double y) : base(10,10)
        {
            Source = new BitmapImage(new Uri("ms-appx:///IMages/bullet0.png"));
            X = x - Width / 2;
            Y = y - Height / 2;
            SpeedY = -10;
        }

    }
}
