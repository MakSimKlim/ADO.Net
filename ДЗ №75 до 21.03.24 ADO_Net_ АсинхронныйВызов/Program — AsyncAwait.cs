using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ДЗ__75_до_21._03._24_ADO_Net__АсинхронныйВызов
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string connectionString = "Data Source=VANYACOMP;Initial Catalog=ФирмаКанцтоваров;Integrated Security=True;Connect Timeout=30";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    Console.WriteLine("Подключение открыто");

                    await ShowAllSalesManagersUp(connection);
                    await ShowProductsWithMaxQuantityUp(connection, "100");
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
        static async Task ShowAllSalesManagersUp(SqlConnection connection)
        {
            string query = @"
            SELECT Менеджеры.Фамилия, Менеджеры.Имя, COUNT(Продажи.ПродажаID) as ОбщиеПродажи FROM Менеджеры
            INNER JOIN Продажи ON Менеджеры.МенеджерID = Продажи.МенеджерID GROUP BY Менеджеры.Фамилия, Менеджеры.Имя  ORDER BY ОбщиеПродажи ASC";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    Console.WriteLine("\nВсе менеджеры по продажам в порядке возрастания их продаж:");
                    while (await reader.ReadAsync())
                    {
                        Console.WriteLine($"{reader["Фамилия"]} {reader["Имя"]}, Продажи: {reader["ОбщиеПродажи"]}");
                    }
                }
            }
        }

        //все канцтовары ниже определённой стоимости, отсортированные по возрастанию оставшегося количества
        static async Task ShowProductsWithMaxQuantityUp(SqlConnection connection, string cost)
        {
            string query = @"
            SELECT * FROM Товары WHERE Себестоимость < @cost ORDER BY Количество ASC";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@cost", cost);
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    Console.WriteLine($"\nВсе канцтовары ниже стоимости = '{cost}', отсортированные по возрастанию оставшегося количества:");
                    while (await reader.ReadAsync())
                    {
                        Console.WriteLine($"Название: {reader["Название"]}, Количество: {reader["Количество"]}");
                    }
                }
            }
        }
    }
}
