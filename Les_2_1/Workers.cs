using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Les_2_1
{
    abstract class Workers: IComparable<Workers>
    {
        public int CompareTo(Workers other)
        {
            if (this.GetSallary() < other.GetSallary()) return -1;
            if (this.GetSallary() > other.GetSallary()) return 1;
            return 0;
        }

        /// <summary>
        /// Расчёт ЗП работника
        /// </summary>
        /// <returns>ЗП</returns>
        abstract public double GetSallary();        
    }

    class Workers_HourlySallary : Workers
    {
        private double hourlyPay;

        public Workers_HourlySallary(double hourlyPay)
        {
            this.hourlyPay = hourlyPay;
        }

        public override double GetSallary()
        {
            return 20.8*8*hourlyPay;
        }

        
    }

    class Workers_fixedSsallary : Workers
    {
        private double fixedPay;


        public Workers_fixedSsallary(double fixedPay)
        {
            this.fixedPay = fixedPay;
        }


        public override double GetSallary()
        {
            return fixedPay;
        }
    }
}
