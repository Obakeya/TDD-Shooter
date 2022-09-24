﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using System.ComponentModel;
using Windows.Foundation;
using Windows.UI.Composition;
using Windows.UI.Xaml.Input;

namespace TDD_Shooter.Model
{
    abstract class Drawable : INotifyPropertyChanged
    {
        protected Drawable (double w, double h)
        {
            Rect.Width = w;
            Rect.Height = h;
            IsValid = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        internal abstract void Tick();

        internal virtual bool IsValid { get; set; }

        protected double x, y;

        internal Rect Rect;

        public double X
        {
            get { return Rect.X; }
            set { Rect.X = value; NotifyPropertyChanged("X"); }
        }
        public double Y
        {
            get { return Rect.Y; }
            set { Rect.Y = value; NotifyPropertyChanged("Y"); }
        }


        /// <summary>途中で変化しない</summary>
        public double Width { get { return Rect.Width; } }
        /// <summary>途中で変化しない</summary>
        public double Height { get { return Rect.Height; } }

        public double SpeedX { get; set; }

        public double SpeedY { get; set; }

        /// <summary>アニメーション描写対応</summary>
        
        
        public BitmapImage Source 
        {
            get { return source; }  
            protected set { source = value; NotifyPropertyChanged("Source") ; } 
        }

        private BitmapImage source;

        public void Move() 
        {
            X += SpeedX;
            Y += SpeedY;
        }

    }
}