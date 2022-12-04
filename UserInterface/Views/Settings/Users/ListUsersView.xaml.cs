using Bk.Infrastructure.Queries.Books;
using Bk.Infrastructure.Queries.Users;
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

namespace Bk.UserInterface.Views.Settings.Users
{
    /// <summary>
    /// Interaction logic for ListUsersView.xaml
    /// </summary>
    public partial class ListUsersView : UserControl, IPageControl
    {
        private readonly IMediator _mediator;
        private readonly PageControl listBooksPageControl;
        private readonly MainWindow _mainWindow;

        private int page;
        private int total;
        public ListUsersView(IMediator mediator, MainWindow mainWindow)
        {
            InitializeComponent();
            _mediator = mediator;
            _mainWindow = mainWindow;
            page = 1;
            total = 0;
            listBooksPageControl = new PageControl(this);
            PageContentControl.Content = listBooksPageControl;
            First();
        }

        public async void ListUsers()
        {
            var result = await _mediator.Send(new ListUsersQuery { Page = page });
            total = result.TotalPages;
            UserDataGrid.ItemsSource = result.Users;
            listBooksPageControl.PageCountText.Text = page + " Of " + total;
            if (page <= 1)
            {
                listBooksPageControl.FirstPageButton.Visibility = Visibility.Collapsed;
                listBooksPageControl.PreviousPageButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                listBooksPageControl.FirstPageButton.Visibility = Visibility.Visible;
                listBooksPageControl.PreviousPageButton.Visibility = Visibility.Visible;
            }

            if (total <= 1 || page == total)
            {
                listBooksPageControl.LastPageButton.Visibility = Visibility.Collapsed;
                listBooksPageControl.NextPageButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                listBooksPageControl.LastPageButton.Visibility = Visibility.Visible;
                listBooksPageControl.NextPageButton.Visibility = Visibility.Visible;
            }
        }


        public void First()
        {
            page = 1;
            ListUsers();
        }

        public void Last()
        {
            if (page != total)
            {
                page = total;
                ListUsers();
            }
        }

       

        public void Next()
        {
            if (total > page)
            {
                page++;
                ListUsers();
            }
        }

        public void Previous()
        {
            if (page > 1)
            {
                page--;
                ListUsers();
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
            var data = (UserResponse)row.DataContext;
            _mainWindow.UserDetails(data);
        }
    }
}
