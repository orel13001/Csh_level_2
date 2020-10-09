using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace les_1
{
    class HelthBox : BaseObject, ICollision
    {
        static Random rnd = new Random();
        public HelthBox(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }
        private static Image img = Image.FromFile(@"img/helth.png");

        /// <summary>
        /// прямоугольник для определения пересечеиня с Кораблём
        /// </summary>
        public Rectangle Rect => new Rectangle(Pos, Size);
        /// <summary>
        /// определения пересечеиня с Кораблём
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public bool Collision(ICollision o)
        {
            return o.Rect.IntersectsWith(this.Rect);
        }
        /// <summary>
        /// отрисовка аптечки
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(img, Pos.X, Pos.Y);
        }

        /// <summary>
        /// обновление позиции аптечки
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X - Dir.X;
            Pos.Y = Pos.Y + Convert.ToInt32(Dir.Y * Math.Cos(Pos.X));
            if (Pos.X < 0) {
                Pos.X = Game.Width + rnd.Next(250, 600);
                Pos.Y = rnd.Next(0, Game.Height);
            }
            if (Pos.Y < 0) { Dir.Y = -Dir.Y; }
            if (Pos.Y > Game.Height - Size.Height) { Dir.Y = -Dir.Y; }
        }
        /// <summary>
        /// Пееррисовка аптечки при столкновении с кораблём
        /// </summary>
        public void ReDraw()
        {
            Pos.X = Game.Width + rnd.Next(250, 600);
            Pos.Y = rnd.Next(0, Game.Height);
        }
    }
}
