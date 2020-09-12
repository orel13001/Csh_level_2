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

        public static BaseObject[] _objs;

        static Game() { }


        /// <summary>
        /// инициализация буфера для рисования на форме
        /// </summary>
        /// <param name="form">форма для отрисовки</param>
        public static void Init(Form form)
        {
            Load();

            // Графическое устройство для вывода графики
            Graphics g;
            // Предоставляет доступ к главному буферу графического контекста для текущего приложения
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            // Создаем объект (поверхность рисования) и связываем его с формой
            // Запоминаем размеры формы
            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;
            // Связываем буфер в памяти с графическим объектом, чтобы рисовать в буфере
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));

            

            Timer timer = new Timer { Interval = 100 };
            timer.Start();
            timer.Tick += Timer_Tick;
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
            Buffer.Render();
        }

        /// <summary>
        /// инициализация массива BaseObject
        /// </summary>
        public static void Load()
        {
            _objs = new BaseObject[60];
            for (int i = 0; i < _objs.Length/3; i++)
            {
                _objs[i] = new BaseObject(new Point(600, i * 20), new Point(15 - i, 15 - i), new Size(20, 20));
            }
            for (int i = _objs.Length / 3; i < _objs.Length - 8; i++)
            {
                _objs[i] = new Star(new Point(600, i * 30-600), new Point(i, 0), new Size(5, 5));
            }
            for (int i = _objs.Length - 8; i <  _objs.Length; i++)
            {
                _objs[i] = new Meteor(new Point(i*50-2500, 0), new Point(10, (i+5)*10), new Size(10,10));
            }
        }


        public static void Update()
        {
            foreach (BaseObject obj in _objs)
                obj.Update();
        }
    }
}
