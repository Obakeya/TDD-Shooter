using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using System.ComponentModel;

namespace TDD_Shooter.Model
{
    class Back : Drawable
    {
        internal Back(String source):base(632,3328)
        {
            Source = new BitmapImage(new Uri(source));
            Y = -2528;
        }

        public override void Tick()
        {
            Y += SpeedY;
            if(Y > 0)
            {
                Y = -2528; 
            }

        }

    }
}
