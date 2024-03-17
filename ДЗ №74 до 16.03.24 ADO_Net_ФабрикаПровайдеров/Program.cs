using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Common;
using System.Configuration;
using System.Data.OleDb;
using System.Collections;

namespace ДЗ__74_до_16._03._24_ADO_Net_ФабрикаПровайдеров
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string provider = "System.Data.SqlClient";
            string connectionString = "Data Source=VANYACOMP;Initial Catalog=ФирмаКанцтоваров;Integrated Security=True;Connect Timeout=30";
            
            //string provider = "System.Data.OleDb";
            //string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=YourDatabasePath";

            DbProviderFactory dbf = DbProviderFactories.GetFactory(provider);

            using (DbConnection connection = dbf.CreateConnection())
            {

                try
                {
                    connection.ConnectionString = connectionString;
                    connection.Open();
                    Console.WriteLine("Подключение открыто");

                    DbCommand cmd = dbf.CreateCommand();
                    cmd.Connection = connection;   

                    ShowAllProducts(connection, dbf);
                    ShowAllProductTypes(connection, dbf);
                    ShowAllSalesManagers(connection, dbf);
                    ShowProductsWithMaxQuantity(connection, dbf);
                    ShowProductsWithMinQuantity(connection, dbf);
                    ShowProductsWithMinCost(connection, dbf);
                    ShowProductsWithMaxCost(connection, dbf);


                    ShowProductsByType(connection, dbf, "Бумажные изделия");
                    ShowProductsSoldByManager(connection, dbf, "Сергеев");
                    ShowProductsBoughtByCompany(connection, dbf, "ЗАО Сфера");
                    ShowMostRecentSale(connection, dbf);
                    ShowAverageQuantityByProductType(connection, dbf);

                    //новое задание
                    ShowAllSalesManagersUp(connection, dbf);
                    ShowProductsWithMaxQuantityUp(connection, dbf, "100");



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

        //Отображение всей информации о канцтоварах
        static void ShowAllProducts(DbConnection connection, DbProviderFactory dbf)
        {
            string query = @"
             SELECT Товары.ТоварID, Товары.Название, Товары.Тип, Товары.Количество, Товары.Себестоимость, 
             SUM(Продажи.КоличествоПроданных) as ПроданоЕдиниц, AVG(Продажи.ЦенаЗаЕдиницу) as СредняяЦенаЗаЕдиницу,
             (Товары.Количество - ISNULL(SUM(Продажи.КоличествоПроданных), 0)) as ОстатокТовара
             FROM Товары 
             LEFT JOIN Продажи ON Товары.ТоварID = Продажи.ТоварID
             GROUP BY Товары.ТоварID, Товары.Название, Товары.Тип, Товары.Количество, Товары.Себестоимость";
            

            using (DbCommand command = dbf.CreateCommand())
            {
                command.Connection = connection;
                command.CommandText = query;

                using (DbDataReader reader = command.ExecuteReader())
                {
                    Console.WriteLine("\nВся информация о канцтоварах:");

                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["ТоварID"]}, Название: {reader["Название"]}, Тип: {reader["Тип"]}, Количество: {reader["Количество"]}, Себестоимость: {reader["Себестоимость"]}, Продано единиц: {reader["ПроданоЕдиниц"]}, Средняя цена за единицу: {reader["СредняяЦенаЗаЕдиницу"]}, Остаток товара: {reader["ОстатокТовара"]}");
                    }
                }
            }
        }


        //Отображение всех типов канцтоваров
        static void ShowAllProductTypes(DbConnection connection, DbProviderFactory dbf)
        {
            string query = @"
            SELECT DISTINCT Тип FROM Товары";

            using (DbCommand command = dbf.CreateCommand())
            {
                command.Connection = connection;
                command.CommandText = query;

                using (DbDataReader reader = command.ExecuteReader())
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
        static void ShowAllSalesManagers(DbConnection connection, DbProviderFactory dbf)
        {
            string query = @"
            SELECT * FROM Менеджеры";

            using (DbCommand command = dbf.CreateCommand())
            {
                command.Connection = connection;
                command.CommandText = query;

                using (DbDataReader reader = command.ExecuteReader())
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
        static void ShowProductsWithMaxQuantity(DbConnection connection, DbProviderFactory dbf)
        {
            string query = @"
            SELECT * FROM Товары WHERE Количество = (SELECT MAX(Количество) FROM Товары)";

            using (DbCommand command = dbf.CreateCommand())
            {
                command.Connection = connection;
                command.CommandText = query;

                using (DbDataReader reader = command.ExecuteReader())
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
        static void ShowProductsWithMinQuantity(DbConnection connection, DbProviderFactory dbf)
        {
            string query = @"
           SELECT * FROM Товары WHERE Количество = (SELECT MIN(Количество) FROM Товары)";

            using (DbCommand command = dbf.CreateCommand())
            {
                command.Connection = connection;
                command.CommandText = query;

                using (DbDataReader reader = command.ExecuteReader())
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
        static void ShowProductsWithMinCost(DbConnection connection, DbProviderFactory dbf)
        {
            string query = @"
           SELECT * FROM Товары WHERE Себестоимость = (SELECT MIN(Себестоимость) FROM Товары)";

            using (DbCommand command = dbf.CreateCommand())
            {
                command.Connection = connection;
                command.CommandText = query;

                using (DbDataReader reader = command.ExecuteReader())
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
        static void ShowProductsWithMaxCost(DbConnection connection, DbProviderFactory dbf)
        {
            string query = @"
           SELECT * FROM Товары WHERE Себестоимость = (SELECT MAX(Себестоимость) FROM Товары)";

            using (DbCommand command = dbf.CreateCommand())
            {
                command.Connection = connection;
                command.CommandText = query;

                using (DbDataReader reader = command.ExecuteReader())
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
        static void ShowProductsByType(DbConnection connection, DbProviderFactory dbf, string type)
        {
            string query = @"
           SELECT * FROM Товары WHERE Тип = @type";

            using (DbCommand command = dbf.CreateCommand())
            {
                command.Connection = connection;
                command.CommandText = query;

                DbParameter paramType = command.CreateParameter();
                paramType.ParameterName = "@type";
                paramType.Value = type;
                command.Parameters.Add(paramType);

                using (DbDataReader reader = command.ExecuteReader())
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
        static void ShowProductsSoldByManager(DbConnection connection, DbProviderFactory dbf, string managerName)
        {
            string query = @"
             SELECT Товары.Название, Товары.Тип, Продажи.КоличествоПроданных
             FROM Товары
             INNER JOIN Продажи ON Товары.ТоварID = Продажи.ТоварID
             INNER JOIN Менеджеры ON Продажи.МенеджерID = Менеджеры.МенеджерID
             WHERE Менеджеры.Фамилия = @managerName";

            using (DbCommand command = dbf.CreateCommand())
            {
                command.Connection = connection;
                command.CommandText = query;

                DbParameter paramManagerName = command.CreateParameter();
                paramManagerName.ParameterName = "@managerName";
                paramManagerName.Value = managerName;
                command.Parameters.Add(paramManagerName);

                using (DbDataReader reader = command.ExecuteReader())
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
        static void ShowProductsBoughtByCompany(DbConnection connection, DbProviderFactory dbf, string companyName)
        {
            string query = @"
             SELECT Товары.Название, Продажи.КоличествоПроданных
             FROM Товары
             INNER JOIN Продажи ON Товары.ТоварID = Продажи.ТоварID
             INNER JOIN Фирмы ON Продажи.ФирмаID = Фирмы.ФирмаID
             WHERE Фирмы.Название = @companyName";

            using (DbCommand command = dbf.CreateCommand())
            {
                command.Connection = connection;
                command.CommandText = query;

                DbParameter paramСompanyName = command.CreateParameter();
                paramСompanyName.ParameterName = "@companyName";
                paramСompanyName.Value = companyName;
                command.Parameters.Add(paramСompanyName);

                using (DbDataReader reader = command.ExecuteReader())
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
        static void ShowMostRecentSale(DbConnection connection, DbProviderFactory dbf)
        {
            string query = @"
             SELECT TOP 1 Товары.Название, Продажи.КоличествоПроданных, Продажи.ДатаПродажи
             FROM Товары
             INNER JOIN Продажи ON Товары.ТоварID = Продажи.ТоварID
             ORDER BY Продажи.ДатаПродажи DESC";

            using (DbCommand command = dbf.CreateCommand())
            {
                command.Connection = connection;
                command.CommandText = query;

                using (DbDataReader reader = command.ExecuteReader())
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
        static void ShowAverageQuantityByProductType(DbConnection connection, DbProviderFactory dbf)
        {
            string query = @"
            SELECT Тип, dbo.GetAverageQuantityByType(Тип) as СреднееКоличество
            FROM Товары
            GROUP BY Тип";

            using (DbCommand command = dbf.CreateCommand())
            {
                command.Connection = connection;
                command.CommandText = query;

                using (DbDataReader reader = command.ExecuteReader())
                {
                    Console.WriteLine("\nСреднее количество товаров по каждому типу канцтоваров:");
                    while (reader.Read())
                    {
                        Console.WriteLine($"Тип: {reader["Тип"]}, Среднее количество: {reader["СреднееКоличество"]}");
                    }
                }
            }
        }
        //===============================================================================================================================================
        //                              НОВОЕ ЗАДАНИЕ
        //===============================================================================================================================================
        // все менеджеры по продажам в порядке возрастания их продаж
        static void ShowAllSalesManagersUp(DbConnection connection, DbProviderFactory dbf)
        {
            string query = @"
            SELECT Менеджеры.Фамилия, Менеджеры.Имя, COUNT(Продажи.ПродажаID) as ОбщиеПродажи
            FROM Менеджеры
            INNER JOIN Продажи ON Менеджеры.МенеджерID = Продажи.МенеджерID
            GROUP BY Менеджеры.Фамилия, Менеджеры.Имя
            ORDER BY ОбщиеПродажи ASC";

            using (DbCommand command = dbf.CreateCommand())
            {
                command.Connection = connection;
                command.CommandText = query;

                using (DbDataReader reader = command.ExecuteReader())
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
        static void ShowProductsWithMaxQuantityUp(DbConnection connection, DbProviderFactory dbf, string cost)
        {
            string query = @"
            SELECT * FROM Товары WHERE Себестоимость < @cost ORDER BY Количество ASC";

            using (DbCommand command = dbf.CreateCommand())
            {
                command.Connection = connection;
                command.CommandText = query;

                DbParameter paramCost = command.CreateParameter();
                paramCost.ParameterName = "@cost";
                paramCost.Value = cost;
                command.Parameters.Add(paramCost);

                using (DbDataReader reader = command.ExecuteReader())
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
    

