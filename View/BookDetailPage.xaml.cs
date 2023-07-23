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
using bookReviewConsoleApplication.Model;

namespace bookReviewConsoleApplication.View
{
    /// <summary>
    /// Interaction logic for BookDetailPage.xaml
    /// </summary>
    public partial class BookDetailPage : Window
    {
        private readonly Book book;
        public BookDetailPage(Book book)
        {
            InitializeComponent();
            this.book = book;
            // with DataContext set to book property, you may now
            // access book properties using the syntax {Binding [PropertyName]}
            DataContext = book;
        }
    }
}
