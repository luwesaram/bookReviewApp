using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace bookReviewConsoleApplication.UserControls
{
    /// <summary>
    /// Interaction logic for loginHeaders.xaml
    /// </summary>
    public partial class loginHeaders : UserControl
    {
        public loginHeaders()
        {
            InitializeComponent();
        }

        private void lblHeaderDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var currentWindow = Window.GetWindow(this);

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            currentWindow.Close();

        }
    }
}
