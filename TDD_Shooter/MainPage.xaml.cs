using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using TDD_Shooter.Model;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x411 を参照してください

namespace TDD_Shooter
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        ViewModel Model;
        DispatcherTimer timer;
        private int count = 0;
        public static readonly Rect Field = new Rect(0, 0, 643, 800);
        public double Width {get { return Field.Width; } }

        public double Heigth {get { return Field.Height; } }


        public MainPage()
        {
            this.InitializeComponent();
            Model = new ViewModel();
            DataContext = Model;
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            Window.Current.CoreWindow.KeyUp += CoreWindow_KeyUp;
            Model.AddEnemy(new TDD_Shooter.Model.Enemy0(200, 100));

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(20);
            timer.Tick += Tick;
            timer.Start();

            Model.Message.Text = "GET READY...";
            Model.AddEnemy(new Enemy1(300, 0));
            Model.AddEnemy(new Enemy1(500, -50));
            Model.Ship.X = 300;
            Model.Ship.Y = 700;
        }

        private void Tick(object sender , object e)
        {
            if (++count == 50)
                Model.Message.Text = "";

            Model.Tick(1);
        }

        private void CoreWindow_KeyDown(CoreWindow sender, KeyEventArgs args)
        {
            Model.KeyDown(args.VirtualKey);
        }

        private void CoreWindow_KeyUp(CoreWindow sender , KeyEventArgs args)
        {
            Model.KeyUp(args.VirtualKey);
        }

    }
}
