﻿using System;
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
using bookReviewConsoleApplication.ViewModel;
using bookReviewConsoleApplication.View;
using bookReviewConsoleApplication.Model;

namespace bookReviewConsoleApplication
{
    /// <summary>
    /// Interaction logic for UserPage.xaml
    /// </summary>
    public partial class UserPage : Window
    {
        public UserPage()
        {
    
            InitializeComponent();
            this.DataContext = new MainViewModel();
        }

        private void ImgMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var image = (Image)sender;

            if (image.Tag is Book book)
            {
                BookDetailPage bookDetailPage = new BookDetailPage(book);
                bookDetailPage.Show();
                this.Close();
            }

        }
    }
}