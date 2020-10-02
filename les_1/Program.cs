using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace les_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Form form = new Form();
            form.Width = 1500;
            form.Height = 1600;
            Game.Init(form);
            form.Show();
            Game.Draw();
            Application.Run(form);
        }
    }
}
