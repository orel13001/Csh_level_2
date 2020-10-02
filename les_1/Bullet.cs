using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace les_1
{
    class Bullet:BaseObject, ICollision
    {
        /// <summary>
        /// Конструктор снаряда
        /// </summary>
        /// <param name="pos">позиция снаряда</param>
        /// <param name="dir">скорость снаряда</param>
        /// <param name="size">размер снаряда</param>
        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size)
        { }

        /// <summary>
        /// прямоугольник для определения пересечеиня с НЛО
        /// </summary>
        public Rectangle Rect => new Rectangle(Pos,Size);
        /// <summary>
        /// определения пересечеиня с НЛО
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public bool Collision(ICollision o)
        {
            return o.Rect.IntersectsWith(this.Rect);
        }

        /// <summary>
        /// отрисовка снаряда
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawRectangle(Pens.Orange, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        /// <summary>
        /// обновление позиции снаряда
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X + 3;
            if (Pos.X > Game.Width)
                Pos.X = Size.Width;
        }

        /// <summary>
        /// перерисовка снаряда
        /// </summary>
        public void ReDraw(Point pt)
        {
            Pos.X = pt.X;
            Pos.Y = pt.Y;
        }
    }
}
