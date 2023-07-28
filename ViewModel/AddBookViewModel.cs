using bookReviewConsoleApplication.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace bookReviewConsoleApplication.ViewModel
{
    public class AddBookViewModel : INotifyPropertyChanged
    {
        private readonly Connection Conn;

        private List<Genre> _genres;

        private DateTime _selectedDate;

        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set { 
                _selectedDate = value; 
                OnPropertyChanged(nameof(SelectedDate));
            }
        }

        public List<Genre> Genres
        {
            get { return _genres; }
            set { 
                _genres = value; 
                OnPropertyChanged(nameof(Genres));
            }
        }

        private Genre _selectedGenre;

        public Genre SelectedGenre
        {
            get { return _selectedGenre; }
            set { 
                _selectedGenre = value; 
                OnPropertyChanged(nameof(SelectedGenre));
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
