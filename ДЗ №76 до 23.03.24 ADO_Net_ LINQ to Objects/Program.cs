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
            student.FirstName = FirstNameStudents[random.Next(FirstNameStudents.Length)];
            student.LastName = LastNameStudents[random.Next(LastNameStudents.Length)];
            student.GroupNumber = GroupNumbers[random.Next(GroupNumbers.Length)];
            student.AverageGrade = Math.Round(1+4*random.NextDouble(),2);//оценка от 1 до 5
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
                if (!students.Any(s => s.FirstName == student.FirstName && s.LastName == student.LastName))
                {
                    students.Add(student);
                }
            }
            return students;
        }
        // метод для получения всех студентов
        private static void LinqMethodALL(List<Student> students)
        {
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
        //метод суммарной статистики по всем студентам
        private static void LinqCalculateStudentStatistics(List<Student> students)
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
        // метод выбора всех студенты из конкретной группы
        private static void LinqMethod1(List<Student> students)
        {
            
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
        //метод выбора студента, который получает максимальную стипендию
        private static void LinqMethod2(List<Student> students)
        {
           
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
        // метод для выбора средней стипендии по массиву
        private static void LinqMethod3(List<Student> students)
        {
         
            var averageScholarship = students.Average(s => s.Scholarship);
            Console.WriteLine($"\nСредняя стипендия: {averageScholarship}");

            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }
        //метод выбора Всех студентов, средняя оценка которых меньше или равна 3
        private static void LinqMethod4(List<Student> students)
        {
            
            var lowGradeStudents = students.Where(s => s.AverageGrade <= 3).OrderBy(s => s.AverageGrade).ThenBy(s => s.LastName); ;
            Console.WriteLine("\nСтуденты со средней оценкой меньше или равной 3:");
            foreach (var item in lowGradeStudents)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }
        // Количество рандомных студентов, которых пользователь задает самостоятельно
        // с условием отбраковки некорректных значений
        private static List<Student> QuantityInput()
        {
            int maxUniqueStudents = FirstNameStudents.Length * LastNameStudents.Length;
            Console.Write($"Введите количество студентов (не более {maxUniqueStudents}): ");
            int studentCount;
            while (!int.TryParse(Console.ReadLine(), out studentCount) || studentCount <= 0 || studentCount > maxUniqueStudents)
            {
                Console.Write($"Некорректный ввод. Пожалуйста, введите положительное число не более {maxUniqueStudents}: ");
            }
            return GenerateStudents(studentCount);
        }

        private static void Main(string[] args)
        {
            Console.WriteLine("Программа работает с LINQ to Object запросами\n");
            var students = QuantityInput();
            Console.Clear();
            LinqMethodALL(students);
            LinqCalculateStudentStatistics(students);
            LinqMethod1(students);
            LinqMethod2(students);
            LinqMethod3(students);
            LinqMethod4(students);
        }
    }
}