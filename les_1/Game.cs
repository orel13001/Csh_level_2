using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace les_1
{
    static class Game
    {
        /// <summary>
        /// поле предоставляет методы для содания графических буферов
        /// </summary>
        private static BufferedGraphicsContext _context;
        /// <summary>
        /// поле графического буфера для двойной буфферизации
        /// </summary>
        public static BufferedGraphics Buffer;

        /// <summary>
        /// Ширина окна
        /// </summary>
        public static int Width { get; set; }
        /// <summary>
        /// Высота окна
        /// </summary>
        public static int Height { get; set; }
        /// <summary>
        /// Максимальные высота и ширина окна
        /// </summary>
        const int MAX_Width = 1000, MAX_Height = 1000;
        public static BaseObject[] _objs;
        private static Bullet _bullet;
        private static Asteroid[] _asteroids;

        private static Random rnd = new Random();

        static int spd = rnd.Next(5, 50);
        static int size = rnd.Next(29, 50);


        static Game() { }


        /// <summary>
        /// инициализация буфера для рисования на форме
        /// </summary>
        /// <param name="form">форма для отрисовки</param>
        public static void Init(Form form)
        {
            // Графическое устройство для вывода графики
            Graphics g;
            // Предоставляет доступ к главному буферу графического контекста для текущего приложения
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            try
            {
                // Создаем объект (поверхность рисования) и связываем его с формой
                // Запоминаем размеры формы
                Width = form.ClientSize.Width;
                Height = form.ClientSize.Height;
                if (Width > MAX_Width || Width < 0) throw new ArgumentOutOfRangeException("Width", "Недопустимая высота окна");
                if (Height > MAX_Height || Height < 0) throw new ArgumentOutOfRangeException("Height", "Недопустимая ширина окна");
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Недопустимые параметры ширины и/или высоты окна! будут пременены параметры по умолчанию!", "Исключение!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (form.ClientSize.Width > MAX_Width)
                    Width = MAX_Width;
                else
                    Width = form.ClientSize.Width;
                if (form.ClientSize.Height > MAX_Height)
                    Height = MAX_Height;
                else
                    Height = form.ClientSize.Height;
            }
            finally
            {
                // Связываем буфер в памяти с графическим объектом, чтобы рисовать в буфере
                Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));
                Load();


                Timer timer = new Timer { Interval = 100 };
                timer.Start();
                timer.Tick += Timer_Tick;
            }
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {    
            Draw();
            Update();
        }

        /// <summary>
        /// Отрисовка изображения на форме
        /// </summary>
        public static void Draw()
        {
            // Проверяем вывод графики
            //Buffer.Graphics.Clear(Color.Black);
            //Buffer.Graphics.DrawRectangle(Pens.White, new Rectangle(100, 100, 200, 200));
            //Buffer.Graphics.FillEllipse(Brushes.White, new Rectangle(100, 100, 200, 200));
            //Buffer.Render();

            Buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in _objs)
                obj.Draw();
            foreach (BaseObject obj in _asteroids)
                obj.Draw();
            _bullet.Draw();
            Buffer.Render();
        }

        /// <summary>
        /// инициализация массива BaseObject
        /// </summary>
        public static void Load()
        {


            _objs = new BaseObject[33];
            _bullet = new Bullet(new Point(0, 200), new Point(5, 0), new Size(4, 1));
            _asteroids = new Asteroid[10];
            for (int i = 0; i< _objs.Length; i++)
            {                
                _objs[i] = new Star(new Point(rnd.Next(0, Game.Width), rnd.Next(0, Game.Height)), new Point(-spd, spd), new Size(3, 3));
            }

            for(int i = 0; i < _asteroids.Length; i++)
            {
                _asteroids[i] = new Asteroid(new Point(500, rnd.Next(0, Game.Height)), new Point(-spd, spd), new Size(size, size));
            }



            //for (int i = 0; i < _objs.Length/3; i++)
            //{
            //    _objs[i] = new BaseObject(new Point(600, i * 20), new Point(15 - i, 15 - i), new Size(20, 20));
            //}
            //for (int i = _objs.Length / 3; i < _objs.Length - 8; i++)
            //{
            //    _objs[i] = new Star(new Point(600, i * 30-600), new Point(i, 0), new Size(5, 5));
            //}
            //for (int i = _objs.Length - 8; i <  _objs.Length; i++)
            //{
            //    _objs[i] = new Meteor(new Point(i*50-2500, 0), new Point(10, (i+5)*10), new Size(10,10));
            //}
        }

        /// <summary>
        /// Обновление положения объектов игры
        /// </summary>
        public static void Update()
        {
            _bullet.Update();
            foreach (BaseObject obj in _objs)
                obj.Update();
            for (int i = 0; i <_asteroids.Length; i++)
            {
                _asteroids[i].Update();
                if (_bullet.Collision(_asteroids[i]))
                {
                    _bullet = new Bullet(new Point(0, 200), new Point(5, 0), new Size(4, 1));
                    _asteroids[i] = new Asteroid(new Point(Game.Width-size, 200), new Point(-spd, spd), new Size(size, size));
                }
            }
            
        }
    }
}
