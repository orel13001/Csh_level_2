using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace les_1
{
    interface ICollision
    {
        bool Collision(ICollision obj);
        Rectangle Rect { get; }
        void ReDraw(Point pt);
    }
}
