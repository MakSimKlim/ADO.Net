using System;
using System.Data.SqlClient;

namespace ДЗ__72_до_11._03._24_ADO_Net_ПрисоедРежим
{
    internal class Program
    {
        static string connectionString = "Data Source=VANYACOMP;Initial Catalog=Склад;Integrated Security=True;Connect Timeout=30";

        static void Main(string[] args)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Подключение к базе данных «Склад» успешно установлено.");

                    ShowProductInfo(connection, "Масло ДВС");
                    ShowAllProductTypes(connection);
                    ShowAllSuppliers(connection);
                    ShowProductWithMaxQuantity(connection);
                    ShowProductWithMinQuantity(connection);
                    ShowProductWithMinCost(connection);
                    ShowProductWithMaxCost(connection);
                    ShowProductsByCategory(connection, "Автотовары");
                    ShowProductsBySupplier(connection, "ТоргРесурс");
                    ShowOldestProduct(connection);
                    ShowAverageQuantityByProductType(connection);

                    Console.WriteLine("\nНажмите Enter, чтобы продолжить...");
                    Console.ReadLine();

                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Ошибка подключения к базе данных: " + ex.Message);
                    Console.WriteLine("Нажмите Enter, чтобы выйти...");
                    Console.ReadLine();

                }
                finally
                {
                    connection.Close();
                    Console.WriteLine("Подключение к базе данных «Склад» закрыто.");
                    Console.WriteLine("Нажмите Enter, чтобы выйти...");
                    Console.ReadLine();

                }
            }
        }

        // Отображение всей информации о выбранном товаре (см. Main):
        static void ShowProductInfo(SqlConnection connection, string productName)
        {
            string queryString =
        "SELECT Товары.Название_товара, Товары.Тип, Поставки.ДатаПоставки, Поставки.Количество, Поставки.Себестоимость, Поставщики.Наименование_поставщика " +
        "FROM Товары " +
        "JOIN ТоварПоставка ON Товары.ID_товара = ТоварПоставка.ID_товара " +
        "JOIN Поставки ON ТоварПоставка.ID_поставки = Поставки.ID_поставки " +
        "JOIN ПоставкиПоставщик ON Поставки.ID_поставки = ПоставкиПоставщик.ID_поставки " +
        "JOIN Поставщики ON ПоставкиПоставщик.ID_поставщика = Поставщики.ID_поставщика " +
        "WHERE Товары.Название_товара = @productName";

            SqlCommand command = new SqlCommand(queryString, connection);
            command.Parameters.AddWithValue("@productName", productName);

            SqlDataReader reader = command.ExecuteReader();

            Console.WriteLine("\n1). Отображение всей информации о товаре");
            while (reader.Read())
            {
                Console.WriteLine("Наименование: {0}", reader[0]);
                Console.WriteLine("Тип: {0}", reader[1]);
                Console.WriteLine("Дата поставки: {0}", reader[2]);
                Console.WriteLine("Количество: {0}", reader[3]);
                Console.WriteLine("Себестоимость: {0}", reader[4]);
                Console.WriteLine("Поставщик: {0}", reader[5]);
            }
            reader.Close();
        }

        // Отображение всех типов товаров
        static void ShowAllProductTypes(SqlConnection connection)
        {
            SqlCommand command = new SqlCommand("SELECT DISTINCT Тип FROM Товары", connection);
            SqlDataReader reader = command.ExecuteReader();
            Console.WriteLine("\n2). Отображение всех типов товаров:"); 
            while (reader.Read())
            {
                Console.WriteLine($"{reader["Тип"]}");
            }
            reader.Close();
        }

        // Отображение всех поставщиков
        static void ShowAllSuppliers(SqlConnection connection)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM Поставщики", connection);
            SqlDataReader reader = command.ExecuteReader();
            Console.WriteLine("\n3). Отображение всех поставщиков:");
            while (reader.Read())
            {
                Console.WriteLine($"{reader["ОПФ"]} {reader["Наименование_поставщика"]}");
            }
            reader.Close();
        }

        // Показать товар с максимальным общим количеством
        static void ShowProductWithMaxQuantity(SqlConnection connection)
        {
            SqlCommand command = new SqlCommand("SELECT TOP 1 Товары.Название_товара, SUM(Поставки.Количество) as Общее_количество FROM Товары INNER JOIN ТоварПоставка ON Товары.ID_товара = ТоварПоставка.ID_товара INNER JOIN Поставки ON ТоварПоставка.ID_поставки = Поставки.ID_поставки GROUP BY Товары.Название_товара ORDER BY Общее_количество DESC", connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"\n4). Товар с максимальным общим кол-вом: {reader["Название_товара"]}, Общее количество: {reader["Общее_количество"]}");
            }
            reader.Close();
        }


        // Показать товар с минимальным общим количеством
        static void ShowProductWithMinQuantity(SqlConnection connection)
        {
            SqlCommand command = new SqlCommand("SELECT TOP 1 Товары.Название_товара, SUM(Поставки.Количество) as Общее_количество FROM Товары INNER JOIN ТоварПоставка ON Товары.ID_товара = ТоварПоставка.ID_товара INNER JOIN Поставки ON ТоварПоставка.ID_поставки = Поставки.ID_поставки GROUP BY Товары.Название_товара ORDER BY Общее_количество ASC", connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"\n5). Товар с минимальным общим кол-вом: {reader["Название_товара"]}, Общее количество: {reader["Общее_количество"]}");
            }
            reader.Close();
        }

        // Показать товар с минимальной себестоимостью
        static void ShowProductWithMinCost(SqlConnection connection)
        {
            SqlCommand command = new SqlCommand("SELECT TOP 1 Товары.Название_товара, Поставки.Себестоимость FROM Товары INNER JOIN ТоварПоставка ON Товары.ID_товара = ТоварПоставка.ID_товара INNER JOIN Поставки ON ТоварПоставка.ID_поставки = Поставки.ID_поставки ORDER BY Поставки.Себестоимость ASC", connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"\n6). Товар с минимальной себестоимостью: {reader["Название_товара"]}, Себестоимость: {reader["Себестоимость"]}");
            }
            reader.Close();
        }

        // Показать товар с максимальной себестоимостью
        static void ShowProductWithMaxCost(SqlConnection connection)
        {
            SqlCommand command = new SqlCommand("SELECT TOP 1 Товары.Название_товара, Поставки.Себестоимость FROM Товары INNER JOIN ТоварПоставка ON Товары.ID_товара = ТоварПоставка.ID_товара INNER JOIN Поставки ON ТоварПоставка.ID_поставки = Поставки.ID_поставки ORDER BY Поставки.Себестоимость DESC", connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"\n7). Товар с максимальной себестоимостью: {reader["Название_товара"]}, Себестоимость: {reader["Себестоимость"]}");
            }
            reader.Close();
        }

        // Показать товары категории "Автотовары"
        static void ShowProductsByCategory(SqlConnection connection, string category)
        {
            SqlCommand command = new SqlCommand($"SELECT * FROM Товары WHERE Тип = '{category}'", connection);
            SqlDataReader reader = command.ExecuteReader();
            Console.WriteLine("\n8). Товары категории 'Автотовары': ");
            while (reader.Read())
            {
                Console.WriteLine($"{reader["Название_товара"]}");
            }
            reader.Close();
        }

        // Показать товары поставщика "ООО ТоргРесурс"
        static void ShowProductsBySupplier(SqlConnection connection, string supplierName)
        {
            SqlCommand command = new SqlCommand($"SELECT Товары.Название_товара FROM Товары INNER JOIN ТоварПоставка ON Товары.ID_товара = ТоварПоставка.ID_товара INNER JOIN Поставки ON ТоварПоставка.ID_поставки = Поставки.ID_поставки INNER JOIN ПоставкиПоставщик ON Поставки.ID_поставки = ПоставкиПоставщик.ID_поставки INNER JOIN Поставщики ON ПоставкиПоставщик.ID_поставщика = Поставщики.ID_поставщика WHERE Поставщики.Наименование_поставщика = '{supplierName}'", connection);
            SqlDataReader reader = command.ExecuteReader();
            Console.WriteLine("\n9). Товары поставщика 'ООО ТоргРесурс': ");
            while (reader.Read())
            {
                Console.WriteLine($"{reader["Название_товара"]}");
            }
            reader.Close();
        }


        // Показать самый старый товар на складе
        static void ShowOldestProduct(SqlConnection connection)
        {
            SqlCommand command = new SqlCommand("SELECT TOP 1 Товары.Название_товара, MIN(Поставки.ДатаПоставки) as Старейшая_дата FROM Товары INNER JOIN ТоварПоставка ON Товары.ID_товара = ТоварПоставка.ID_товара INNER JOIN Поставки ON ТоварПоставка.ID_поставки = Поставки.ID_поставки GROUP BY Товары.Название_товара ORDER BY Старейшая_дата ASC", connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"\n10). Самый старый товар на складе: {reader["Название_товара"]}, дата поставки: {reader["Старейшая_дата"]}");
            }
            reader.Close();
        }


        // Показать среднее количество товаров по каждому типу товара
        static void ShowAverageQuantityByProductType(SqlConnection connection)
        {
            SqlCommand command = new SqlCommand("SELECT Товары.Тип, SUM(Поставки.Количество) / COUNT(DISTINCT Товары.ID_товара) as Среднее_количество FROM Товары INNER JOIN ТоварПоставка ON Товары.ID_товара = ТоварПоставка.ID_товара INNER JOIN Поставки ON ТоварПоставка.ID_поставки = Поставки.ID_поставки GROUP BY Товары.Тип", connection);
            SqlDataReader reader = command.ExecuteReader();
            Console.WriteLine("\n11). Среднее количество товаров по каждому типу товара:");
            while (reader.Read())
            {
                Console.WriteLine($"{reader["Тип"]}, Среднее количество: {reader["Среднее_количество"]}");
            }
            reader.Close();
        }

    }


}

