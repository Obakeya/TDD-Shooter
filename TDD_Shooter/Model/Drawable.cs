using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using System.ComponentModel;

namespace TDD_Shooter.Model
{
    class Drawable : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
       
        protected double x, y;

        public double X { get { return x; } set { x = value; NotifyPropertyChanged("X"); } } 

        public double Y { get { return y; } set { y = value; NotifyPropertyChanged("Y"); } }
        
        /// <summary>途中で変化しない</summary>
        public double Width { get; }
        /// <summary>途中で変化しない</summary>
        public double Height { get; }

        public double SpeedX { get; set; }

        public double SpeedY { get; set; }

        public BitmapImage Source { get; protected set; }

        protected Drawable (double w, double h)
        {
            Width = w; Height = h;
        }

        public void Move() 
        {
            X += SpeedX;
            Y += SpeedY;
        }


        private void NotifyPropertyChanged (String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
