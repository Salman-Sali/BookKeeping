using Bk.Infrastructure.Queries.BookEntries;
using Bk.Infrastructure.Queries.Books;
using Bk.UserInterface.Views.Book.ListBooks;
using Bk.UserInterface.Views.Layout;
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

namespace Bk.UserInterface.Views.Book.BookEntry
{
    /// <summary>
    /// Interaction logic for BookEntriesView.xaml
    /// </summary>
    public partial class BookEntriesView : UserControl, IPageControl
    {
        private readonly int BookId;
        private readonly IMediator _mediator;
        private int? page = null;
        private int total = 0;
        private readonly PageControl pageControl;
        private readonly MainWindow _mainWindow;
        public BookEntriesView(IMediator mediator, int bookId, MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            _mediator = mediator;
            BookId = bookId;
            pageControl = new PageControl(this);
            PageContentControl.Content = pageControl;
            LoadEntries();
        }

        public async void LoadEntries()
        {
            var result = await _mediator.Send(new GetBookEntries { BookId = BookId, Page = page });
            EntriesDataGrid.ItemsSource = result.Entries;
            total = result.TotalPages;
            if (page == null)
            {
                page = total;
            }
            pageControl.PageCountText.Text = page + " Of " + total;
            if (page <= 1)
            {
                pageControl.FirstPageButton.Visibility = Visibility.Collapsed;
                pageControl.PreviousPageButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                pageControl.FirstPageButton.Visibility = Visibility.Visible;
                pageControl.PreviousPageButton.Visibility = Visibility.Visible;
            }

            if (total <= 1 || page == total)
            {
                pageControl.LastPageButton.Visibility = Visibility.Collapsed;
                pageControl.NextPageButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                pageControl.LastPageButton.Visibility = Visibility.Visible;
                pageControl.NextPageButton.Visibility = Visibility.Visible;
            }
        }

        public void First()
        {
            page = 1;
            LoadEntries();
        }

        public void Last()
        {
            if (page != total)
            {
                page = total;
                LoadEntries();
            }
        }

        public void Next()
        {
            if (total > page)
            {
                page++;
                LoadEntries();
            }
        }

        public void Previous()
        {
            if (page > 1)
            {
                page--;
                LoadEntries();
            }
        }

        private void Row_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var row = ItemsControl.ContainerFromElement((DataGrid)sender,
                                                e.OriginalSource as DependencyObject) as DataGridRow;
            if (row == null)
            {
                return;
            }
            var data = (BookEntryResponse)row.DataContext;
            _mainWindow.ShowBookEntryDetails(data);
        }
    }
}
