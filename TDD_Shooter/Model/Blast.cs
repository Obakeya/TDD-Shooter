using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using System.ComponentModel;

namespace TDD_Shooter.Model
{

     class Blast : Drawable
    {
        private static BitmapImage[] images = new BitmapImage[8];
        private int counter = 0;

        static Blast()
        {
            for (var i =0; i<8; i++)
            {
                images[i] = new BitmapImage(new Uri("ms-appx:///Images/explode_" + i + ".png"));
            }
        }

        internal Blast (double x, double y) : base(96,96)
        {
            Source = images[counter];
            X = x - Width / 2;
            Y = y - Height / 2;
        }
       
        internal override void Tick()
        {
            counter++;
            Source = images[Math.Min(7, counter)];
        }

        internal override bool IsValid
        {
            get { return counter < 8; }
        }

    }
}
