using bookReviewConsoleApplication.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace bookReviewConsoleApplication.ViewModel
{
    public class AddBookViewModel : INotifyPropertyChanged
    {
        private readonly Connection Conn;

        private List<Genre> _genres;

        public List<Genre> Genres
        {
            get { return _genres; }
            set { 
                _genres = value; 
                OnPropertyChanged(nameof(Genres));
            }
        }

        public AddBookViewModel(Connection conn)
        {
            Conn = conn;
            Genres = GetGenres();
        }

        private List<Genre> GetGenres()
        {
            List<Genre> genreList = new();

            try
            {
                if(!Conn.OpenConnection())
                {
                    MessageBox.Show("Unable to connect to the database.", "Error");
                    return genreList;
                }

                string sql = "SELECT * FROM genre";
                using MySqlCommand command = new(sql, Conn.GetConnection());
                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Genre genre = new()
                    {
                        Id = reader.GetInt32("id"),
                        Name = reader.GetString("name")
                    };
                    genreList.Add(genre);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                Conn.CloseConnection();
            }

            return genreList;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
