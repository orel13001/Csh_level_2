using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace les_1
{
    /// <summary>
    /// базовый объект, отображаемый на форме
    /// </summary>
    abstract class BaseObject
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

        /// <summary>
        /// Конструктор базоого объекта 
        /// </summary>
        /// <param name="pos">позиция базоого объекта </param>
        /// <param name="dir">скорость базоого объекта </param>
        /// <param name="size">размер базоого объекта </param>
        protected BaseObject (Point pos, Point dir, Size size)
        {
            try
            {

                Pos = pos;
                Dir = dir;
                Size = size;
                if (Size.Width > 45 || Size.Height > 45 || Size.Width <= 0 || Size.Height <= 0) throw new SizeObjectException();
                if (Dir.X > 400 || Dir.Y > 400) throw new SpeedObjectException();
            }
            catch(SizeObjectException)
            {
                MessageBox.Show("Недопустимые размеры обекта! будут пременены параметры по умолчанию!", "Исключение!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Size.Width = 45;
                Size.Height = 45;
            }
            catch (SpeedObjectException)
            {
                MessageBox.Show("Недопустимая скорость обекта! будут пременены параметры по умолчанию!", "Исключение!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Dir.X = 40;
                Dir.Y = 40;
            }
        }
        /// <summary>
        /// отрисовка объектов на форме
        /// </summary>
        public abstract void Draw();
        //{
        //    Game.Buffer.Graphics.DrawEllipse(Pens.White, Pos.X, Pos.Y, Size.Width, Size.Height);
        //}

        /// <summary>
        /// обновление местоположения объекта
        /// </summary>
        public abstract void Update();
        //{
        //    Pos.X = Pos.X + Dir.X;
        //    Pos.Y = Pos.Y + Dir.Y;
        //    if (Pos.X < 0) { Dir.X = -Dir.X; }
        //    if (Pos.X > Game.Width) { Dir.X = -Dir.X; }
        //    if (Pos.Y < 0) { Dir.Y = -Dir.Y; }
        //    if (Pos.Y > Game.Height) { Dir.Y = -Dir.Y; }
        //}
    }

    /// <summary>
    /// Класс-исключение для объектов большого размера
    /// </summary>
    class SizeObjectException : Exception
    {
        public string Msg { get; }
        public SizeObjectException()
        {
            Msg = "Некорректные размеры объекта!";
        }
    }
    /// <summary>
    /// Класс-исключение для объектов с большой скоростью
    /// </summary>
    class SpeedObjectException : Exception
    {
        public string Msg { get; }
        public SpeedObjectException()
        {
            Msg = "Некорректная скорость объекта!";
        }
    }
}
