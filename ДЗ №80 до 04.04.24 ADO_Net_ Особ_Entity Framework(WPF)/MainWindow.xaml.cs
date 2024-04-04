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

namespace BooksLibrary
{
    public partial class MainWindow : Window
    {
        private List<Book> books;
        private int currentBookIndex;

        public MainWindow()
        {
            InitializeComponent();
            LoadBooksFromDatabase();
            DisplayCurrentBook();
        }

        private void LoadBooksFromDatabase()
        {
            // Загрузите все книги из вашей базы данных в список "books"
            // Это будет зависеть от того, как вы настроили свою базу данных и Entity Framework
            // Например: books = dbContext.Books.Include(b => b.Author).Include(b => b.Status).ToList();
        }

        private void DisplayCurrentBook()
        {
            // Отобразите информацию о текущей книге в текстовых полях
            //var book = books[currentBookIndex];
            //AuthorTextBox.Text = book.Author.Name;
            //TitleTextBox.Text = book.Title;
            //PageReadTextBox.Text = book.PagesRead.ToString();
            //PageTextBox.Text = book.TotalPages.ToString();
            //StatusTextBox.Text = book.Status.Title;
            //RatingTextBox.Text = book.Rating.ToString();

            // Загрузите изображение обложки из базы данных
            // Это будет зависеть от того, как вы храните изображения в вашей базы данных
            // Например: CoverImage.Source = new BitmapImage(new Uri(book.Cover));
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentBookIndex > 0)
            {
                currentBookIndex--;
                DisplayCurrentBook();
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentBookIndex < books.Count - 1)
            {
                currentBookIndex++;
                DisplayCurrentBook();
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Создайте новую книгу с информацией из текстовых полей и добавьте ее в базу данных
            // Это будет зависеть от того, как вы настроили свою базу данных и Entity Framework
            // Например:
            // var newBook = new Book { ... };
            // dbContext.Books.Add(newBook);
            // dbContext.SaveChanges();
        }

        private void CorrectButton_Click(object sender, RoutedEventArgs e)
        {
            // Обновите информацию о текущей книге в базе данных с информацией из текстовых полей
            // Это будет зависеть от того, как вы настроили свою базу данных и Entity Framework
            // Например:
            // var book = books[currentBookIndex];
            // book.Title = TitleTextBox.Text;
            // ...
            // dbContext.SaveChanges();
        }

        //private void DownloadCoverButton_Click(object sender, RoutedEventArgs e)
        //{
        //    // Сохраните изображение обложки текущей книги
        //    // Это будет зависеть от того, как вы храните изображения в вашей базы данных
        //    // Например:
        //    // SaveFileDialog saveFileDialog = new SaveFileDialog();
        //    // saveFileDialog.Filter = "Images (*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG";
        //    // if (saveFileDialog.ShowDialog() == true)
        //    // {
        //    //     File.Copy(book.Cover, saveFileDialog.FileName);
        //    // }
        //}

        private void WallImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Создайте новое окно
            Window imageWindow = new Window
            {
                Width = CoverImage.Width * 5,
                Height = CoverImage.Height * 5,
                Content = new Image { Source = CoverImage.Source }
            };

            // Отобразите окно
            imageWindow.Show();
        }

        private void DownloadCoverButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Images (*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG";
            if (saveFileDialog.ShowDialog() == true)
            {
                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create((BitmapSource)CoverImage.Source));
                using (FileStream stream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                {
                    encoder.Save(stream);
                }
            }
        }


    }
}
