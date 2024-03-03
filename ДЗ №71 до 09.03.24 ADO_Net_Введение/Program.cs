using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Runtime.Remoting.Messaging;
using System.Text.RegularExpressions;


namespace ДЗ__71_до_09._03._24_ADO_Net_Введение
{
    internal class Program
    {
        SqlConnection conn = null;
        public Program()
        {
            conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=VANYACOMP;Initial Catalog=VegetablesFruits;Integrated Security=True;Connect Timeout=30;";
        }
        static void Main(string[] args)
        {
            Program pr = new Program();
            //pr.InsertQuery(); // метод преднзначен для выполнения запросов insert, update и delete
            //pr.ReadData(); //метод предназначен для выполнения одного запроса select
            pr.ReadDataSeveral(); //метод предназначен для выполнения нескольких запросов select

        }

        /*public void InsertQuery()
        {
            try
            {
                //открыть соединение
                conn.Open();

                //подготовить запрос insert
                //в переменной типа string
                string insertString = @"insert into
VegetableFruitTable (Title, Typing, Colour, Calories)
values ('Арбуз', 'Фрукт', 'Зеленый', 30)";

                //создать объект command,
                //инициализировав оба свойства
                SqlCommand cmd = new SqlCommand(insertString, conn);
                
                //выполнить запрос, занесенный
                //в объект command
                // метод преднзначен для выполнения запросов insert, update и delete
                cmd.ExecuteNonQuery();
            }
            finally
            {
                // закрыть соединение
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }*/

        /*public void ReadData()
        {
            SqlDataReader rdr = null;
            try
            {
                //открыть соединение
                conn.Open();
                Console.WriteLine("Подключение успешно установлено!\n");

                //создать новый объект command с запросом select
                SqlCommand cmd = new SqlCommand("select * from VegetableFruitTable", conn);

                //выполнить запрос select, сохранив
                //возвращенный результат
                //метод предназначен для выполнения запросов select
                rdr = cmd.ExecuteReader();
                int line = 0;//счетчик строк

                //извлечь полученные строки
                while (rdr.Read())
                {
                    //формируем шапку таблицы перед выводом
                    //первой строки
                    if (line == 0)
                    {
                        //цикл по числу прочитанных полей
                        for (int i = 0; i < rdr.FieldCount; i++)
                        {
                            //вывести в консольное окно имена полей
                            Console.Write(rdr.GetName(i).ToString() + " ");
                        }
                        Console.Write("\n");
                    }
                    Console.WriteLine();
                    line++;

                    Console.WriteLine(rdr["Id"] + " " + rdr["Title"] + " " + rdr["Typing"] + " " + rdr["Colour"] + " " + rdr["Calories"]);
                    // записать можно и так:
                    //Console.WriteLine(rdr[1] + " " + rdr[2] + " " + rdr[3] + " " + rdr[4]);
                }
                Console.Write("\n");
                Console.WriteLine("Обработано записей: " + line.ToString());
                Console.ReadKey();
            }
            catch (SqlException ex)
            {
                // Выводим детали ошибки
                Console.WriteLine("Ошибка при подключении к базе данных: " + ex.Message);
            }
            finally
            {
                //закрыть reader
                if (rdr != null)
                {
                    rdr.Close();
                }
                //закрыть соединение
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }*/

        public void ReadDataSeveral()
        {
            SqlDataReader rdr = null;
            try
            {
                //открыть соединение
                conn.Open();
                Console.WriteLine("Подключение успешно установлено!\n");

                //создать новый объект command с запросом select
                SqlCommand cmd = new SqlCommand(
            "SELECT * FROM VegetableFruitTable;" +
            "SELECT Title FROM VegetableFruitTable;" +
            "SELECT DISTINCT Colour FROM VegetableFruitTable;" +
            "SELECT MAX(Calories) FROM VegetableFruitTable;" +
            "SELECT MIN(Calories) FROM VegetableFruitTable;" +
            "SELECT AVG(Calories) FROM VegetableFruitTable;" +
            "SELECT COUNT(*) FROM VegetableFruitTable WHERE Typing = 'Овощ';" +
            "SELECT COUNT(*) FROM VegetableFruitTable WHERE Typing = 'Фрукт';" +
            "SELECT COUNT(*) FROM VegetableFruitTable WHERE Colour = 'Зеленый';" +
            "SELECT Colour, COUNT(*) FROM VegetableFruitTable GROUP BY Colour;" +
            "SELECT * FROM VegetableFruitTable WHERE Calories < 40;" +
            "SELECT * FROM VegetableFruitTable WHERE Calories > 40;" +
            "SELECT * FROM VegetableFruitTable WHERE Calories BETWEEN 20 AND 40;" +
            "SELECT * FROM VegetableFruitTable WHERE Colour IN('Желтый', 'Красный')",
            conn);

                //выполнить запрос select, сохранив
                //возвращенный результат
                //метод предназначен для выполнения запросов select
                rdr = cmd.ExecuteReader();
               
                //извлечь полученные строки
                do
                {
                    while (rdr.Read())
                    {
                        //формируем шапку таблицы перед выводом
                        //первой строки
                        
                            //цикл по числу прочитанных полей
                            for (int i = 0; i < rdr.FieldCount; i++)
                            {
                            //вывести в консольное окно имена полей
                            Console.Write($"{rdr.GetName(i)}: {rdr[i]} \t");
                            }
                            Console.WriteLine();
                    }
                    Console.WriteLine("\n");

                } while (rdr.NextResult());

                Console.ReadKey();
            }


            catch (SqlException ex)
            {
                // Выводим детали ошибки
                Console.WriteLine("Ошибка при подключении к базе данных: " + ex.Message);
            }
            finally
            {
                //закрыть reader
                if (rdr != null)
                {
                    rdr.Close();
                }
                //закрыть соединение
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
    }
}
    

