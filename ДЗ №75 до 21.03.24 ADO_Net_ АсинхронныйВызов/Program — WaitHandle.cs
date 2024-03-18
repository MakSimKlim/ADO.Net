using System;
using System.Data.SqlClient;
using System.Threading;

namespace ДЗ__75_до_21._03._24_ADO_Net__АсинхронныйВызов
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=VANYACOMP;Initial Catalog=ФирмаКанцтоваров;Integrated Security=True;Connect Timeout=30";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Подключение открыто");

                    ShowAllSalesManagersUp(connection);
                    ShowProductsWithMaxQuantityUp(connection, "100");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("\nОШИБКА: " + ex.Message);
                }
                finally
                {
                    // Закрыть подключение
                    connection.Close();
                    Console.WriteLine("Подключение закрыто");
                }
            }
            Console.ReadKey();
        }

        // все менеджеры по продажам в порядке возрастания их продаж
        static void ShowAllSalesManagersUp(SqlConnection connection)
        {
            string query = @"
            SELECT Менеджеры.Фамилия, Менеджеры.Имя, COUNT(Продажи.ПродажаID) as ОбщиеПродажи FROM Менеджеры
            INNER JOIN Продажи ON Менеджеры.МенеджерID = Продажи.МенеджерID GROUP BY Менеджеры.Фамилия, Менеджеры.Имя  ORDER BY ОбщиеПродажи ASC";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                IAsyncResult result = command.BeginExecuteReader();
                result.AsyncWaitHandle.WaitOne();
                using (SqlDataReader reader = command.EndExecuteReader(result))
                {
                    Console.WriteLine("\nВсе менеджеры по продажам в порядке возрастания их продаж:");
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["Фамилия"]} {reader["Имя"]}, Продажи: {reader["ОбщиеПродажи"]}");
                    }
                }
            }
        }

        //все канцтовары ниже определённой стоимости, отсортированные по возрастанию оставшегося количества
        static void ShowProductsWithMaxQuantityUp(SqlConnection connection, string cost)
        {
            string query = @"
            SELECT * FROM Товары WHERE Себестоимость < @cost ORDER BY Количество ASC";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@cost", cost);
                IAsyncResult result = command.BeginExecuteReader();
                result.AsyncWaitHandle.WaitOne();
                using (SqlDataReader reader = command.EndExecuteReader(result))
                {
                    Console.WriteLine($"\nВсе канцтовары ниже стоимости = '{cost}', отсортированные по возрастанию оставшегося количества:");
                    while (reader.Read())
                    {
                        Console.WriteLine($"Название: {reader["Название"]}, Количество: {reader["Количество"]}");
                    }
                }
            }
        }
    }
}
