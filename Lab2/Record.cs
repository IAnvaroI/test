using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    class Record
    {
        private string patientLastName;
        private string doctorLastName;
        private DateTime date;
        private int priority;
        private string medicalExam;
        
        public Record(string PatientLastName, string DoctorLastName, DateTime Date, int Priority, string MedicalExam)
        {
            this.patientLastName = PatientLastName;
            this.doctorLastName = DoctorLastName;
            this.date = Date;
            this.priority = Priority;
            this.medicalExam = MedicalExam;
        }
        public Record()
        {
            this.patientLastName = "";
            this.doctorLastName = "";
            this.date = DateTime.Now;
            this.priority = 0;
            this.medicalExam = "";
        }
        
        public override string ToString()
        {
            return "Patient last name: " + this.patientLastName 
                                         + ", doctor last name: " + this.doctorLastName
                                         + ", date: " + this.date
                                         + ", priority: " + this.priority
                                         + ", medical exam: " + medicalExam;
        }

        public string PatientLastName { get => patientLastName; set => patientLastName = value; }
        public string DoctorLastName { get => doctorLastName; set => doctorLastName = value; }
        public DateTime Date { get => date; set => date = value; }
        public int Priority { get => priority; set => priority = value; }
        public string MedicalExam { get => medicalExam; set => medicalExam = value; }
    }
}
