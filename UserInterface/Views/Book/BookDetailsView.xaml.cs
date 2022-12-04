using Bk.Infrastructure.Queries.Books;
using Bk.UserInterface.Views.Login;
using BK.Application.Commands.Books;
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

namespace Bk.UserInterface.Views.Book
{
    /// <summary>
    /// Interaction logic for BookDetailsView.xaml
    /// </summary>
    public partial class BookDetailsView : UserControl
    {
        private readonly MainWindow _mainWindow;
        private readonly IMediator _mediator;
        private readonly int BookId;
        public BookDetailsView(MainWindow mainWindow, IMediator mediator, int bookId)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            _mediator = mediator;
            BookId = bookId;
            LoadBookDetails();
        }

        public async void LoadBookDetails()
        {
            var result = await _mediator.Send(new GetBookByIdQuery { BookId = BookId });
            NameTextBox.Text = result.Name;
            PhoneTextBox.Text = result.Phone;
            BookTypeText.Text = result.BookType;
            CreatedByText.Text = result.CreatedBy + " On " + result.CreatedOn;
            UpdatedByText.Text = result.UpdatedBy + " On " + result.UpdatedOn;
        }

        private async void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            await _mediator.Send(new UpdateBookCommand 
            {
                BookId= BookId, 
                Name = NameTextBox.Text, 
                Phone = PhoneTextBox.Text, 
                UserId = LoginView.UserId.Value 
            });
            _mainWindow.ShowBookEntries(BookId);
            MessageBox.Show("Book Details Updated!");
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.ShowBookEntries(BookId);
        }
    }
}
