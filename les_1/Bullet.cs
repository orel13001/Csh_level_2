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
        /// Вызывется при создании снаряда
        /// </summary>
        public static event Action<string> CreateBullet;

        /// <summary>
        /// Вызывется при столкновении снаряда с НЛО снаряда
        /// </summary>
        public static event Action<string> CollisionBullet;

        /// <summary>
        /// Конструктор снаряда
        /// </summary>
        /// <param name="pos">позиция снаряда</param>
        /// <param name="dir">скорость снаряда</param>
        /// <param name="size">размер снаряда</param>
        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            CreateBullet?.Invoke($"{DateTime.Now}: Снаряд создан");
        }

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
            if(o.Rect.IntersectsWith(this.Rect))
                CollisionBullet?.Invoke($"{DateTime.Now}: Снаряд попал в НЛО");
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
            Pos.X = Pos.X + Dir.X;
            //if (Pos.X > Game.Width)
            //    Pos.X = Size.Width;
        }


    }
}
