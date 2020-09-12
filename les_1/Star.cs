using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace les_1
{
    class Star: BaseObject
    {
        public Star(Point pos, Point dir, Size size):base(pos,dir,size)
        {
        }
        
        private static Image img = Image.FromFile(@"star_PNG1591.png");
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
            if (Pos.X < 0) 
            {
                Pos.X = Game.Width+Size.Width; 
            }
           
        }
    }
}
