using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Lab2
{
    class Records
    {
        private List<string> paidMedExamsList = new List<string>(){
            "autopsy",
            "biopsy",
            "PET scan",
            "X-ray"
        };
        private List<Record> recordsList;
        
        public Records(List<Record> recordsList)
        {
            this.recordsList = recordsList;
        }
        public Records()
        {
            this.recordsList = new List<Record>();
        }

        public void Task1()
        {
            var patientsData = from record in recordsList
                                  select new
                                  {
                                      LastName = record.PatientLastName,
                                      MedExam = record.MedicalExam,
                                      DateT = record.Date
                                  };
            foreach (var patient in patientsData)
            {
                Console.WriteLine("Last name: " + patient.LastName + ", exam: " + patient.MedExam + ", date: " + patient.DateT);
            }
        }
        public void Task2()
        {
            DateTime dateTimeNow = DateTime.Now;
            var patientsLastNames = from record in recordsList
                                   where record.Date >= dateTimeNow && record.Date < dateTimeNow.AddDays(1)
                                   select record.PatientLastName;
            Console.WriteLine("Patients for nearest day:");
            foreach (var lastName in patientsLastNames)
            {
                Console.WriteLine(lastName);
            }
        }
        public void Task3()
        {
            var priorityWithPatientsDictionary = recordsList
                                    .GroupBy(record => record.Priority)
                                    .ToDictionary(group => group.Key,
                                                group => group
                                                    .Select(record => record.PatientLastName)
                                                    .ToList<string>());

            foreach (var onePair in priorityWithPatientsDictionary)
            {
                Console.Write("Priority " + onePair.Key + ":");
                foreach (var lastName in onePair.Value)
                {
                    Console.Write(" ");
                    Console.Write(lastName + ", ");
                }
                Console.Write("\n");
            }
        }
        public void Task4()
        {
            var paidPatients = from record in recordsList
                where record.MedicalExam.IsPaidMedExam(paidMedExamsList)
                select new
                {
                    LastName = record.PatientLastName,
                    Prior = record.Priority,
                    DateT = record.Date
                };
            Console.WriteLine("Patients for nearest day:");
            foreach (var patient in paidPatients)
            {
                Console.WriteLine("Last name: " + patient.LastName + ", priority: " + patient.Prior + ", date: " +
                                  patient.DateT);
            }
        }

        public void Task6()
        {
            recordsList.Sort(new RecordsComparator());
            Console.Write(this);
        }

        public void Task8()
        {
            Record[] recordArray = null;
            recordArray = recordsList.OrderByDescending(record => record.Date).ToArray();
            foreach (var record in recordArray)
            {
                Console.WriteLine(record);
            }
        }

       
        public override string ToString()
        {
            string result = "";

            foreach (var record in recordsList)
            {
                result += record + "\n";
            }

            return result;
        }

        public List<Record> RecordsList { get => recordsList; set => recordsList = value; }

    }
}
