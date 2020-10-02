using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace les_1
{
    class UFO: BaseObject, ICollision
    {
        /// <summary>
        /// Конструктор НЛО
        /// </summary>
        /// <param name="pos">позиция НЛО</param>
        /// <param name="dir">скорость НЛО</param>
        /// <param name="size">размер НЛО</param>
        public UFO (Point pos, Point dir, Size size):base(pos,dir,size)
        {
            this.Size.Width = img.Width;
            this.Size.Height = img.Height;
        }
        private static Image img = Image.FromFile(@"img/ufo.png");


        /// <summary>
        /// отрисовка НЛО
        /// </summary>
        public override void Draw()
        {
            //Game.Buffer.Graphics.DrawEllipse(Pens.White, Pos.X, Pos.Y, Size.Width, Size.Height);
            Game.Buffer.Graphics.DrawImage(img, Pos.X, Pos.Y);
        }

        /// <summary>
        /// обновление позиции НЛО
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            Pos.Y = Pos.Y + Dir.Y;
            if (Pos.X < 0) { Dir.X = -Dir.X; }
            if (Pos.X > Game.Width-Size.Width) { Dir.X = -Dir.X; }
            if (Pos.Y < 0) { Dir.Y = -Dir.Y; }
            if (Pos.Y > Game.Height-Size.Height) { Dir.Y = -Dir.Y; }
        }


        /// <summary>
        /// определения пересечеиня со снарядом
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public bool Collision(ICollision o)
        {
            return o.Rect.IntersectsWith(this.Rect);
        }
        /// <summary>
        /// перерисовка НЛО
        /// </summary>
        public void ReDraw(Point pt)
        {
            Pos.X = pt.X;
            Pos.Y = pt.Y;
        }
        /// <summary>
        /// прямоугольник для определения пересечеиня со снарядом
        /// </summary>
        public Rectangle Rect => new Rectangle(Pos, Size);
    }
}
