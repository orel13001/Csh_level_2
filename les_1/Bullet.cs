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
        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size)
        { }



        public Rectangle Rect => new Rectangle(Pos,Size);

        public bool Collision(ICollision o)
        {
            return o.Rect.IntersectsWith(this.Rect);
        }




        public override void Draw()
        {
            Game.Buffer.Graphics.DrawRectangle(Pens.Orange, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        public override void Update()
        {
            Pos.X = Pos.X + 3;
        }
    }
}
