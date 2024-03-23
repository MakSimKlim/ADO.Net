using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Xml.Linq;
using System.Threading;

namespace ДЗ__76_до_23._03._24_ADO_Net__LINQ_to_Objects
{
    class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string GroupNumber { get; set; }
        public double AverageGrade { get; set; }
        public int Scholarship { get; set; }

        public override string ToString()
        {
            return String.Format("Student: {1} {0} from group №{2}, average grade {3}, scholarship {4}",
                FirstName, LastName, GroupNumber, AverageGrade, Scholarship);
        }
    }
    internal class Program
    {
        static string[] FirstNameStudents = {
        "Petr", "Ivan", "Sergey", "Stanislav", "Georgy",
        "Valentin", "Stepan", "Vladimir", "Alexey",
        "Danil", "Alexandr", "Yuri", "Olga",
        "Irina", "Marina", "Ekaterina", "Luybov",
        "Svetlana", "Irina", "Elizaveta", "Anastasiya", "Evginya", "Elena",
        "Sophia"
        };

        static string[] LastNameStudents = {
        "Kim", "Li", "Gluhih", "Chen",
        "Tanaka", "Cato", "Park", "Shi"
        };

        static string[] GroupNumbers = {
        "101", "102", "103", "104", "105"
        };
        private static Student GetRandomStudent()
        {
            var random = new Random();
            var student = new Student();
            student.FirstName = FirstNameStudents[random.Next(FirstNameStudents.Length)];
            student.LastName = LastNameStudents[random.Next(LastNameStudents.Length)];
            student.GroupNumber = GroupNumbers[random.Next(GroupNumbers.Length)];
            student.AverageGrade = Math.Round(1+4*random.NextDouble(),2);//оценка от 1 до 5
            student.Scholarship = random.Next(1500, 20001);//Стипендия от 1500 до 20001
            return student;
        }
        private static List<Student> GenerateStudents(int count)
        {
            var students = new List<Student>();
            for (int i = 0; i < count; i++)
            {
                Thread.Sleep(5);
                students.Add(GetRandomStudent());
            }
            return students;
        }

        private static void LingMethodALL(List<Student> students)
        {
            // LINQ запрос для получения всех студентов
            var allStudents = from s in students
                              orderby s.LastName
                              select s;

            Console.WriteLine("Все студенты:");
            foreach (var item in allStudents)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }
        private static void CalculateStudentStatistics(List<Student> students)
        {
            // Общее количество студентов
            var totalStudents = students.Count;

            // Сумма стипендий всех студентов
            var totalScholarship = students.Sum(s => s.Scholarship);

            // Средняя оценка всех студентов
            var averageGrade = students.Average(s => s.AverageGrade);

            Console.WriteLine($"\nОбщее количество студентов: {totalStudents}");
            Console.WriteLine($"Сумма стипендий всех студентов: {totalScholarship}");
            Console.WriteLine($"Средняя оценка всех студентов: {averageGrade}");
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }

        private static void LingMethod1(List<Student> students)
        {
            // Все студенты из конкретной группы
            var result = from s in students
                         where s.GroupNumber == "101"
                         orderby s.LastName, s.FirstName
                         select s; 
            Console.WriteLine("Студенты из группы 101:");
            foreach (var item in result)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }
        private static void LingMethod2(List<Student> students)
        {
            // Студент, который получает максимальную стипендию
            var maxScholarship = students.Max(s => s.Scholarship);
            var maxScholarshipStudents = students.Where(s => s.Scholarship == maxScholarship);
            Console.WriteLine("\nСтудент, получающий максимальную стипендию:");
            foreach (var item in maxScholarshipStudents)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }
        private static void LingMethod3(List<Student> students)
        {
           // Средняя стипендия по массиву
            var averageScholarship = students.Average(s => s.Scholarship);
            Console.WriteLine($"\nСредняя стипендия: {averageScholarship}");

            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }
        private static void LingMethod4(List<Student> students)
        {
            // Все студенты, средняя оценка которых меньше или равна 3
            var lowGradeStudents = students.Where(s => s.AverageGrade <= 3).OrderBy(s => s.AverageGrade).ThenBy(s => s.LastName); ;
            Console.WriteLine("\nСтуденты со средней оценкой меньше или равной 3:");
            foreach (var item in lowGradeStudents)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }


        private static void Main(string[] args)
        {
            // Количество рандомных студентов
            var students = GenerateStudents(10);
            Console.WriteLine("Программа работает с LINQ to Object запросами\n");
            LingMethodALL(students);
            CalculateStudentStatistics(students);
            LingMethod1(students);
            LingMethod2(students);
            LingMethod3(students);
            LingMethod4(students);
        }
    }
}