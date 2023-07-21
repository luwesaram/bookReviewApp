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
            ObservableCollection<Book> books = new ObservableCollection<Book>();

            try
            {
                if (!Conn.OpenConnection())
                {
                    MessageBox.Show("Unable to connect to the database.", "Error");
                    return books;
                }

                string sql = "SELECT b.ISBNNumber, b.title, b.description, b.cover_image, b.publication_date, b.page_count, " +
                             "a.pen_name, g.name " +
                             "FROM book b " +
                             "INNER JOIN author a ON b.author_id = a.id " +
                             "LEFT JOIN genre g ON b.genre_id = g.id " +
                             "ORDER BY b.publication_date DESC LIMIT @count";

                using (MySqlCommand command = new MySqlCommand(sql, Conn.GetConnection()))
                {
                    command.Parameters.AddWithValue("@count", count);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (await reader.ReadAsync())
                        {

                            Author author = new Author {
                                PenName = reader.GetString("pen_name")
                            };
                            
                            Genre genre = new Genre{
                                Name = reader.IsDBNull("name") ? null : reader.GetString("name")
                            };

                            Book book = new Book();
                            book.ISBNNumber = reader.GetString("ISBNNumber");
                            book.Title = reader.GetString("title");
                            book.Description = reader.GetString("description");
                            book.PublicationDate = reader.GetDateTime("publication_date");
                            book.PageCount = reader.GetInt32("page_count");
                            book.Author = author;
                            book.Genre = genre;

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
            ObservableCollection<Review> reviews = new ObservableCollection<Review>();

            try
            {
                if(!Conn.OpenConnection())
                {
                    MessageBox.Show("Unable to connect to the database.", "Error");
                    return reviews;
                }

                string sql ="SELECT u.Username, b.title, r.description, r.rating, r.review_date " +
                            "FROM review r " + 
                            "JOIN user u ON r.user_id = u.id " +
                            "JOIN book b ON r.book_id = b.ISBNNumber " +
                            "LIMIT @count";

                using (MySqlCommand command = new MySqlCommand(sql, Conn.GetConnection())) 
                {
                    command.Parameters.AddWithValue("@count", count);
                    
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while(await reader.ReadAsync())
                        {                       
                            Book book = new Book {
                                Title = reader.GetString("title")
                            };

                            User user = new User {
                                Username = reader.GetString("username")
                            };

                            Review review = new Review {
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