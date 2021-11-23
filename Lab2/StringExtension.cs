using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    static class StringExtension
    {
        public static bool IsPaidMedExam(this String paidMedExam, List<string> paidMedExamsList)
        {
            if (paidMedExamsList.Contains(paidMedExam))
            {
                return true;
            }
            return false;
        }
    }
}
