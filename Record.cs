using System;

namespace Lab7
{
    public class Record
    {
        private int id;
        private string patientLastName;
        private string doctorLastName;
        private DateTime date;
        private int priority;
        private string medicalExam;
        private string tmp;

        public Record(string PatientLastName, string DoctorLastName, DateTime Date, int Priority, string MedicalExam, string Tmp)
        {
            this.patientLastName = PatientLastName;
            this.doctorLastName = DoctorLastName;
            this.date = Date;
            this.priority = Priority;
            this.medicalExam = MedicalExam;
            this.tmp = Tmp;
        }
        public Record()
        {
            this.patientLastName = "";
            this.doctorLastName = "";
            this.date = DateTime.Now;
            this.priority = 0;
            this.medicalExam = "";
            this.tmp = "";
        }

        public override string ToString()
        {
            return "Patient last name: " + this.patientLastName
                                         + ", doctor last name: " + this.doctorLastName
                                         + ", date: " + this.date
                                         + ", priority: " + this.priority
                                         + ", medical exam: " + medicalExam
                                         + ", tmp: " + tmp;
        }

        public int Id { get => id; set => id = value; }
        public string PatientLastName { get => patientLastName; set => patientLastName = value; }
        public string DoctorLastName { get => doctorLastName; set => doctorLastName = value; }
        public DateTime Date { get => date; set => date = value; }
        public int Priority { get => priority; set => priority = value; }
        public string MedicalExam { get => medicalExam; set => medicalExam = value; }
        public string Tmp { get => tmp; set => tmp = value; }
    }
}
