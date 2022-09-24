using System;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Foundation;

namespace TDD_Shooter.Model
{

    internal class Bullet : Drawable
    {
        internal const double Speed = 10;
        internal bool IsEnemy { get; set; }

        /// <summary>
        /// 弾丸作成時に自機か敵のものか指定
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        /// <param name="isEnemy"></param>
        internal Bullet (double x, double y,double dx = 0, double dy = -Bullet.Speed, bool isEnemy = false) : base(10,10)
        {
            IsEnemy = isEnemy;
            var file = isEnemy ? "bullet1.png" : "bullet0.png";
            Source = new BitmapImage(new Uri("ms-appx:///IMages/bullet0.png"));
            X = x;
            Y = y;
            SpeedX = dx;
            SpeedY = dy;
        }

        public override void Tick()
        {
            Y += SpeedY;
            X += SpeedX;
            var r = this.Rect;
            r.Intersect(ViewModel.Field);
            if(r == Rect.Empty)
            {
                IsValid = false;
            }


        }

    }
}
