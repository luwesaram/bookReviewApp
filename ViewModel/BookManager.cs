using bookReviewConsoleApplication.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.ObjectModel;
using System.IO;
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

        public async Task<ObservableCollection<Book>> GetMostRecentBooks()
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
                             "a.pen_name, u.username, " +
                             "g.name " +
                             "FROM book b " +
                             "JOIN author a ON b.author_id = a.id " +
                             "JOIN genre g ON g.id = b.genre_id " +
                             "JOIN user u ON u.id = a.user_id " +
                             "ORDER BY STR_TO_DATE(b.publication_date, '%m/%d/%Y') DESC";

                using (MySqlCommand command = new(sql, Conn.GetConnection()))
                {
                    // command.Parameters.AddWithValue("@count", count);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (await reader.ReadAsync())
                        {


                            User user = new()
                            {
                                Username = reader.GetString("username")
                            };

                            Author author = new Author(user.Username);

                            Genre genre = new()
                            {
                                Name = reader.GetString("name")
                            };

                            int penNameOrdinal = reader.GetOrdinal("pen_name");
                            if (!reader.IsDBNull(penNameOrdinal))
                            {
                                author.PenName = reader.GetString(penNameOrdinal);
                            }
                            else
                            {
                                int usernameOrdinal = reader.GetOrdinal("username");
                                if (!reader.IsDBNull(usernameOrdinal))
                                {
                                    author.PenName = reader.GetString(usernameOrdinal);
                                }
                                else
                                {
                                    author.PenName = reader.GetString("username");
                                }
                            }

                            Book book = new()
                            {
                                ISBNNumber = reader.GetString("id"),
                                Title = reader.GetString("title"),
                                Description = reader.GetString("description"),
                                PublicationDate = reader.GetString("publication_date"),
                                PageCount = reader.GetInt32("page_count"),
                                Author = author,
                                Genre = genre
                            };

                            object CoverImageObj = reader["cover_image"];
                            if (CoverImageObj != null && CoverImageObj != DBNull.Value)
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

        public void CreateBook(string ISBNNumber, string title, string description, Genre genre, DateTime publicationDate, int pageCount, string coverImagePath) {
            try 
            {
                if(!Conn.OpenConnection()) {
                    return;
                } 

                Book newBook = new()
                {   
                    ISBNNumber = ISBNNumber,
                    Title = title,
                    Description = description,
                    Genre = genre,
                    PublicationDate = publicationDate,
                    PageCount = pageCount,                    
                }

                ImageSource imageSource = new ImageSourceConverter().ConvertFromString(coverImagePath) as ImageSource; 
                newBook.CoverImage = imageSource;

                string sql = "";
                using MySqlCommand command = new(sql, Conn.GetConnection());

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                Conn.CloseConnection();
            }
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