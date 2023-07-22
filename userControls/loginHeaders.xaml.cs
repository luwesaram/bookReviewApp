using System;
using System.Collections.Generic;
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
