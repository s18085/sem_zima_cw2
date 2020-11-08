using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace cw2
{
   [XmlType("student")]
    public class Student : IEquatable<Student>
    {
        [XmlElement("imie")]
        public string Imie { get; set; }
        [XmlElement("nazwisko")]
        public string Nazwisko { get; set; }
        [XmlAttribute("indexNo")]
        public string IndexNo { get; set; }
        public Study study { get; set; }
        [XmlElement("lname")]
        public string Birthdate { get; set; }
        [XmlElement("email")]
        public string Email { get; set; }
        [XmlElement("fathersName")]
        public string FathersName { get; set; }
        [XmlElement("motherName")]
        public string MothersName { get; set; }
        static List<Student> StExtension = new List<Student>();

        public Student (string IndexNo, string Imie, string Nazwisko)
        {
            this.IndexNo = IndexNo;
            this.Imie = Imie;
            this.Nazwisko = Nazwisko;
        }
        public Student ()
        {

        }
        public bool Equals(Student other)
        {
            return  Imie.Equals(other.Imie) &&
                    Nazwisko.Equals(other.Nazwisko) &&
                    IndexNo.Equals(other.IndexNo);
        }
        public static bool AddToExtIfAbsent(Student st)
        {
            if (StExtension.Contains(st))
            {
                return false;
            }
            StExtension.Add(st);
            return true;
        }

        internal static void SerializeToFile(StreamWriter writer, string serialType)
        {
           switch (serialType.ToLower())
            {
                case "xml":
                    XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                    ns.Add("", "");
                    XmlSerializer serializer = new XmlSerializer(typeof(StudentConteiner));
                    StudentConteiner conteiner = new StudentConteiner { Studenci = StExtension };
                    serializer.Serialize(writer, conteiner, ns);
                    break;
                default:
                    throw new ArgumentException($"Serializacja: {serialType} nie jest wspierana");
            } 
        }
        public static void SaveStudentToExtension(string line, int lineNo)
        {
            var recordCorrect = true;
            string[] student = line.Split(',');
            foreach (var i in student)
            {
                if (String.IsNullOrEmpty(i) || String.IsNullOrWhiteSpace(i))
                {
                    recordCorrect = false;
                    break;
                }
            }
            if (student.Length == 9 && recordCorrect)
            {
                var st = new Student
                {
                    Imie = student[0],
                    Nazwisko = student[1],
                    study = new Study
                    {
                        Name = student[2],
                        Mode = student[3]
                    },
                    IndexNo = student[4],
                    Birthdate = student[5],
                    Email = student[6],
                    MothersName = student[7],
                    FathersName = student[8],
                };
                var elementAdded = Student.AddToExtIfAbsent(st);
                if (!elementAdded)
                {
                    throw new ArgumentException($"Linia nr {lineNo} juz raz wystapila");
                }
            }
            else
            {
                throw new ArgumentException($"Linia nr {lineNo} nie posiada odpowiedniej ilosci danych lub niektore dane są puste"); ;

            }
        }
    }
}