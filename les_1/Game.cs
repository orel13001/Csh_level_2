﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

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
        public static List<BaseObject> _objs;
        private static Bullet _bullet;
        private static List<UFO> _ufo;

        private static Random rnd = new Random();

        static int spd = rnd.Next(5, 10);
        static int size = rnd.Next(5, 10);

        private static Ship _ship = new Ship(new Point(30, 300), new Point(10, 10), new Size(45, 45));


        static Game() { }

        static Timer timer = new Timer { Interval = 100 };

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
                form.KeyDown += Form_KeyDown;
                Ship.MessageDie += Finish;

                timer.Start();
                timer.Tick += Timer_Tick;
            }
        }



        private static void Form_KeyDown (object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space) _bullet = new Bullet(new Point(_ship.Rect.X + _ship.Rect.Size.Width, _ship.Rect.Y + _ship.Rect.Size.Height / 2),
                new Point(40, 0), new Size(4, 1));
            if (e.KeyCode == Keys.Up) _ship.Up();
            if (e.KeyCode == Keys.Down) _ship.Down();
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
            Buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in _objs)
                obj.Draw();
            foreach (BaseObject obj in _ufo)
                obj?.Draw();
            _bullet?.Draw();
            _ship?.Draw();
            if (_ship != null)
            {
                Buffer.Graphics.DrawString("Energy: " + _ship.Energy, SystemFonts.DefaultFont, Brushes.White, 0, 0);
            }

            Buffer.Render();
        }

        /// <summary>
        /// инициализация массива BaseObject
        /// </summary>
        public static void Load()
        {
            _objs = new List<BaseObject>();
            _ufo = new List<UFO>();
            for (int i = 0; i < 33; i++)
            {
                _objs.Add(new Star(new Point(rnd.Next(0, Game.Width), rnd.Next(0, Game.Height)), new Point(-spd, spd), new Size(3, 3)));
            }
            for (int i = 0; i < 1; i++)
            {
                _objs.Add(new Meteor(new Point(rnd.Next(0, Game.Width), rnd.Next(0, 0)), new Point(spd, spd*10), new Size(3, 3)));
            }

            for (int i = 0; i < 5; i++)
            {
                _ufo.Add(new UFO(new Point(Game.Width, rnd.Next(0, Game.Height)), new Point(5, 25), new Size(45, 28)));
            }
        }

        /// <summary>
        /// Обновление положения объектов игры
        /// </summary>
        public static void Update()
        {
            _bullet?.Update();
            if (_bullet?.Rect.X == Game.Width) _bullet = null;
            foreach (BaseObject obj in _objs)
            {
                obj.Update();
            }
            for (int i = 0; i <_ufo.Count; i++)
            {
                if (_ufo[i] == null) continue;
                _ufo[i].Update();
                if (_bullet != null && _bullet.Collision(_ufo[i]))
                {
                    System.Media.SystemSounds.Hand.Play();
                    _bullet = null;
                    _ufo[i].ReDraw();
                    continue;
                }
                if (!_ship.Collision(_ufo[i])) continue;
                _ship.EnergyLow(rnd.Next(1, 10));
                _ufo[i].ReDraw();
                System.Media.SystemSounds.Asterisk.Play();
                if (_ship.Energy <= 0) _ship?.Die();
            }
        }

        public static void Finish ()
        {
            timer.Stop();
            Buffer.Graphics.DrawString("The End", new Font(FontFamily.GenericSansSerif, 60, FontStyle.Underline), Brushes.White, 200, 100);
            Buffer.Render();
        }
    }
}
