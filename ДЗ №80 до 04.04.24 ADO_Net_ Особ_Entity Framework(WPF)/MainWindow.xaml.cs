using BooksLibrary;
using System;
using System.Collections.Generic;
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
using Microsoft.Win32; // Добавлено для извлечения картинок из базы данных
using System.IO; // Добавлено для извлечения картинок из базы данных
using System.Data.SqlClient;
using System.Data.Entity.Infrastructure;


namespace BooksLibrary
{
    public partial class MainWindow : Window
    {
        readonly BooksLibraryEntities _context;
        private List<Book> _books;
        private int _currentBookIndex;

        public MainWindow()
        {
            InitializeComponent();
            LoadStatuses(); //  получить список статусов из базы данных
            _context = new BooksLibraryEntities();
            _books = _context.Books.Include("Author").Include("Status").ToList();
            _currentBookIndex = 0;
            DisplayCurrentBook();
        }

        // загрузка текущей книги
        private void DisplayCurrentBook()
        {
            var book = _books[_currentBookIndex];
            if (book != null && book.Author != null)
            {
                AuthorTextBox.Text = book.Author.AuthorName + " " + book.Author.AuthorSurname;
            }
            else
            {
                // Обработка ситуации, когда book или book.Author равны null
                AuthorTextBox.Text = string.Empty;
            }            
            TitleTextBox.Text = book.BookTitle;
            PageReadTextBox.Text = book.NumberOfPagesRead.ToString();
            PageTextBox.Text = book.NumberOfPages.ToString();
            StatusComboBox.Text = book.Status.TitleStatus;
            RatingTextBox.Text = book.Rating.ToString();
            CoverImage.Source = LoadImage(book.Cover); // Загрузка изображения из массива байтов
        }
        // загрузка изображения
        private BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }
        // переход к предыдущей книге
        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentBookIndex > 0)
            {
                _currentBookIndex--;
                DisplayCurrentBook();
            }
        }
        // переход к следующей книге
        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentBookIndex < _books.Count - 1)
            {
                _currentBookIndex++;
                DisplayCurrentBook();
            }
        }

        // загрузка статусов в combobox
        private void LoadStatuses()
        {
            // выполнение запроса к базе данных
            // и получения списка статусов
            List<string> statuses = GetStatusesFromDatabase();

            // Заполняем StatusComboBox полученными статусами
            foreach (var status in statuses)
            {
                StatusComboBox.Items.Add(status);
            }
        }
        private List<string> GetStatusesFromDatabase()
        {
            List<string> statuses = new List<string>();

            // Создаем подключение к базе данных
            // Подключение к базе данных закрывается автоматически при выходе из блока using
            using (SqlConnection connection = new SqlConnection("Data Source=(local);Initial Catalog=BooksLibrary;Integrated Security=True"))
            {
                // Открываем подключение
                connection.Open();

                // Создаем команду SQL
                using (SqlCommand command = new SqlCommand("SELECT TitleStatus FROM Statuses", connection))
                {
                    // Выполняем команду и получаем результаты
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Читаем результаты
                        while (reader.Read())
                        {
                            statuses.Add(reader.GetString(0));
                        }
                    }
                }
            }

            return statuses;
        }
        // при выборе статуса просто отображается список статусов
        private void StatusComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Ничего не делаем при выборе статуса
        }

        // кнопка сохранить в базу данных с проверкой на триггеры
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var newBook = new Book
                {
                    BookTitle = TitleTextBox.Text,
                    Author = _context.Authors.FirstOrDefault(a => a.AuthorName == AuthorTextBox.Text), // Предполагается, что имя автора уникально
                    NumberOfPagesRead = int.Parse(PageReadTextBox.Text),
                    NumberOfPages = int.Parse(PageTextBox.Text),
                    Status = _context.Statuses.FirstOrDefault(s => s.TitleStatus == StatusComboBox.Text), // Предполагается, что статус уникален
                    Rating = int.Parse(RatingTextBox.Text),
                    //Cover = ImageToByteArray(CoverImage.Source as BitmapImage) // Преобразование изображения в массив байтов
                };
                _context.Books.Add(newBook);
                _context.SaveChanges();
                _books = _context.Books.Include("Author").Include("Status").ToList();// Обновление списка книг

            }
            catch (DbUpdateException ex)
            {
                // Проверяем внутреннее исключение
                var innerException = ex.InnerException;
                while (innerException != null)
                {
                    // Если внутреннее исключение является SqlException, обрабатываем его
                    if (innerException is SqlException sqlEx)
                    {
                        // Проверяем номер ошибки
                        if (sqlEx.Number == 50000) // номер ошибки для пользовательских ошибок RAISERROR
                        {
                            // Отображаем сообщение об ошибке
                            MessageBox.Show(sqlEx.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            // Обрабатываем все остальные ошибки SQL
                            MessageBox.Show("Произошла ошибка при работе с базой данных: " + sqlEx.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }

                    // Переходим к следующему внутреннему исключению
                    innerException = innerException.InnerException;
                }
            }
        }

        // кнопка откорректировать книгу в базе данных с проверкой на триггеры
        private void CorrectButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var book = _books[_currentBookIndex];
                book.BookTitle = TitleTextBox.Text;
                book.Author = _context.Authors.FirstOrDefault(a => a.AuthorName == AuthorTextBox.Text); // Предполагается, что имя автора уникально
                book.NumberOfPagesRead = int.Parse(PageReadTextBox.Text);
                book.NumberOfPages = int.Parse(PageTextBox.Text);
                book.Status = _context.Statuses.FirstOrDefault(s => s.TitleStatus == StatusComboBox.Text); // Предполагается, что статус уникален
                book.Rating = int.Parse(RatingTextBox.Text);
                //book.Cover = ImageToByteArray(CoverImage.Source as BitmapImage); // Преобразование изображения в массив байтов
                _context.SaveChanges();
                _books = _context.Books.Include("Author").Include("Status").ToList();// Обновление списка книг
            }
            catch (DbUpdateException ex)
            {
                // Проверяем внутреннее исключение
                var innerException = ex.InnerException;
                while (innerException != null)
                {
                    // Если внутреннее исключение является SqlException, обрабатываем его
                    if (innerException is SqlException sqlEx)
                    {
                        // Проверяем номер ошибки
                        if (sqlEx.Number == 50000) // номер ошибки для пользовательских ошибок RAISERROR
                        {
                            // Отображаем сообщение об ошибке
                            MessageBox.Show(sqlEx.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            // Обрабатываем все остальные ошибки SQL
                            MessageBox.Show("Произошла ошибка при работе с базой данных: " + sqlEx.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }

                    // Переходим к следующему внутреннему исключению
                    innerException = innerException.InnerException;
                }
            }
        }

        // кнопка сохранить обложку в нужном формате
        private void DownloadCoverButton_Click(object sender, RoutedEventArgs e)
        {
            // Получаем выбранную книгу
            var selectedBook = _books[_currentBookIndex];
            if (selectedBook != null)
            {
                // Столбец Cover в таблице Books хранит обложки книг в формате byte[]
                byte[] imageBytes = selectedBook.Cover;
                if (imageBytes != null)
                {
                    // Создаем диалог для сохранения файла
                    var saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "JPEG Image|*.jpg|Bitmap Image|*.bmp|PNG Image|*.png"; // определение фильтров для типа файлов
                    if (saveFileDialog.ShowDialog() == true)
                    {
                        // Сохраняем изображение в выбранный файл
                        File.WriteAllBytes(saveFileDialog.FileName, imageBytes);
                    }
                }
            }
        }


        // Обработчик события клика мышью на изображении (увеличивает изображение в отдельном окне)
        private void CoverImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Создаем новое окно
            Window imageWindow = new Window
            {
                Title = "Увеличенное изображение",
                Width = 800,
                Height = 800
            };

            // Создаем новый Image контрол и устанавливаем его как контент окна
            Image largeImage = new Image
            {
                Source = CoverImage.Source,
                Stretch = Stretch.Uniform
            };
            imageWindow.Content = largeImage;

            // Открываем окно
            imageWindow.Show();
        }

        // кнопка очистки всех полей
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            AuthorTextBox.Text = string.Empty;
            TitleTextBox.Text = string.Empty;
            PageReadTextBox.Text = string.Empty;
            PageTextBox.Text = string.Empty;
            StatusComboBox.Text = string.Empty;
            RatingTextBox.Text = string.Empty;
            CoverImage.Source = null; // Очистка изображения обложки
        }


        //private byte[] ImageToByteArray(BitmapImage bitmapImage)
        //{
        //    byte[] data;
        //    var encoder = new PngBitmapEncoder();
        //    encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
        //    using (var ms = new MemoryStream())
        //    {
        //        encoder.Save(ms);
        //        data = ms.ToArray();
        //    }
        //    return data;
        //}
    }
}