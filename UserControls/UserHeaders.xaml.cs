using bookReviewConsoleApplication.Model;
using bookReviewConsoleApplication.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public partial class UserHeaders : UserControl, INotifyPropertyChanged
    {
        private string _upload;

        public string Upload
        {
            get { return _upload; }
            set
            {
                _upload = value;
                OnPropertyChanged(nameof(Upload));
            }
        }

        public UserHeaders()
        {
            InitializeComponent();
            this.DataContext = this;
            User currentUser = CurrentUserManager.Instance.CurrentUser;
            Connection Conn = new();
            UserManager userManager = new();

            lblUserName.Content = "Hello " + currentUser.Username;
            Upload = userManager.IsAuthor() ? "Upload A Book" : "Become an Author";
        }

        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {

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

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
