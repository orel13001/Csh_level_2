using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace les_1
{
    class Ship : BaseObject, ICollision
    {

        public static event Message MessageDie;

        private int _point = 0;
        private int _energy = 100;
        public int Energy => _energy;
        public int _Point => _point;

        public Rectangle Rect => new Rectangle(Pos, Size);

        private static Image img = Image.FromFile(@"img/Ship.png");

        public void AddPoint()
        {
            _point++;
        }

        public void EnergyLow (int n)
        {
            _energy -= n;
        }
        public void EnergyUp (int n)
        {
            if(_energy < 100)
            {
                _energy = Math.Min(100, _energy += n);
            }
        }

        public Ship(Point pos, Point dir, Size size) : base(pos, dir, size) { }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(img, Pos.X, Pos.Y);
        }

        public override void Update()
        {
        }

        public void Up()
        {
            if (Pos.Y > 0) Pos.Y = Pos.Y - Dir.Y;
        }
        public void Down()
        {
            if (Pos.Y < Game.Height-Size.Height) Pos.Y = Pos.Y + Dir.Y;
        }
        public void Die()
        {
            MessageDie?.Invoke();
        }

        public bool Collision(ICollision o)
        {
            return o.Rect.IntersectsWith(this.Rect);
        }

    }
}
