﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace les_1
{
    /// <summary>
    /// базовый объект, отображаемый на форме
    /// </summary>
    class BaseObject
    {
        /// <summary>
        /// позиция объекта
        /// </summary>
        protected Point Pos;
        /// <summary>
        /// смещение объекта
        /// </summary>
        protected Point Dir;
        /// <summary>
        /// размер объекта
        /// </summary>
        protected Size Size;

        public BaseObject (Point pos, Point dir, Size size)
        {
            Pos = pos;
            Dir = dir;
            Size = size;
        }
        private static Image img = Image.FromFile(@"meteor_PNG8.png");
        /// <summary>
        /// отрисовка объектов на форме
        /// </summary>
        public virtual void Draw()
        {
            Game.Buffer.Graphics.DrawEllipse(Pens.White, Pos.X, Pos.Y, Size.Width, Size.Height);
            
        }

        /// <summary>
        /// обновление местоположения объекта
        /// </summary>
        public virtual void Update()
        {
            Pos.X = Pos.X + Dir.X;
            Pos.Y = Pos.Y + Dir.Y;
            if (Pos.X < 0) { Dir.X = -Dir.X; }
            if (Pos.X > Game.Width) { Dir.X = -Dir.X; }
            if (Pos.Y < 0) { Dir.Y = -Dir.Y; }
            if (Pos.Y > Game.Height) { Dir.Y = -Dir.Y; }
        }
    }
}
