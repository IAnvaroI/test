using System;
using System.Collections.Generic;

namespace Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            Records recordsList = new Records(new List<Record>()
            {
                new Record("Smith", "Jones", new DateTime(2021, 10, 29), 5, "amniocentesis"),
                new Record("Taylor", "Williams", new DateTime(2021, 9, 1), 1, "blood test"),
                new Record("Brown", "White", new DateTime(2021, 9, 17), 3, "PET scan"),
                new Record("Harris", "Martin", new DateTime(2021, 11, 23), 6, "ultrasound"),
                new Record("Davies", "Wilson", new DateTime(2021, 10, 16), 4, "cervical smear"),
                new Record("Cooper", "Thomas", new DateTime(2021, 11, 24), 3, "autopsy"),
                new Record("King", "Evans", new DateTime(2021, 11, 24), 5, "biopsy"),
                new Record("Baker", "Green", new DateTime(2021, 12, 19), 1, "check-up"),
                new Record("Wright", "Johnson", new DateTime(2021, 10, 15), 2, "X-ray"),
                new Record("King", "Evans", new DateTime(2021, 11, 24), 5, "biopsy"),
                new Record("Baker", "Green", new DateTime(2021, 12, 19), 1, "check-up"),
                new Record("Wright", "Johnson", new DateTime(2021, 10, 15), 2, "X-ray"),
            });

            Console.WriteLine("TASK 1:");
            recordsList.Task1();
            Console.WriteLine("\nTASK 2:");
            recordsList.Task2();
            Console.WriteLine("\nTASK 3:");
            recordsList.Task3();
            Console.WriteLine("\nTASK 4:");
            recordsList.Task4();
            Console.WriteLine("\nTASK 5: in the realization of task 1, 4");
            Console.WriteLine("\nTASK 6:");
            recordsList.Task6();
            Console.WriteLine("\nTASK 7: in the realization of task 8");
            Console.WriteLine("\nTASK 8:");
            recordsList.Task8();
        }
    }
}
