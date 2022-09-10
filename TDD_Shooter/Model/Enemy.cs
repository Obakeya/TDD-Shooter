using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using System.ComponentModel;

namespace TDD_Shooter.Model
{
    internal class Enemy : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public double X { get { return x; } set { x = value; NotifyPropertyChanged("X"); } }
        private double x;
        public double Y { get { return y; } set {y = value; NotifyPropertyChanged("Y"); } }
        private double y;
        public double Speed { get { return 5; } }

        public BitmapImage Source { get; set; }

        internal Enemy (double x, double y)
        {
            Source = new BitmapImage(new Uri("ms-appx:///IMages/enemy0_0.png"));
            X = x;
            Y = y;
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
