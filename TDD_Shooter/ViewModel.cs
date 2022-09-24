using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using TDD_Shooter.Model;
using Windows.System;
using Windows.Foundation;
using System.ComponentModel.DataAnnotations;

namespace TDD_Shooter
{
    /// <summary>
    /// 画面上に表示されるオブジェクトを定義
    /// </summary>
    class ViewModel
    {
        private Dictionary<VirtualKey, bool> keyMap = new Dictionary<VirtualKey, bool>();

        public Ship Ship { get; set; }

        public Back Back { get; set; }

        public Back Cloud { get; set; }

        public static readonly Rect Field = new Rect(0, 0, 643, 800); //ウィンドウサイズ指定

        public double Width { get { return Field.Width; } }

        public double Height { get { return Field.Height; } }

        private ObservableCollection<Drawable> drawables = new ObservableCollection<Drawable>();


        public ObservableCollection<Drawable> Drawables
        {
            get { return drawables; }

        }

        private List<Drawable> Filter<T>()
        {
            var data = Drawables.Where(e => e is T);
            return data.ToList();
        }

        /// <summary>動的に画面上での表示数を変化させる </summary>
        public List<Drawable> Enemies  {get { return Filter<Enemy>(); }}
        /// <summary>動的に画面上での表示数を変化させる </summary>
        public List<Drawable> Bullets{get{return Filter<Bullet>();}} 
        /// <summary>動的に画面上での表示数を変化させる </summary>
        public List<Drawable>Blasts{get { return Filter<Blast>(); }}

        internal ViewModel()
        {
            Ship = new Ship();
            Back = new Back("ms-appx:///Images/back.png");
            Back.SpeedY = 1;
            Cloud = new Back("ms-appx:///Images/back_cloud.png");
            Cloud.SpeedY = 2;
            drawables.Add(Back);
            drawables.Add(Cloud);
            drawables.Add(Ship);
        }

        internal void KeyDown(VirtualKey key)
        {
            keyMap[key] = true;
        }

        internal void KeyUp(VirtualKey key)
        {
            keyMap[key] = false;
        }

        private Boolean IsKeyDown (VirtualKey key)
        {
            return keyMap.ContainsKey(key) && keyMap[key];
        }
   

        internal void Tick (int frame)
        {
            for (int i = 0; i< frame; i++)
            {
                Ship.SpeedX = 0;
                Ship.SpeedY = 0;

                if (IsKeyDown(VirtualKey.Left))
                {
                    Ship.SpeedX = -Ship.Speed;
                }

                if (IsKeyDown(VirtualKey.Right))
                {
                    Ship.SpeedX = Ship.Speed;
                }

                if (IsKeyDown(VirtualKey.Up))
                {
                    Ship.SpeedY = -Ship.Speed;
                }

                if (IsKeyDown(VirtualKey.Down))
                {
                    Ship.SpeedY = Ship.Speed;
                }

                if (IsKeyDown(VirtualKey.Space))
                {
                    var b = new Bullet(Ship.X + Ship.Width / 2, Ship.Y + Ship.Height / 2);
                    AddBullet(b);
                    keyMap[VirtualKey.Space] = false;
                }

                foreach (Drawable e in Drawables.ToArray())//Drawablesの中身を削除してはダメ
                {
                    e.Tick();
                    var r = e.Rect;
                    r.Intersect(Field);
                    if(!e.IsValid || r.IsEmpty)
                        Drawables.Remove(e);  //画面の外に出た物体を削除
                    
                    if(e is Enemy)
                    {
                        var enemy = (Enemy)e;
                        if (enemy.IsFire)
                            CreateEnemyBullet(enemy);

                        r = e.Rect;
                        r.Intersect(Ship.Rect);
                        if (r != Rect.Empty)
                            Ship.IsValid = false;
                    }

                }

                // 弾丸と敵の衝突判定
                foreach(Bullet b in Bullets)
                {
                    if (b.IsEnemy)
                    {
                        var r = b.Rect;
                        r.Intersect(Ship.Rect);
                        if (r != Rect.Empty)
                            Ship.IsValid = false;
                        
                        continue;
                    }

                    foreach (Enemy e in Enemies)
                    {
                        var r = e.Rect;
                        r.Intersect(b.Rect);

                        if (r != Rect.Empty && b.IsValid && e.IsValid)
                        {
                            e.IsValid = false;
                            b.IsValid = false;
                            var blast = new Blast(b.X + b.Width / 2, b.Y + b.Height / 2);
                            Drawables.Add(blast);
                        }
                    }
                }

                Ship.Y = Math.Max(0, Math.Min(Field.Height - Ship.Height, Ship.Y));//画面から実機がでないようにする
                Ship.X = Math.Max(0, Math.Min(Field.Width - Ship.Width, Ship.X));
            }
        }

        internal void AddEnemy(Enemy e)
        {
            Drawables.Add(e);
        }

        internal void AddBullet(Bullet b)
        {
            Drawables.Add(b);
        }

        private void CreateEnemyBullet(Enemy e)
        {
            var sX = e.X + e.Width / 2;
            var sY = e.Y + e.Height / 2;
            var eX = Ship.X + Ship.Width / 2;
            var eY = Ship.Y + Ship.Height / 2;
            var theta = Math.Atan2(eY - sY, eX - sX);
            var dx = Math.Cos(theta) * Bullet.Speed;
            var dy = Math.Sin(theta) * Bullet.Speed;
            var b = new Bullet(sX, sY, dx, dy, true);
            AddBullet(b);
        }

        internal static bool Crash(Drawable d0, Drawable d1)
        {
            var r = d0.Rect;
            r.Intersect(d1.Rect);
            return r != Rect.Empty;
        }
    }
}
