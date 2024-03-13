using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ДЗ__73_до_14._03._24_ADO_Net_ПрисоедРежим2
{
    internal class Program
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

                    ShowAllProducts(connection);
                    ShowAllProductTypes(connection);
                    ShowAllSalesManagers(connection);
                    ShowProductsWithMaxQuantity(connection);
                    ShowProductsWithMinQuantity(connection);
                    ShowProductsWithMinCost(connection);
                    ShowProductsWithMaxCost(connection);
                    
                    
                    ShowProductsByType(connection, "Бумажные изделия");
                    ShowProductsSoldByManager(connection, "Сергеев");
                    ShowProductsBoughtByCompany(connection, "ЗАО Сфера");
                    ShowMostRecentSale(connection);
                    ShowAverageQuantityByProductType(connection);



                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Ошибка подключения: " + ex.Message);
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

        //Отображение всей информации о канцтоварах
        static void ShowAllProducts(SqlConnection connection)
        {
            string query = @"
             SELECT Товары.ТоварID, Товары.Название, Товары.Тип, Товары.Количество, Товары.Себестоимость, 
             SUM(Продажи.КоличествоПроданных) as ПроданоЕдиниц, AVG(Продажи.ЦенаЗаЕдиницу) as СредняяЦенаЗаЕдиницу,
             (Товары.Количество - ISNULL(SUM(Продажи.КоличествоПроданных), 0)) as ОстатокТовара
             FROM Товары 
             LEFT JOIN Продажи ON Товары.ТоварID = Продажи.ТоварID
             GROUP BY Товары.ТоварID, Товары.Название, Товары.Тип, Товары.Количество, Товары.Себестоимость";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["ТоварID"]}, Название: {reader["Название"]}, Тип: {reader["Тип"]}, Количество: {reader["Количество"]}, Себестоимость: {reader["Себестоимость"]}, Продано единиц: {reader["ПроданоЕдиниц"]}, Средняя цена за единицу: {reader["СредняяЦенаЗаЕдиницу"]}, Остаток товара: {reader["ОстатокТовара"]}");
                    }
                }
            }
        }


        //Отображение всех типов канцтоваров
        static void ShowAllProductTypes(SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand("SELECT DISTINCT Тип FROM Товары", connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    Console.WriteLine("\nВсех типы канцтоваров:");
                    while (reader.Read())
                    {
                        Console.WriteLine($"Тип: {reader["Тип"]}");
                    }
                }
            }
        }

        //Отображение всех менеджеров по продажам
        static void ShowAllSalesManagers(SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand("SELECT * FROM Менеджеры", connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    Console.WriteLine("\nВсе менеджеры по продажам:");
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["Фамилия"]} {reader["Имя"]}");
                    }
                }
            }
        }

        //канцтовары с максимальным количеством единиц
        static void ShowProductsWithMaxQuantity(SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand("SELECT * FROM Товары WHERE Количество = (SELECT MAX(Количество) FROM Товары)", connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    Console.WriteLine("\nКанцтовары с максимальным количеством единиц:");
                    while (reader.Read())
                    {
                        Console.WriteLine($"Название: {reader["Название"]}, Количество: {reader["Количество"]}");
                    }
                }
            }
        }

        //канцтовары с минимальным количеством единиц
        static void ShowProductsWithMinQuantity(SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand("SELECT * FROM Товары WHERE Количество = (SELECT MIN(Количество) FROM Товары)", connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    Console.WriteLine("\nКанцтовары с минимальным количеством единиц:");
                    while (reader.Read())
                    {
                        Console.WriteLine($"Название: {reader["Название"]}, Количество: {reader["Количество"]}");
                    }
                }
            }
        }

        //канцтовары с минимальной себестоимостью единицы
        static void ShowProductsWithMinCost(SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand("SELECT * FROM Товары WHERE Себестоимость = (SELECT MIN(Себестоимость) FROM Товары)", connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    Console.WriteLine("\nКанцтовары с минимальным себестоимостью единицы:");
                    while (reader.Read())
                    {
                        Console.WriteLine($"Название: {reader["Название"]}, Себестоимость: {reader["Себестоимость"]}");
                    }
                }
            }
        }

        //канцтовары с максимальной себестоимостью единицы
        static void ShowProductsWithMaxCost(SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand("SELECT * FROM Товары WHERE Себестоимость = (SELECT MAX(Себестоимость) FROM Товары)", connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    Console.WriteLine("\nКанцтовары с максимальной себестоимостью единицы:");
                    while (reader.Read())
                    {
                        Console.WriteLine($"Название: {reader["Название"]}, Себестоимость: {reader["Себестоимость"]}");
                    }
                }
            }
        }

        //канцтовары, заданного типа
        static void ShowProductsByType(SqlConnection connection, string type)
        {
            string query = "SELECT * FROM Товары WHERE Тип = @type";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@type", type);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    Console.WriteLine($"\nКанцтовары типа {type}:");
                    while (reader.Read())
                    {
                        Console.WriteLine($"Название: {reader["Название"]}, Количество: {reader["Количество"]}, Себестоимость: {reader["Себестоимость"]}");
                    }
                }
            }
        }

        //канцтовары, которые продал конкретный менеджер по продажам
        static void ShowProductsSoldByManager(SqlConnection connection, string managerName)
        {
            string query = @"
             SELECT Товары.Название, Товары.Тип, Продажи.КоличествоПроданных
             FROM Товары
             INNER JOIN Продажи ON Товары.ТоварID = Продажи.ТоварID
             INNER JOIN Менеджеры ON Продажи.МенеджерID = Менеджеры.МенеджерID
             WHERE Менеджеры.Фамилия = @managerName";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@managerName", managerName);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    Console.WriteLine($"\nКанцтовары, проданные менеджером {managerName}:");
                    while (reader.Read())
                    {
                        Console.WriteLine($"Название: {reader["Название"]}, Тип: {reader["Тип"]}, Продано единиц: {reader["КоличествоПроданных"]}");
                    }
                }
            }
        }

        //канцтовары, которые купила конкретная фирма покупатель
        static void ShowProductsBoughtByCompany(SqlConnection connection, string companyName)
        {
            string query = @"
             SELECT Товары.Название, Продажи.КоличествоПроданных
             FROM Товары
             INNER JOIN Продажи ON Товары.ТоварID = Продажи.ТоварID
             INNER JOIN Фирмы ON Продажи.ФирмаID = Фирмы.ФирмаID
             WHERE Фирмы.Название = @companyName";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@companyName", companyName);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    Console.WriteLine($"\nКанцтовары, купленные фирмой {companyName}:");
                    while (reader.Read())
                    {
                        Console.WriteLine($"Название: {reader["Название"]}, Количество: {reader["КоличествоПроданных"]}");
                    }
                }
            }
        }

        // информацию о самой недавней продаже
        static void ShowMostRecentSale(SqlConnection connection)
        {
            string query = @"
             SELECT TOP 1 Товары.Название, Продажи.КоличествоПроданных, Продажи.ДатаПродажи
             FROM Товары
             INNER JOIN Продажи ON Товары.ТоварID = Продажи.ТоварID
             ORDER BY Продажи.ДатаПродажи DESC";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    Console.WriteLine("\nИнформация о самой недавней продаже:");
                    while (reader.Read())
                    {
                        Console.WriteLine($"Название: {reader["Название"]}, Количество: {reader["КоличествоПроданных"]}, Дата продажи: {reader["ДатаПродажи"]}");
                    }
                }
            }
        }

        // среднее количество товаров по каждому типу канцтоваров
        // с применением пользовательской функции GetAverageQuantityByType
        static void ShowAverageQuantityByProductType(SqlConnection connection)
        {
            string query = @"
        SELECT Тип, dbo.GetAverageQuantityByType(Тип) as СреднееКоличество
        FROM Товары
        GROUP BY Тип";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    Console.WriteLine("\nСреднее количество товаров по каждому типу канцтоваров:");
                    while (reader.Read())
                    {
                        Console.WriteLine($"Тип: {reader["Тип"]}, Среднее количество: {reader["СреднееКоличество"]}");
                    }
                }
            }
        }


    }
}
