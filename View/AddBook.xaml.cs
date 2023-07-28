using bookReviewConsoleApplication.Model;
using bookReviewConsoleApplication.ViewModel;
using Microsoft.Win32;
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

namespace bookReviewConsoleApplication.View
{
    /// <summary>
    /// Interaction logic for AddBook.xaml
    /// </summary>
    public partial class AddBook : Window
    {
        public AddBook()
        {
            InitializeComponent();
            Connection Conn = new();

            DataContext = new AddBookViewModel(Conn);
        }

        private void BtnBrowse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files (*.jpg;*.jpeg;*.png;*.gif)|*.jpg;*.jpeg;*.png;*.gif|All Files (*.*)|*.*",
                Title = "Select an Image"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                txtImagePath.Text = openFileDialog.FileName;
            }
        }
        private void BtnCreateBook_Click(object sender, RoutedEventArgs e)
        {
            string isbnNumber = txtISBNNumber.Text;
            string title = txtTitle.Text;
            string description = txtDescription.Text;
            string imagePath = txtImagePath.Text; // Use this path to load the image
            int pageCount = int.Parse(txtPageCount.Text);
            string selectedGenre = cmbGenre.SelectedItem?.ToString(); // Use the selected genre
        }
    }
}
