using Bk.UserInterface.Views.Book.BookEntry;
using MediatR;
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

namespace Bk.UserInterface.Views.Layout.LayoutButtonControls
{
    /// <summary>
    /// Interaction logic for BookDetailsControl.xaml
    /// </summary>
    public partial class BookDetailsControl : UserControl
    {
        private readonly int BookId;
        private readonly MainWindow _mainWindow;
        public BookDetailsControl(int bookId, MainWindow mainWindow)
        {
            InitializeComponent();
            BookId = bookId;
            _mainWindow = mainWindow;
        }

        private void AddEntryButton_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.InsertBookEntry(BookId);
        }

        private void BookDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.UpdateBookDetails(BookId);
        }
    }
}
