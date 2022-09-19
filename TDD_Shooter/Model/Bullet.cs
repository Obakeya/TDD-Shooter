using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using System.ComponentModel;

namespace TDD_Shooter.Model
{

    internal class Bullet : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public double X { get; set; }
        public double Y { get { return y; } set {y = value; NotifyPropertyChanged("Y"); } }
        private double y;

        public double Width { get { return 10; } }
        public double Height { get { return 10; } }
        public double Speed { get { return -10; } }

        public BitmapImage Source { get; set; }

        internal Bullet (double x, double y)
        {
            Source = new BitmapImage(new Uri("ms-appx:///IMages/bullet0.png"));
            X = x -5; // left
            Y = y -5; //top
        }

        internal void Move()
        {
            Y += Speed;
        }

        private void NotifyPropertyChanged (String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
