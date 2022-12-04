using Bk.Infrastructure.Queries.Books;
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

namespace Bk.UserInterface.Views.Book.ListBooks
{
    /// <summary>
    /// Interaction logic for ListBooksSort.xaml
    /// </summary>
    public partial class ListBooksSortControl : UserControl
    {
        private readonly IMediator _mediator;
        private readonly ListBooksView _listBooksView;
        public ListBooksSortControl(IMediator mediator, ListBooksView listBooksView)
        {
            InitializeComponent();
            _mediator = mediator;
            _listBooksView = listBooksView;
            BookTypes();
        }

        public async void BookTypes()
        {
            var result = await _mediator.Send(new ListBookTypesQuery());
            foreach (var item in result.BookTypes)
            {
                BookTypeComboBox.Items.Add(item);
            }
        }

        private void BookTypeComboBox_DropDownClosed(object sender, EventArgs e)
        {
            _listBooksView.First();
        }
    }
}
