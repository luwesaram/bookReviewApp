using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using bookReviewConsoleApplication.Model;
using MySql.Data.MySqlClient;

namespace bookReviewConsoleApplication.ViewModel 
{
    public class BookManager
    {
        private Connection Conn;

        public BookManager()
        {
            Conn = new Connection();
        }

        public List<Book> GetMostRecentBooks(int count)
        {
            List<Book> books = new List<Book>();

            try
            {
                if (!Conn.OpenConnection())
                {
                    MessageBox.Show("Unable to connect to the database.", "Error");
                    return books;
                }

                string sql = "SELECT b.ISBNNumber, b.title, b.description, b.cover_image, b.publication_date, b.page_count, " +
                             "a.pen_name, g.genre_name " +
                             "FROM books b " +
                             "INNER JOIN authors a ON b.author_id = a.author_id " +
                             "LEFT JOIN genres g ON b.genre_id = g.genre_id " +
                             "ORDER BY b.publication_date DESC LIMIT @count";

                using (MySqlCommand command = new MySqlCommand(sql, Conn.GetConnection()))
                {
                    command.Parameters.AddWithValue("@count", count);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Book book = new Book();
                            book.ISBNNumber = reader.GetString("ISBNNumber");
                            book.Title = reader.GetString("title");
                            book.Description = reader.GetString("description");
                            book.CoverImage = GetImageFromBytes(reader["cover_image"]);
                            book.PublicationDate = reader.GetDateTime("publication_date");
                            book.PageCount = reader.GetInt32("page_count");

                            Author author = new Author();
                            author.PenName = reader.GetString("pen_name");
                            book.Author = author;

                            Genre genre = new Genre();
                            genre.Name = reader.IsDBNull("genre_name") ? null : reader.GetString("genre_name");
                            book.Genre = genre;

                            byte[] imageData = (byte[])reader["cover_image"];

                            // Convert the byte array to a BitmapImage
                            book.CoverImage = GetImageFromBytes(imageData);

                            books.Add(book);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                Conn.CloseConnection();
            }

            return books;
        }

        private ImageSource GetImageFromBytes(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0)
                return null;

            using (MemoryStream stream = new MemoryStream(imageData))
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = stream;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.EndInit();
                image.Freeze(); // Freeze the image to make it usable in other threads (if needed)
                return image;
            }
        }

    }
}