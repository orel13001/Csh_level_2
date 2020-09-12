using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace les_1
{
    class Meteor: BaseObject
    {
        public Meteor(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }
        private static Image img = Image.FromFile(@"meteor_PNG8.png");
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(img, Pos.X, Pos.Y); 
        }

        /// <summary>
        /// обновление местоположения объекта
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X - Dir.X;
            Pos.Y = (int)(Pos.Y + 0.1*Dir.Y);
            if (Pos.X < 0)
            {
                Pos.X = Game.Width + Size.Width;
            }
            if (Pos.Y >Game.Height )
            {
                Pos.Y =  Size.Height;
            }
        }
    }
}
