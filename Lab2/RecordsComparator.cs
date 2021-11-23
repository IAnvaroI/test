using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    class RecordsComparator : IComparer<Record>
    {
        public int Compare(Record x, Record y)
        {
            return y.Priority.CompareTo(x.Priority);
        }
    }
}
