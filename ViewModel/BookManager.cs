using bookReviewConsoleApplication.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace bookReviewConsoleApplication.ViewModel
{
    public class BookManager
    {
        private Connection Conn;

        // when BookManager loads, it constructs the connection property in order for the Conn to be globally usable by other 
        // existing methods
        public BookManager(Connection conn)
        {
            this.Conn = conn;
        }

        public async Task<ObservableCollection<Book>> GetMostRecentBooks(int count)
        {
            ObservableCollection<Book> books = new();

            try
            {
                if (!Conn.OpenConnection())
                {
                    MessageBox.Show("Unable to connect to the database.", "Error");
                    return books;
                }

                string sql = "SELECT b.*, " +
                             "a.pen_name, u.username " +
                             "FROM book b " +
                             "JOIN author a ON b.author_id = a.id " +
                             "JOIN user u ON u.id = a.user_id " + 
                             "ORDER BY b.publication_date DESC LIMIT @count";

                using (MySqlCommand command = new(sql, Conn.GetConnection()))
                {
                    command.Parameters.AddWithValue("@count", count);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (await reader.ReadAsync())
                        {


                            User user = new()
                            {
                                Username = reader.GetString("username")
                            };

                            Author author = new(user.Username)
                            {
                                // PenName = reader.GetString("pen_name")
                            };

                            Book book = new()
                            {
                                Title = reader.GetString("title"),
                                Description = reader.GetString("description"),
                                PublicationDate = reader.GetDateTime("publication_date"),
                                Author = author
                            };

                            object CoverImageObj = reader["cover_image"];
                            if(CoverImageObj != null && CoverImageObj != DBNull.Value)
                            {
                                byte[] imageData = (byte[])CoverImageObj;
                                book.CoverImage = GetImageFromBytes(imageData);                                
                            }

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