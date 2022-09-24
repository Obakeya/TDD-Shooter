using System;
using Windows.UI.Xaml.Media.Imaging;
using System.ComponentModel;

namespace TDD_Shooter.Model
{
    class Message : INotifyPropertyChanged, IClock
    {
        private String text;
        private double theta;
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertName));
        }

        public void Tick() 
        {
            theta += 0.1;
            NotifyPropertyChanged("Alpha");
        }

        public double Alpha
        {
            get { return (Math.Sin(theta) + 1 / 2); }
        }

        public String Text
        {
            get { return text; }
            set { text = value; NotifyPropertyChanged("Text"); }
        }


    }
}
