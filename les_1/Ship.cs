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

        /// <summary>
        /// Вызывется при столкновении корабля с НЛО 
        /// </summary>
        public static event Action<string> CollisionShip;
        /// <summary>
        /// Вызывется при лечении корабля
        /// </summary>
        public static event Action<string> Helthing;

        /// <summary>
        /// Вызывется при уничтожении корабля
        /// </summary>
        public static event Message MessageDie;

        private int _point = 0;
        private int _energy = 100;
        public int Energy => _energy;
        public int _Point => _point;
        /// <summary>
        /// прямоугольник для определения пересечеиня с иными объектами
        /// </summary>
        public Rectangle Rect => new Rectangle(Pos, Size);

        private static Image img = Image.FromFile(@"img/Ship.png");

        /// <summary>
        /// Очки за сбитые НЛО
        /// </summary>
        public void AddPoint()
        {
            _point++;
            Helthing?.Invoke($"{DateTime.Now}: Уничтожено НЛО");
        }
        /// <summary>
        /// Уменьшение очков здоровья
        /// </summary>
        /// <param name="n"></param>
        public void EnergyLow (int n)
        {
            _energy -= n;
            Helthing?.Invoke($"{DateTime.Now}: Столкновение с НЛО. Здоровье уменьшено на {n}");
        }
        /// <summary>
        /// Увеличение очков здоровья
        /// </summary>
        public void EnergyUp (int n)
        {
            if(_energy < 100)
            {
                _energy = Math.Min(100, _energy += n);
                Helthing?.Invoke($"{DateTime.Now}: Здоровье увеличено на {n}");
            }
        }
        
        public Ship(Point pos, Point dir, Size size) : base(pos, dir, size) { }
        /// <summary>
        /// отрисовка корабля
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(img, Pos.X, Pos.Y);
        }
        /// <summary>
        /// обновление позиции корабля
        /// </summary>
        public override void Update()
        {
        }
        /// <summary>
        /// смещение корабля вверх
        /// </summary>
        public void Up()
        {
            if (Pos.Y > 0) Pos.Y = Pos.Y - Dir.Y;
        }
        /// <summary>
        /// смещение корабля вниз
        /// </summary>
        public void Down()
        {
            if (Pos.Y < Game.Height-Size.Height) Pos.Y = Pos.Y + Dir.Y;
        }

        /// <summary>
        /// гибель корабля
        /// </summary>
        public void Die()
        {
            CollisionShip?.Invoke($"{DateTime.Now}: Корабль уничножен. Игра окончена");
            MessageDie?.Invoke();
        }
        /// <summary>
        /// определение пересечеиня с иными объектами
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public bool Collision(ICollision o)
        {
            
            return o.Rect.IntersectsWith(this.Rect);
        }

    }
}
