using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Les_2_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            Workers[] workers = new Workers[10];

            for (int i = 0; i < workers.Length/2; i++)
            {
                workers[i] = new Workers_HourlySallary(rnd.Next(50, 100) + Math.Round(rnd.NextDouble(), 2));
            }
            for (int i = workers.Length / 2; i < workers.Length ; i++)
            {
                workers[i] = new Workers_fixedSsallary(rnd.Next(5000, 10000) + Math.Round(rnd.NextDouble(), 2));
            }

            Console.WriteLine("*** Исходный массив ***");
            for (int i = 0; i < workers.Length; i++)
            {
                Console.WriteLine($"Рабочий {i}: Зарплата {workers[i].GetSallary()}");
            }
            Console.WriteLine();

            Console.WriteLine("*** ArrWorkrs через foreach ***");
            ArrWorkers arrWork = new ArrWorkers(workers);

            int j = 0;
            foreach (var work in arrWork)
            {                
                Console.WriteLine($"Рабочий {j++} : Тип {work.GetType()} : Зарплата {work.GetSallary()}");                
            }
            Console.WriteLine();

            Console.WriteLine("*** Сортированный исходный массив ***");
            Array.Sort(workers);
            
            for (int i = 0; i < workers.Length; i++)
            {
                Console.WriteLine($"Рабочий {i} : Тип {workers[i].GetType()} : Зарплата {workers[i].GetSallary()}");
            }


            Console.ReadKey();
        }
    }
}
