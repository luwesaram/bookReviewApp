using bookReviewConsoleApplication.Model;
using bookReviewConsoleApplication.ViewModel;
using Microsoft.Win32;
using System;
using System.Windows;

namespace bookReviewConsoleApplication.View
{
    /// <summary>
    /// Interaction logic for AddBook.xaml
    /// </summary>
    public partial class AddBook : Window
    {
        private readonly Connection Conn;
        public AddBook()
        {
            InitializeComponent();
            Conn = new();

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
            Genre selectedGenre = ((AddBookViewModel)DataContext).SelectedGenre; // Use the selected genre
            DateTime publicationDate = dpPublicationDate.SelectedDate ?? DateTime.Now; // Use the selected date or default to current date


            BookManager bookManager = new(Conn);
            bookManager.CreateBook(isbnNumber,title,description, selectedGenre, publicationDate, pageCount, imagePath);
        }
    }
}
