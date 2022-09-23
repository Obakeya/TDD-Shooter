using System;
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
    /// <summary>
    /// 画面上に表示されるオブジェクトを定義
    /// </summary>
    class ViewModel
    {
        private Dictionary<VirtualKey, bool> keyMap = new Dictionary<VirtualKey, bool>();

        public Ship Ship { get; set; }

        public Back Back { get; set; }

        public Back Cloud { get; set; }

        private ObservableCollection<Drawable> drawables = new ObservableCollection<Drawable>();

        public static readonly Rect Field = new Rect(0, 0, 643, 800); //ウィンドウサイズ指定

        public double Width { get { return Field.Width; } }

        public double Height { get { return Field.Height; } }

        public ObservableCollection<Drawable> Drawables
        {
            get { return drawables; }

        }

        public List<Drawable> Enemies 
        {
            get {
                IEnumerable<Drawable> data = Drawables.Where(e => e is Enemy);
                return data.ToList<Drawable>(); 
               }
        } // 動的に画面上に表示数を変化させる


        public List<Drawable> Bullets
        {
            get
            {
                IEnumerable<Drawable> data = Drawables.Where(e => e is Bullet);
                return data.ToList<Drawable>();
            }
        } // 動的に画面上に表示数を変化させる


        public List<Drawable>Blasts
        {
            get
            {
                var data = Drawables.Where((e) => e is Blast);
                return data.ToList<Drawable>();
            }
        }

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
                    {
                        Drawables.Remove(e);  //画面の外に出た物体を削除
                    }
                }

                // 弾丸と敵の衝突判定
                foreach(Bullet b in Bullets)
                {
                    foreach(Enemy e in Enemies)
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

    }
}
