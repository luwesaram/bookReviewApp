using bookReviewConsoleApplication.Model;
using bookReviewConsoleApplication.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace bookReviewConsoleApplication.UserControls
{
    /// <summary>
    /// Interaction logic for UserHeaders.xaml
    /// </summary>
    public partial class UserHeaders : UserControl
    {
        public UserHeaders()
        {
            InitializeComponent();
            User currentUser = CurrentUserManager.Instance.CurrentUser;
            lblUserName.Content = "Hi, " + currentUser.Username;
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to logout?", "Logout Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                CurrentUserManager.Instance.CurrentUser = null;
                var currentWindow = Window.GetWindow(this);

                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                currentWindow.Close();
            }
        }
    }
}
