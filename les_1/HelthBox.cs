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

        public Rectangle Rect => new Rectangle(Pos, Size);

        public bool Collision(ICollision o)
        {
            return o.Rect.IntersectsWith(this.Rect);
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(img, Pos.X, Pos.Y);
        }

        public override void Update()
        {
            Pos.X = Pos.X - Dir.X;
            Pos.Y = Pos.Y + Convert.ToInt32(Dir.Y * Math.Cos(Pos.X));
            if (Pos.X < 0) {
                Pos.X = Game.Width - Size.Width;
                Pos.Y = rnd.Next(0, Game.Height);
            }
            if (Pos.Y < 0) { Dir.Y = -Dir.Y; }
            if (Pos.Y > Game.Height - Size.Height) { Dir.Y = -Dir.Y; }
        }
        
        public void ReDraw()
        {
            Pos.X = Game.Width + rnd.Next(250, 600);
            Pos.Y = rnd.Next(0, Game.Height);
        }
    }
}
