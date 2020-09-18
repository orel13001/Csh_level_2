using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace les_1
{
    class Asteroid: BaseObject, ICollision
    {
        public int Power { get; set; }
        public Asteroid (Point pos, Point dir, Size size):base(pos,dir,size)
        {
            Power = 1;
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawEllipse(Pens.White, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            Pos.Y = Pos.Y + Dir.Y;
            if (Pos.X < 0) { Dir.X = -Dir.X; }
            if (Pos.X > Game.Width) { Dir.X = -Dir.X; }
            if (Pos.Y < 0) { Dir.Y = -Dir.Y; }
            if (Pos.Y > Game.Height) { Dir.Y = -Dir.Y; }
        }


        public bool Collision(ICollision o)
        {
            return o.Rect.IntersectsWith(this.Rect);
        }
        public Rectangle Rect => new Rectangle(Pos, Size);
    }
}
