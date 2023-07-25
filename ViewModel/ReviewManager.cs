using bookReviewConsoleApplication.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

namespace bookReviewConsoleApplication.ViewModel
{
    public class ReviewManager
    {
        private readonly Connection Conn;

        // when BookManager loads, it constructs the connection property in order for the Conn to be globally usable by other 
        // existing methods
        public ReviewManager(Connection conn)
        {
            this.Conn = conn;
        }

        public async Task<ObservableCollection<Review>> GetAllReviews(Book book)
        {
            ObservableCollection<Review> reviews = new();

            try
            {
                if (!Conn.OpenConnection())
                {
                    MessageBox.Show("Unable to connect to the database.", "Error");
                    return reviews;
                }

                string sql = "SELECT u.username, r.description, r.rating, r.id, r.review_date " +
                            "FROM review r " +
                            "JOIN user u ON r.user_id = u.id " +
                            "WHERE r.book_id = @BookId ";

                using MySqlCommand command = new(sql, Conn.GetConnection());
                command.Parameters.AddWithValue("@BookId", book.ISBNNumber);

                using MySqlDataReader reader = command.ExecuteReader();
                while (await reader.ReadAsync())
                {
                    Review review = new Review
                    {
                        Id = reader.GetInt32("id"),
                        Description = reader.GetString("description"),
                        Rating = reader.GetInt32("rating"),
                        ReviewDate = reader.GetDateTime("review_date"),
                        User = new User
                        {
                            Username = reader.GetString("username")
                        }
                    };

                    reviews.Add(review);
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

        public void AddReview(string description, int rating, Book book)
        {
            User user = CurrentUserManager.Instance.CurrentUser;
            try
            {
                if (!Conn.OpenConnection())
                {
                    MessageBox.Show("Unable to connect to the database.", "Error");
                    return;
                }

                string sql = "INSERT INTO review (user_id, book_id, description, review_date, rating) VALUES (@UserId, @BookId, @Description, @CreatedAt, @Rating)";
                MySqlCommand command = new MySqlCommand(sql, Conn.GetConnection());
                command.Parameters.AddWithValue("@UserId", user.Id);
                command.Parameters.AddWithValue("@Rating", rating);
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
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                Conn.CloseConnection();
            }
        }

        public bool IsReviewed(Book book)
        {
            User user = CurrentUserManager.Instance.CurrentUser;

            try
            {
                if (!Conn.OpenConnection())
                {
                    MessageBox.Show("Unable to connect to the database.", "Error");
                    return false;
                }

                string sql = "SELECT COUNT(*) FROM review WHERE user_id = @UserId AND book_id = @BookId";
                MySqlCommand command = new MySqlCommand(sql, Conn.GetConnection());
                command.Parameters.AddWithValue("@UserId", user.Id);
                command.Parameters.AddWithValue("@BookId", book.ISBNNumber);

                int count = Convert.ToInt32(command.ExecuteScalar());

                return count > 0;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                Conn.CloseConnection();
            }

            return false;
        }

        public int OneStar(Book book)
        {
            try
            {
                if (!Conn.OpenConnection())
                {
                    MessageBox.Show("Unable to connect to the database.", "Error");
                }
                string sql = "SELECT COUNT(*) AS count FROM review WHERE rating = 1 AND book_id = @BookId;";
                using (MySqlCommand command = new MySqlCommand(sql, Conn.GetConnection()))
                {
                    command.Parameters.AddWithValue("@BookId", book.ISBNNumber);
                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToInt32(result);
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

            return 0;
        }

        public int TwoStar(Book book)
        {
            try
            {
                if (!Conn.OpenConnection())
                {
                    MessageBox.Show("Unable to connect to the database.", "Error");
                }
                string sql = "SELECT COUNT(*) AS count FROM review WHERE rating = 2 AND book_id = @BookId;";
                using (MySqlCommand command = new MySqlCommand(sql, Conn.GetConnection()))
                {
                    command.Parameters.AddWithValue("@BookId", book.ISBNNumber);
                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToInt32(result);
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

            return 0;
        }

        public int ThreeStar(Book book)
        {
            try
            {
                if (!Conn.OpenConnection())
                {
                    MessageBox.Show("Unable to connect to the database.", "Error");
                }
                string sql = "SELECT COUNT(*) AS count FROM review WHERE rating = 3 AND book_id = @BookId;";
                using (MySqlCommand command = new MySqlCommand(sql, Conn.GetConnection()))
                {
                    command.Parameters.AddWithValue("@BookId", book.ISBNNumber);
                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToInt32(result);
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

            return 0;
        }

        public int FourStar(Book book)
        {
            try
            {
                if (!Conn.OpenConnection())
                {
                    MessageBox.Show("Unable to connect to the database.", "Error");
                }
                string sql = "SELECT COUNT(*) AS count FROM review WHERE rating = 4 AND book_id = @BookId;";
                using (MySqlCommand command = new MySqlCommand(sql, Conn.GetConnection()))
                {
                    command.Parameters.AddWithValue("@BookId", book.ISBNNumber);
                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToInt32(result);
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

            return 0;
        }

        public int FiveStar(Book book)
        {
            try
            {
                if (!Conn.OpenConnection())
                {
                    MessageBox.Show("Unable to connect to the database.", "Error");
                }
                string sql = "SELECT COUNT(*) AS count FROM review WHERE rating = 5 AND book_id = @BookId;";
                using (MySqlCommand command = new MySqlCommand(sql, Conn.GetConnection()))
                {
                    command.Parameters.AddWithValue("@BookId", book.ISBNNumber);
                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToInt32(result);
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

            return 0;
        }

        public int AllStar(Book book)
        {
            try
            {
                if (!Conn.OpenConnection())
                {
                    MessageBox.Show("Unable to connect to the database.", "Error");
                }
                string sql = "SELECT COUNT(*) AS count FROM review WHERE book_id = @BookId;";
                using (MySqlCommand command = new MySqlCommand(sql, Conn.GetConnection()))
                {
                    command.Parameters.AddWithValue("@BookId", book.ISBNNumber);
                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToInt32(result);
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

            return 0;
        }
    }
}

