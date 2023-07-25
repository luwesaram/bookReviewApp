using MySql.Data.MySqlClient;
using System.Windows;

namespace bookReviewConsoleApplication
{
    public class Connection
    {
        private MySqlConnection Conn;

        public Connection()
        {
            string ConnectionString = "Data Source=localhost;Database=bookreview;User=root;Password=;SSL Mode =0";
            Conn = new MySqlConnection(ConnectionString);
        }

        public bool OpenConnection()
        {
            try
            {
                Conn.Open();
                return true;
            }
            catch (MySqlException e)
            {
                switch (e.Number)
                {
                    case 0:
                        MessageBox.Show("Error! Cannot connect to server:" + e, "Error!");
                        break;
                    case 1045:
                        MessageBox.Show("Invalid username/password.");
                        break;
                }
                return false;
            }
        }

        public void CloseConnection()
        {
            this.Conn.Close();
        }

        public MySqlConnection GetConnection()
        {
            return this.Conn;
        }
    }

}
