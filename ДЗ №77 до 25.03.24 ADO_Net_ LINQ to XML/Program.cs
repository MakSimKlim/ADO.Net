using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;


namespace ДЗ__77_до_25._03._24_ADO_Net__LINQ_to_XML
{
    class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string GroupNumber { get; set; }
        public double AverageGrade { get; set; }
        public int Scholarship { get; set; }

    }
    internal class Program
    {
        //список имен
        static string[] FirstNameStudents = {
        "Petr", "Ivan", "Sergey", "Stanislav", "Georgy",
        "Valentin", "Stepan", "Vladimir", "Alexey",
        "Danil", "Alexandr", "Yuri", "Olga",
        "Irina", "Marina", "Ekaterina", "Luybov",
        "Svetlana", "Irina", "Elizaveta", "Anastasiya", "Evginya", "Elena",
        "Sophia"
        };
        //список фамилий
        static string[] LastNameStudents = {
        "Kim", "Li", "Gluhih", "Chen",
        "Tanaka", "Cato", "Park", "Shi"
        };
        //список групп
        static string[] GroupNumbers = {
        "101", "102", "103", "104", "105"
        };
        // метод для создания рандомных значений из списков значений
        private static Student GetRandomStudent(Random random)
        {
            var student = new Student();
            student.LastName = LastNameStudents[random.Next(LastNameStudents.Length)];
            student.FirstName = FirstNameStudents[random.Next(FirstNameStudents.Length)];
            student.GroupNumber = GroupNumbers[random.Next(GroupNumbers.Length)];
            student.AverageGrade = Math.Round(1 + 4 * random.NextDouble(), 2);//оценка от 1 до 5
            student.Scholarship = random.Next(1500, 20001);//Стипендия от 1500 до 20001
            return student;
        }
        // метод, который генерирует список уникальных студентов, чтобы все методы работали с одним и тем же набором данных
        private static List<Student> GenerateStudents(int count)
        {
            var students = new List<Student>();
            var random = new Random();
            while (students.Count < count)
            {
                var student = GetRandomStudent(random);
                students.Add(student);
            }
            return students;
        }
        // метод для записи результатов в несколько файлов xml
        private static void WriteToXml(List<Student> students, string fileName)
        {
            var xdoc = new XDocument();
            var xroot = new XElement("students");
            foreach (var student in students)
            {
                var xstudent = new XElement("student",
                    new XAttribute("LastName", student.LastName),
                    new XAttribute("FirstName", student.FirstName),
                    new XAttribute("GroupNumber", student.GroupNumber),
                    new XAttribute("Scholarship", student.Scholarship)
                );
                xroot.Add(xstudent);
            }
            xdoc.Add(xroot);
            xdoc.Save(fileName);
        }

        // метод выбора всех студенты из конкретной группы
        private static void WriteGroup101(List<Student> students)
        {
            var group101 = students.Where(s => s.GroupNumber == "101").OrderBy(s => s.LastName).ToList();
            WriteToXml(group101, "group101.xml");
        }

        //метод выбора студента, который получает максимальную стипендию
        private static void WriteMaxScholarshipStudents(List<Student> students)
        {
            var maxScholarship = students.Max(s => s.Scholarship);
            var maxScholarshipStudents = students.Where(s => s.Scholarship == maxScholarship).ToList();
            WriteToXml(maxScholarshipStudents, "maxScholarship.xml");
        }

        // метод для выбора средней стипендии по массиву
        private static void WriteAvgScholarship(List<Student> students)
        {
            var avgScholarship = students.Average(s => s.Scholarship);
            var xdocAvgScholarship = new XDocument(new XElement("AverageScholarship", avgScholarship));
            xdocAvgScholarship.Save("avgScholarship.xml");
        }

        //метод выбора Всех студентов, средняя оценка которых меньше или равна 3
        private static void WriteLowGradeStudents(List<Student> students)
        {
            var lowGradeStudents = students.Where(s => s.AverageGrade <= 3).ToList();
            WriteToXml(lowGradeStudents, "lowGrade.xml");
        }

        private static void Main(string[] args)
        {
            var students = GenerateStudents(10);// задать необходимое количество студентов
            WriteToXml(students, "students.xml");
            WriteGroup101(students);
            WriteMaxScholarshipStudents(students);
            WriteAvgScholarship(students);
            WriteLowGradeStudents(students);
        }
    }
}