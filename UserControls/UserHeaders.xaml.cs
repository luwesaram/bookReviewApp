using bookReviewConsoleApplication.Model;
using bookReviewConsoleApplication.View;
using bookReviewConsoleApplication.ViewModel;
using System;
using System.ComponentModel;
using System.Printing;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

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
            if(Upload == "Upload A Book")
            {
                var currentWindow = Window.GetWindow(this);

                AddBook window = new();
                window.Show();
                currentWindow.Close();
            }
            else
            {
               PenNameWindow penNameWindow = new();

                if(penNameWindow.ShowDialog() == true )
                {
                    string PenName = penNameWindow.PenName;
                    Author author = new(CurrentUserManager.Instance.CurrentUser.Username)
                    {
                        PenName = PenName,
                    };

                    UserManager userManager = new();
                    userManager.CreateAuthor(PenName);
                    CurrentUserManager.Instance.Initialize(CurrentUserManager.Instance.CurrentUser);

                    var currentWindow = Window.GetWindow(this);
                    MainWindow window = new();
                    window.Show();
                    currentWindow.Close();
                }
            }
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
