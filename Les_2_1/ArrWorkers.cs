using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Les_2_1
{
    class ArrWorkers : IEnumerable<Workers>
    {
        private Workers[] _workers;
        
        public ArrWorkers(Workers[] arrWork)
        {
            _workers = new Workers[arrWork.Length]; 
            for (int i = 0; i < arrWork.Length; i++)
            {
                _workers[i] = arrWork[i];
            }
        }


        public IEnumerator<Workers> GetEnumerator()
        {
            for (int i = 0; i < _workers.Length; i++)
            {
                yield return _workers[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
