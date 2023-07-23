using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;
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

                string sql = "SELECT b.cover_image, b.title, " +
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
                                Author = author,
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

        public async Task<ObservableCollection<Review>> GetMostRecentReviews(int count)
        {
            ObservableCollection<Review> reviews = new();

            try
            {
                if(!Conn.OpenConnection())
                {
                    MessageBox.Show("Unable to connect to the database.", "Error");
                    return reviews;
                }

                string sql ="SELECT u.username, b.title, r.description, r.rating, r.review_date " +
                            "FROM review r " + 
                            "JOIN user u ON r.user_id = u.id " +
                            "JOIN book b ON r.book_id = b.id " +
                            "LIMIT @count";

                using (MySqlCommand command = new(sql, Conn.GetConnection())) 
                {
                    command.Parameters.AddWithValue("@count", count);
                    
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while(await reader.ReadAsync())
                        {                       
                            Book book = new()
                            {
                                Title = reader.GetString("title")
                            };

                            User user = new()
                            {
                                Username = reader.GetString("username")
                            };

                            Review review = new()
                            {
                                Id = reader.GetInt32("id"),
                                ReviewDate = reader.GetDateTime("review_date"),
                                Description = reader.GetString("description"),
                                Rating = reader.GetInt32("rating"),
                                Book = book,
                                User = user
                            };

                            reviews.Add(review);
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

            }

            return reviews;
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