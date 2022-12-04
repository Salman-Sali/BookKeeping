using Bk.Domain.Enums;
using Bk.Infrastructure.Queries.Books;
using Bk.UserInterface.Views.Book.BookEntry;
using Bk.UserInterface.Views.Layout;
using MediatR;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Bk.UserInterface.Views.Book.ListBooks
{
    /// <summary>
    /// Interaction logic for ListBooksView.xaml
    /// </summary>
    public partial class ListBooksView : UserControl, IPageControl
    {
        private readonly ListBooksSortControl listBooksSortControl;
        private readonly PageControl listBooksPageControl;
        private readonly IMediator _mediator;
        private readonly MainWindow _mainWindow;
        public static int page;
        public static int total;
        public ListBooksView(IMediator mediator, MainWindow mainWindow)
        {
            InitializeComponent();
            page = 1;
            _mediator = mediator;
            _mainWindow = mainWindow;
            listBooksSortControl = new ListBooksSortControl(mediator, this);
            SortContentControl.Content = listBooksSortControl;
            listBooksPageControl = new PageControl(this);
            PageContentControl.Content = listBooksPageControl;
            First();
        }

        public async void ListBooks()
        {
            string? bookType = listBooksSortControl.BookTypeComboBox.Text;
            if(bookType == "All")
            {
                bookType = null;
            }
            var result = await _mediator.Send(new ListBooksQuery { BookType = bookType, Page = page });
            total = result.TotalPages;
            BookDataGrid.ItemsSource = result.Books;
            listBooksPageControl.PageCountText.Text = page + " Of " + total;
            if (page <= 1)
            {
                listBooksPageControl.FirstPageButton.Visibility = Visibility.Collapsed;
                listBooksPageControl.PreviousPageButton.Visibility = Visibility.Collapsed;
            } else {
                listBooksPageControl.FirstPageButton.Visibility = Visibility.Visible;
                listBooksPageControl.PreviousPageButton.Visibility = Visibility.Visible;
            }

            if (total <= 1 || page == total)
            {
                listBooksPageControl.LastPageButton.Visibility = Visibility.Collapsed;
                listBooksPageControl.NextPageButton.Visibility = Visibility.Collapsed;
            } else {
                listBooksPageControl.LastPageButton.Visibility = Visibility.Visible;
                listBooksPageControl.NextPageButton.Visibility = Visibility.Visible;
            }
        }

        public void Next()
        {
            if(total > page)
            {
                page++;
                ListBooks();
            }
        }

        public void Previous()
        {
            if (page > 1)
            {
                page--;
                ListBooks();
            }
        }

        public void Last()
        {
            if(page != total)
            {
                page = total;
                ListBooks();
            }
        }

        public void First()
        {
            page = 1;
            ListBooks();
        }

        private void Row_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var row = ItemsControl.ContainerFromElement((DataGrid)sender,
                                                e.OriginalSource as DependencyObject) as DataGridRow;
            if (row == null)
            {
                return;
            }
            var data = (BookResponse)row.DataContext;
            _mainWindow.ShowBookEntries(data.Id);
        }
    }
}
