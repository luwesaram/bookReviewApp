using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows;
using System.Security.Cryptography.X509Certificates;

namespace bookReviewConsoleApplication
{
    public class Connection
    {
        private MySqlConnection Conn;
        private string Server;
        private string User;
        private string Password;
        private string Db;

        public Connection()
        {

        }

        private void Initialize()
        {
            Server = "localhost";
            Db = "";
            User = "root";
            Password = "";
            string ConnectionString;
            ConnectionString = "Data Source=" + Server +
                               ";Database=" + Db +
                               ";User Id=" + User +
                               ";Password" + Password + ";SSL Mode=0";
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
                        MessageBox.Show("Error! Cannot connect to server:" + e);
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
