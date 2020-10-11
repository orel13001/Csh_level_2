using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace les_4_2
{
    class Program
    {
        static Random rnd = new Random();
        static void Main(string[] args)
        {
            List<int> intList = new List<int>();
            for (int i = 0; i < 20; i++)
            {
                intList.Add(rnd.Next(0, 10));
                Console.Write(intList[i] + " ");
            }
            Console.WriteLine();
            int search = rnd.Next(0, 10);
            Console.WriteLine($"Количество элементов {search}: {CountElement<int>(intList, search)}");

            List<char> charList = new List<char>();
            for (int i = 0; i < 20; i++)
            {
                charList.Add(Convert.ToChar(rnd.Next(100, 110)));
                Console.Write(charList[i] + " ");
            }
            Console.WriteLine();
            char chr = Convert.ToChar(rnd.Next(100, 110));
            Console.WriteLine($"Количество элементов {chr}: {CountElement<char>(charList, chr)}");

            List<string> strList = new List<string>();
            strList.Add("Миша");
            strList.Add("Саша");
            strList.Add("Маша");
            strList.Add("Петя");
            strList.Add("Витя");
            strList.Add("Паша");
            strList.Add("Саша");
            strList.Add("Миша");
            strList.Add("Дима");
            strList.Add("Рома");
            strList.Add("Рома");
            strList.Add("Миша");
            strList.Add("Миша");
            for (int i = 0; i < strList.Count; i++)
            {
                Console.Write(strList[i] + " ");
            }
            Console.WriteLine();
            string str = "Миша";
            Console.WriteLine($"Количество элементов {str}: {CountElement<string>(strList, str)}");



            Console.ReadKey();
        }

    






        static int CountElement<T> (List<T> lst, T search)
        {
            return (from element in lst
                         where element.Equals(search)
                         select element).Count();
             
        }
    }
}
