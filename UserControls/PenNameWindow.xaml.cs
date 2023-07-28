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
using System.Windows.Shapes;

namespace bookReviewConsoleApplication.UserControls
{
    /// <summary>
    /// Interaction logic for PenNameWindow.xaml
    /// </summary>
    public partial class PenNameWindow : Window
    {
        public string PenName { get; private set; }

        public PenNameWindow()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPenName.Text))
            {
                MessageBox.Show("Pen Name cannot be empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                PenName = txtPenName.Text;
                DialogResult = true;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
