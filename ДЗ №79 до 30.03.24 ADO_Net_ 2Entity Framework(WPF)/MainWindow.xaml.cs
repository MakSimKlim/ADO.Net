using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StationeryCompany_WPF_
{
    public partial class MainWindow : Window
    {
        readonly ФирмаКанцтоваровEntities db;

        public MainWindow()
        {
            InitializeComponent();
            db = new ФирмаКанцтоваровEntities();
            LoadDataGoods();
            LoadDataFirms();
            LoadDataManagersFamily();
            LoadDataSales();
        }

        /*
         //чтобы в выпадающем меню отражались правильные данные, нужно в соответствующем классе переопределить метод ToString()
        public override string ToString()
        {
            return Название; // возвращает название товара
        }
        */


        private void LoadDataGoods()
        {
            var goods = db.Товары.ToList(); // Отложенная загрузка. Загрузка данных из таблицы “Товары” происходит при обращении к данным, то есть когда вы обращаетесь к GoodsComboBox.ItemsSource
            GoodsComboBox.ItemsSource = goods;
           
        }

        private void LoadDataFirms()
        {
            var firms = db.Фирмы.Include(f => f.Продажи).ToList(); // Безотложная загрузка. Здесь мы сразу загружаем связанные данные (Продажи) вместе с основными данными (Фирмы) с помощью метода Include().
            FirmsComboBox.ItemsSource = firms;
        }
        private void LoadDataManagersFamily()
        {
            var managersFamily = db.Менеджеры.ToList();
            ManagersFamilyComboBox.ItemsSource = managersFamily;
        }
        private void LoadDataSales()
        {
            var sales = db.Продажи.ToList();
            foreach (var sale in sales)
            {
                db.Entry(sale).Reference(s => s.Товары).Load(); // Явная загрузка. Здесь мы явно загружаем связанные данные (Товары) с помощью метода Load().
            }
            SalesComboBox.ItemsSource = sales;
        }


        // ===================================  ТОВАРЫ  =============================================================================
        private void GoodsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedGood = (Товары)GoodsComboBox.SelectedItem;
            if (selectedGood != null)
            {
                NameTextBox.Text = selectedGood.Название;
                TypeTextBox.Text = selectedGood.Тип;
                QuantityTextBox.Text = selectedGood.Количество.ToString();
                CostPriceTextBox.Text = selectedGood.Себестоимость.ToString();

                // Очистка выбранного элемента в ComboBox
                GoodsComboBox.SelectedItem = null;

            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedGood = (Товары)GoodsComboBox.SelectedItem;
            if (selectedGood != null)
            {
                selectedGood.Название = NameTextBox.Text;
                selectedGood.Тип = TypeTextBox.Text;
                selectedGood.Количество = int.Parse(QuantityTextBox.Text);
                selectedGood.Себестоимость = decimal.Parse(CostPriceTextBox.Text);
                db.SaveChanges();
            }
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Товары newGood = new Товары
            {
                Название = NameTextBox.Text,
                Тип = TypeTextBox.Text,
                Количество = int.Parse(QuantityTextBox.Text),
                Себестоимость = decimal.Parse(CostPriceTextBox.Text)
            };

            db.Товары.Add(newGood);
            db.SaveChanges();
        }
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            // Очистите текстовые поля
            NameTextBox.Clear();
            TypeTextBox.Clear();
            QuantityTextBox.Clear();
            CostPriceTextBox.Clear();
        }

        // ===================================  ФИРМЫ  =============================================================================

        private void FirmsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedFirm = (Фирмы)FirmsComboBox.SelectedItem;
            if (selectedFirm != null)
            {
                NameTextBox2.Text = selectedFirm.Название;

                // Очистите выбранный элемент в ComboBox
                FirmsComboBox.SelectedItem = null;
            }
        }

        private void SaveButton_Click2(object sender, RoutedEventArgs e)
        {
            var selectedFirm = (Фирмы)FirmsComboBox.SelectedItem;
            if (selectedFirm != null)
            {
                selectedFirm.Название = NameTextBox2.Text;

                db.SaveChanges();
            }
        }

        private void AddButton_Click2(object sender, RoutedEventArgs e)
        {
            Фирмы newFirm = new Фирмы
            {
                Название = NameTextBox2.Text,
            };

            db.Фирмы.Add(newFirm);
            db.SaveChanges();
        }

        private void ClearButton_Click2(object sender, RoutedEventArgs e)
        {
            NameTextBox2.Clear();
        }

        // ===================================  МЕНЕДЖЕРЫ  =============================================================================

        private void FamilyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedManager = (Менеджеры)ManagersFamilyComboBox.SelectedItem;
            if (selectedManager != null)
            {
                NameTextBox3.Text = selectedManager.Фамилия;
                NameTextBox4.Text = selectedManager.Имя;


                // Очистите выбранный элемент в ComboBox
                ManagersFamilyComboBox.SelectedItem = null;
            }
        }

        private void SaveButton_Click3(object sender, RoutedEventArgs e)
        {
            var selectedManager = (Менеджеры)ManagersFamilyComboBox.SelectedItem;
            if (selectedManager != null)
            {
                selectedManager.Фамилия = NameTextBox3.Text;
                selectedManager.Имя = NameTextBox4.Text;

                db.SaveChanges();
            }
        }

        private void AddButton_Click3(object sender, RoutedEventArgs e)
        {
            Менеджеры newManager = new Менеджеры
            {
                Фамилия = NameTextBox3.Text,
                Имя = NameTextBox4.Text,
            };

            db.Менеджеры.Add(newManager);
            db.SaveChanges();
        }

        private void ClearButton_Click3(object sender, RoutedEventArgs e)
        {
            NameTextBox3.Clear();
            NameTextBox4.Clear();
        }

        // ===================================  ПРОДАЖИ  =============================================================================

        private void SalesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedSale = (Продажи)SalesComboBox.SelectedItem;
            if (selectedSale != null)
            {
                QuantityTextBox4.Text = selectedSale.КоличествоПроданных.ToString();
                CostTextBox.Text = selectedSale.ЦенаЗаЕдиницу.ToString();
                // Проверяем, есть ли значение у ДатаПродажи перед форматированием
                if (selectedSale.ДатаПродажи.HasValue)
                {
                    DateTextBox.Text = selectedSale.ДатаПродажи.Value.ToString("dd.MM.yyyy");
                }
                else
                {
                    // Если значение ДатаПродажи отсутствует (null), можно задать пустую строку, или текст по умолчанию
                    DateTextBox.Text = ""; // или "Дата не указана", например
                }

                // Очистите выбранный элемент в ComboBox
                SalesComboBox.SelectedItem = null;
            }
        }

        private void SaveButton_Click4(object sender, RoutedEventArgs e)
        {
            var selectedSale = (Продажи)SalesComboBox.SelectedItem;
            if (selectedSale != null)
            {
                selectedSale.КоличествоПроданных = int.Parse(QuantityTextBox4.Text);
                selectedSale.ЦенаЗаЕдиницу = decimal.Parse(CostTextBox.Text);
                selectedSale.ДатаПродажи = DateTime.Parse(DateTextBox.Text);
                // Здесь вы можете обновить другие поля, если они есть

                db.SaveChanges();
            }
        }

        private void AddButton_Click4(object sender, RoutedEventArgs e)
        {
            Продажи newSale = new Продажи
            {
                КоличествоПроданных = int.Parse(QuantityTextBox4.Text),
                ЦенаЗаЕдиницу = decimal.Parse(CostTextBox.Text),
                ДатаПродажи = DateTime.Parse(DateTextBox.Text),
                // Здесь вы можете установить другие поля, если они есть
            };

            db.Продажи.Add(newSale);
            db.SaveChanges();
        }

        private void ClearButton_Click4(object sender, RoutedEventArgs e)
        {
            QuantityTextBox4.Clear();
            CostTextBox.Clear();
            DateTextBox.Clear();
        }

    }
}