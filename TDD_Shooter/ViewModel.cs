﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using TDD_Shooter.Model;
using Windows.System;
using Windows.Foundation;

namespace TDD_Shooter
{

    class ViewModel
    {
        private Dictionary<VirtualKey, bool> keyMap = new Dictionary<VirtualKey, bool>();

        public Ship Ship { get; set; }

        public Back Back { get; set; }

        public Back Cloud { get; set; }

        private ObservableCollection<Enemy> enemies = new ObservableCollection<Enemy>();

        public ObservableCollection<Enemy> Enemies { get { return enemies; } } // 動的に画面上に表示数を変化させる

        public static readonly Rect Field = new Rect(0, 0, 643, 800); //ウィンドウサイズ指定\\

        public double Width { get { return Field.Width; } }

        public double Height { get { return Field.Height; } }

        internal ViewModel()
        {
            Ship = new Ship();
            Back = new  Back("ms-appx:///Images/back.png");
            Cloud = new Back("ms-appx:///Images/back_cloud.png");
        }

        internal void KeyDown(VirtualKey key)
        {
            keyMap[key] = true;
        }

        internal void KeyUp(VirtualKey key)
        {
            keyMap[key] = false;
        }

        internal void AddEnemy(Enemy e)
        {
            Enemies.Add(e);
        }

        internal void Tick (int frame)
        {
            for (int i = 0; i< frame; i++)
            {
                Back.Scroll(1);
                Cloud.Scroll(2);

                foreach (Enemy e in Enemies.ToArray())//Enemiesの中身を削除してはダメ
                {
                    e.Move();
                    if(e.Y > Field.Height)
                    {
                        Enemies.Remove(e);  //画面の外に出た敵を削除
                    }
                }

                if (keyMap.ContainsKey(VirtualKey.Left) && keyMap[VirtualKey.Left])
                 {
                    Ship.Move(-Ship.Speed, 0);
                 }

                if(keyMap.ContainsKey(VirtualKey.Right) && keyMap[VirtualKey.Right])
                {
                    Ship.Move(+Ship.Speed, 0);
                }

                if (keyMap.ContainsKey(VirtualKey.Up) && keyMap[VirtualKey.Up])
                {
                    Ship.Move(0, -Ship.Speed);
                }

                if(keyMap.ContainsKey(VirtualKey.Down) && keyMap[VirtualKey.Down])
                {
                    Ship.Move(0, +Ship.Speed);
                }


            }
        }

    }
}
