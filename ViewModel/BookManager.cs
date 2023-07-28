using bookReviewConsoleApplication.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
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
                                PageCount = reader.GetInt32("page_count"),
                                Author = author,
                                Genre = genre
                            };

                            string publicationDateString = reader.GetString("publication_date");
                            if (DateTime.TryParseExact(publicationDateString, "MMMM d, yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime publicationDate))
                            {
                                book.PublicationDate = publicationDate;
                            }
                            else
                            {
                                // Handle parsing error, show a default date, or set it to DateTime.MinValue, depending on your requirements.
                                // For example:
                                book.PublicationDate = DateTime.MinValue;
                            }

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

        public bool CreateBook(string ISBNNumber, string title, string description, Genre genre, DateTime publicationDate, int pageCount, string coverImagePath) {
            Author author = CurrentUserManager.Instance.Author;
            int affected = 0;

            try 
            {
                if(!Conn.OpenConnection()) {
                    return false;
                }

                Book newBook = new()
                {
                    ISBNNumber = ISBNNumber,
                    Title = title,
                    Description = description,
                    Genre = genre,
                    Author = author,
                    PublicationDate = publicationDate,
                    PageCount = pageCount,
                };

                ImageSource imageSource = new ImageSourceConverter().ConvertFromString(coverImagePath) as ImageSource; 
                newBook.CoverImage = imageSource;

                string sql = "INSERT INTO book (id, title, description, genre_id, author_id, publication_date, page_count, cover_image) " +
                     "VALUES (@ISBNNumber, @Title, @Description, @GenreId, @AuthorId, @PublicationDate, @PageCount, @CoverImage)";

                using MySqlCommand command = new(sql, Conn.GetConnection());
                command.Parameters.AddWithValue("@ISBNNumber", newBook.ISBNNumber);
                command.Parameters.AddWithValue("@Title", newBook.Title);
                command.Parameters.AddWithValue("@Description", newBook.Description);
                command.Parameters.AddWithValue("@GenreId", newBook.Genre.Id);
                command.Parameters.AddWithValue("@AuthorId", newBook.Author.Id);
                command.Parameters.AddWithValue("@PublicationDate", newBook.PublicationDate);
                command.Parameters.AddWithValue("@PageCount", newBook.PageCount);
                command.Parameters.AddWithValue("@CoverImage", ConvertImageToBytes(newBook.CoverImage)); // Convert the image to a byte array
                affected = command.ExecuteNonQuery();

                MessageBox.Show("Success", "Status", MessageBoxButton.OK);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                Conn.CloseConnection();
            }

            return affected > 0;
        }
        private byte[] ConvertImageToBytes(ImageSource imageSource)
        {
            if (imageSource == null)
                return null;

            using (MemoryStream stream = new MemoryStream())
            {
                var encoder = new JpegBitmapEncoder(); // Use JpegBitmapEncoder for JPEG images, or other encoders based on your image format
                encoder.Frames.Add(BitmapFrame.Create(imageSource as BitmapSource));
                encoder.Save(stream);
                return stream.ToArray();
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