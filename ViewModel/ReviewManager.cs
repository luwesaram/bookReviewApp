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
    public class ReviewManager
    {
        private Connection Conn;

        // when BookManager loads, it constructs the connection property in order for the Conn to be globally usable by other 
        // existing methods
        public ReviewManager(Connection conn)
        {
            this.Conn = conn;
        }

        public async Task<ObservableCollection<Review>> GetMostRecentReviews(int count)
        {
            ObservableCollection<Review> reviews = new();

            try
            {
                if (!Conn.OpenConnection())
                {
                    MessageBox.Show("Unable to connect to the database.", "Error");
                    return reviews;
                }

                string sql = "SELECT u.username, b.title, r.description, r.rating, r.id, r.review_date " +
                            "FROM review r " +
                            "JOIN user u ON r.user_id = u.id " +
                            "JOIN book b ON r.book_id = b.id " +
                            "LIMIT @count";

                    using (MySqlCommand command = new(sql, Conn.GetConnection()))
                {
                    command.Parameters.AddWithValue("@count", count);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (await reader.ReadAsync())
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
                Conn.CloseConnection();
            }

            return reviews;
        }

        public void AddReview(string description, Book book)
        {
            User user = CurrentUserManager.Instance.CurrentUser;
            MessageBox.Show("Im here", "Nice", MessageBoxButton.OK);
            try
            {
                if(!Conn.OpenConnection())
                {
                    MessageBox.Show("Unable to connect to the database.", "Error");
                    return;
                }

                string sql = "INSERT INTO review (user_id, book_id, description, review_date, rating) VALUES (@UserId, @BookId, @Description, @CreatedAt, @Rating)";
                MySqlCommand command = new MySqlCommand(sql, Conn.GetConnection());
                command.Parameters.AddWithValue("@UserId", user.Id);
                command.Parameters.AddWithValue("@Rating", 5);
                command.Parameters.AddWithValue("@BookId", book.ISBNNumber);
                command.Parameters.AddWithValue("@Description", description);
                command.Parameters.AddWithValue("@CreatedAt", DateTime.Now);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Review added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Failed to add review", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch(MySqlException ex)  
            { 
                MessageBox.Show("Error: " + ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                Conn.CloseConnection();
            }
        }
    }
}