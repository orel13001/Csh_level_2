using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace les_1
{
    class UFO: BaseObject, ICollision, IComparable<UFO>, ICloneable
    {
        static Random rnd = new Random();
        public int Power { get; set; } = 3;

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
            CreateUFO?.Invoke($"{DateTime.Now}: НЛО создано\n");
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
            Pos.X = Pos.X - Dir.X;
            Pos.Y = Pos.Y + Convert.ToInt32(Dir.Y * Math.Sin(Pos.X));
            //if (Pos.X < 0) 
            //{ 
            //    Pos.X = Game.Width - Size.Width;
            //    Pos.Y = rnd.Next(0, Game.Height);
            //}
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
        public void ReDraw()
        {
            Pos.X = Game.Width;
            Pos.Y = rnd.Next(0, Game.Height);
        }

        /// <summary>
        /// Копирование НЛО
        /// </summary>
        /// <returns>Копия вызывающего объекта</returns>
        public object Clone()
        {
            UFO ufo = new UFO(new Point(Pos.X, Pos.Y), new Point(Dir.X, Dir.Y), new Size(Size.Width, Size.Height)) { Power = Power};
            return ufo;
            
        }

        /// <summary>
        /// Сравнение энергии двух НЛО
        /// </summary>
        /// <param name="obj">объект для сравнения</param>
        /// <returns></returns>
        public int CompareTo(UFO obj)
        {
            if (Power > obj.Power)
                return 1;
            if (Power < obj.Power)
                return -1;
            else
                return 0;
        }

        /// <summary>
        /// прямоугольник для определения пересечеиня со снарядом
        /// </summary>
        public Rectangle Rect => new Rectangle(Pos, Size);


        /// <summary>
        /// Вызывется при создании НЛО
        /// </summary>
        public static event Action<string> CreateUFO;


    }
}
